using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Action : MonoBehaviour
{
    public abstract IEnumerator Initialize();
}

public class ActionManager : MonoBehaviour
{
    public static GameObject ActionListUI = null;
    // Register Action Here
    Dictionary<string, Type> ActionDictionary = new Dictionary<string, Type>()
    {
        {"Bounce", typeof(Bounce)},
        {"Orbit", typeof(Orbit)}
    };

    public void Start()
    {
        if (ActionListUI == null)
        {
            ActionListUI = GameObject.FindWithTag("ActionListUI");
            toggleActionListUI();
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

    public static void pauseAll(){}

    
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
