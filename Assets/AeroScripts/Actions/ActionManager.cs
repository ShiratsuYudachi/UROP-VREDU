using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Action : MonoBehaviour
{
    //public static string name;
    public bool isActive = false; 
    public abstract IEnumerator Initialize(GameObject attachedObject);

    public abstract IEnumerator InitializeUpdater(ActionUpdater updater);
    //PBR for class, PBV for struct by default. However 'ref' keyword cannot be used in Iterator so we use class
    //Target Object is also inside ActionUpdater

    public abstract void InitializeWith(Dictionary<string,object> param);
}


//stores an update of an action, and release it later
public class ActionUpdater
{
    public GameObject gameObject;
    public Type actionType;
    public Dictionary<string,object> param = new Dictionary<string,object>();
    
    public bool doneCreate = false;

    public void update()
    {
        ActionManager manager = this.gameObject.GetComponent<ActionManager>();
        if ( manager != null)
        {
            manager.AddAction(actionType, param);
            Debug.Log("ActionUpdater: Action Added");
        }else
        {
            Debug.Log("ActionUpdater: Unable to find Update Target");
        }
    }
}


public class ActionManager : MonoBehaviour
{
    public static GameObject ActionListUI = null;
    public static string objectTag = "selectable";
    public static List<GameObject> activeObjects = new List<GameObject>();
    public static Dictionary<GameObject,bool> objectActiveState = new Dictionary<GameObject,bool>();

    // Register Action Here
    public static Dictionary<string, Type> ActionDictionary = new Dictionary<string, Type>()
    {
        {"Bounce", typeof(Bounce)},
        {"Orbit", typeof(Orbit)},
        {"Spin", typeof(Spin)}
    };

    

    public void Start()
    {
        if (ActionListUI == null)
        {
            ActionListUI = GameObject.FindWithTag("ActionListUI");
            toggleActionListUI();
        }

        activeObjects.Add(this.gameObject);
        Action action = this.gameObject.GetComponent<Action>();
        if (action!=null){
            objectActiveState[this.gameObject] = action.isActive;
        }
    }

    public void AddAction (Type T, Dictionary<string,object> param)
    {
        Component component = this.gameObject.AddComponent(T);
        if (component is Action action)//to make sure component is an instance of Action
        {
            action.InitializeWith(param);
        }else
        {
            Debug.LogError("ActionManager/ERROR: Action Component not found/matched!");
        }
    }


    public void LegacyAddActionWithNameToSelected(string actionName)
    {
        GameObject targetObject = ObjectSelector.selectedObject;
        Type T = ActionDictionary[actionName];
        if (targetObject.GetComponent(T) != null)
        {
            RemoveComponent(T);
        }
        Component component = targetObject.AddComponent(T);
        if (component is Action action)
        {
            StartCoroutine(action.Initialize(targetObject));
        }
    }

    public void AddActionWithNameToSelected(string actionName)
    {
        Type T = ActionDictionary[actionName];
        if (this.gameObject.GetComponent(T) != null)
        {
            this.RemoveComponent(T);
        }
        ActionUpdater updater = new ActionUpdater();
        updater.gameObject = ObjectSelector.selectedObject;
        updater.actionType = T;
        
        Component component = this.gameObject.AddComponent(T);
        if (component is Action action)
        {
            StartCoroutine(action.InitializeUpdater(updater));
            StartCoroutine(BehaviourBuilder.Listen<OnStart>(updater));
        }
        
    }

    
    public static void RemoveComponentFrom(Type T, GameObject targetObject)
    {
        Destroy(targetObject.GetComponent(T));
    }

    public void RemoveComponent(Type T)
    {
        Destroy(this.gameObject.GetComponent(T));
    }



    public void EditSystem(){}

    public static void pauseAll()
    {
        int i = 0;
        foreach (var activeObject in activeObjects)
        {
            Action[] actions = activeObject.GetComponents<Action>();
            if (actions!=null)
            {
                foreach (var action in actions)
                {
                    //objectActiveState[activeObject] = action.isActive;
                    action.isActive = false; 
                    Debug.Log("Updated "+activeObject.name+" with action "+action.GetType());
                }
            }
            i+=1;
            
        }
    }

    public static void activeAll()
    {
        int i = 0;
        foreach (var activeObject in activeObjects)
        {
            Action[] actions = activeObject.GetComponents<Action>();
            if (actions!=null)
            {
                foreach (var action in actions)
                {
                action.isActive = true;
                Debug.Log("Activated "+activeObject.name+" with action "+action.GetType());
                }
            }
            i+=1;
        }
    }

     //TODO, not usable
    public static void resumeAll()
    {
        int i = 0;
        foreach (var activeObject in activeObjects)
        {
            Action[] actions = activeObject.GetComponents<Action>();
            if (actions!=null)
            {
                foreach (var action in actions)
                {
                    action.isActive = objectActiveState[activeObject];
                    Debug.Log("Paused "+activeObject.name+" with action "+action.GetType());
                }
            }
            i+=1;
        }
    }

    
    private IEnumerator SelectToEdit()
    {
        yield return ObjectSelector.SelectObject();
        toggleActionListUI();
        yield return WaitForSelectAction();
    }
    public void selectToEdit(){StartCoroutine(SelectToEdit());}

    public static void toggleActionListUI ()
    {
        ActionListUI.SetActive(!ActionListUI.activeSelf);
    }

    public static IEnumerator WaitForSelectAction()
    {
        while(ActionListUI.activeSelf)
        {
            yield return null;
        }
    }
}
