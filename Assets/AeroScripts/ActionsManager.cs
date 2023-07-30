using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Action : MonoBehaviour
{
    public abstract IEnumerator Initialize();
}

public class ActionsManager : MonoBehaviour
{
    // Register Action Here
    Dictionary<string, Type> ActionDictionary = new Dictionary<string, Type>()
    {
        {"Bounce", typeof(Bounce)},
        {"Orbit", typeof(Orbit)}
    };

    public void AddAction<T>() where T : Action
    {
        if (gameObject.GetComponent<T>() != null)
        {
            RemoveComponent(typeof(T));
        }
        T action = gameObject.AddComponent<T>();
        StartCoroutine(action.Initialize());
    }

    public void AddActionWithName(string actionName)
    {
        Type T = ActionDictionary[actionName];
        if (gameObject.GetComponent(T) != null)
        {
            RemoveComponent(T);
        }
        Component component = gameObject.AddComponent(T);
        if (component is Action action)
        {
            StartCoroutine(action.Initialize());
        }
    }

    public void RemoveComponent(Type T)
    {
        Destroy(gameObject.GetComponent(T));
    }
}
