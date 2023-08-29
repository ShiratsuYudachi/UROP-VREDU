using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BuilderScrollPanelUI : MonoBehaviour
{
    public int BehaviourIndex = 0;
    public BehaviourBuilder behaviour;
    
    public GameObject behaviourBuilderUI;
    public GameObject actionTemplate; // Assign them in Hierarchy
    public GameObject triggerTemplate;

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
            GameObject button = Instantiate(actionTemplate);
            button.GetComponentInChildren<TextMeshProUGUI>().text = updater.actionType.Name;
            float buttonHeight = button.GetComponent<RectTransform>().rect.height * button.transform.localScale.y;;
            button.transform.localPosition = actionTemplate.transform.localPosition - i*new Vector3(0, buttonHeight, 0);
            button.SetActive(true);
            i+=1;
        }
    }



}
