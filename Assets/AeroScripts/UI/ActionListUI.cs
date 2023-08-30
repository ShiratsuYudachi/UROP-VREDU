using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ActionListUI : MonoBehaviour
{
    private bool isSelecting = false;
    public Type TAction;

    public void Start()
    {
        this.gameObject.SetActive(false);
    }

    public IEnumerator selectActionForUpdater(ActionUpdater updater)
    {
        this.gameObject.SetActive(true);
        isSelecting = true;
        while (isSelecting)
        {
            yield return null;
        }
        this.gameObject.SetActive(false);
        updater.actionType = TAction;
    }

    public void selectAction(string actionName)//for button to use
    {
        TAction = ActionManager.ActionDictionary[actionName];
        isSelecting = false;
    }
    
    
}
