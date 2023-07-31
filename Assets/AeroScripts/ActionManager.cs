using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Action : MonoBehaviour
{
    public bool isActive = false; 
    public abstract IEnumerator Initialize();

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

    public void AddAction<T>() where T : Action
    {
        Action existing = gameObject.GetComponent<Action>();
        if (existing != null)
        {
            RemoveComponent(existing.GetType());
        }
        T action = gameObject.AddComponent<T>();
        StartCoroutine(action.Initialize());
    }

    public void AddActionWithNameToSelected(string actionName)
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
            StartCoroutine(action.Initialize());
        }
    }

    
    public void RemoveComponent(Type T, GameObject targetObject = null)
    {
        if (targetObject == null) targetObject = this.gameObject;
        Destroy(targetObject.GetComponent(T));
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
