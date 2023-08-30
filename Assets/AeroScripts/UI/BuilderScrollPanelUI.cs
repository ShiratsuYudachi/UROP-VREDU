using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BuilderScrollPanelUI : MonoBehaviour
{
    public int BehaviourIndex = 0;
    public BehaviourBuilder behaviour;
    
    public int editingActionIndex = 0;
    public GameObject behaviourBuilderUI;
    public GameObject actionTemplate; // Assign them in Hierarchy
    public GameObject triggerTemplate;
    public ActionListUI actionListUI;

    void Awake()
    {
        this.actionTemplate.SetActive(false);
    }

    public void SetTriggerListUI()
    {
        behaviourBuilderUI.GetComponent<BehaviourBuilderUI>().setIndex(BehaviourIndex);
        behaviourBuilderUI.GetComponent<BehaviourBuilderUI>().DisplayTriggerList();
    }

    public void SetActionListUI()
    {
        
    }

    public void updateInfo()
    {
        if (behaviour.TTrigger != null)
            triggerTemplate.GetComponentInChildren<TextMeshProUGUI>().text = behaviour.TTrigger.Name;
        else
            triggerTemplate.GetComponentInChildren<TextMeshProUGUI>().text = "Select Trigger";
        
        if (behaviour.updaters != null)
            GenerateButtonsForActions();
        
    }

    public void GenerateButtonsForActions()
    {
    int i = 0;
    foreach (ActionUpdater updater in behaviour.updaters)
        {
            GameObject button = Instantiate(actionTemplate, actionTemplate.transform.parent);
            if (updater.actionType != null)//updaters already set up
                button.GetComponentInChildren<TextMeshProUGUI>().text = updater.actionType.Name;
            else button.GetComponentInChildren<TextMeshProUGUI>().text = "Select Action";
            float buttonHeight = button.GetComponent<RectTransform>().rect.height * button.transform.localScale.y;;
            button.transform.localPosition = actionTemplate.transform.localPosition - i*new Vector3(0, buttonHeight, 0);
            button.SetActive(true);
            i+=1;
            Debug.Log("ScrollPanel/INFO: updater button set!");
        }
    }

    public void AddAction()
    {
        this.behaviour.updaters.Add(new ActionUpdater());
        this.editingActionIndex = this.behaviour.updaters.ToArray().Length-1;
        updateInfo();
        StartCoroutine(this.EditAction());
    }
    

    public IEnumerator EditAction()
    {
        //TODO: Implementa a Aciton Editor UI, with Test run button
        
        ActionUpdater updater = behaviour.updaters[editingActionIndex];
        

        //Select Object
        yield return ObjectSelector.SelectObject();
        updater.gameObject = ObjectSelector.selectedObject;
        

        //Select Action
        yield return actionListUI.selectActionForUpdater(updater);
        updateInfo();
        if (updater.gameObject.GetComponent(updater.actionType) != null)
            updater.gameObject.GetComponent<ActionManager>().RemoveComponent(updater.actionType);
        Component component = updater.gameObject.AddComponent(updater.actionType);
        
        //Setup updater params
        if (component is Action action)
            yield return action.InitializeUpdater(updater);
        else
            Debug.LogError("ScrollPanel/ERROR: Component is not Action!");
    }
    
    
    public void enable()
    {
        StartCoroutine(behaviour.enable());
    }



}
