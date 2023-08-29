using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BehaviourBuilderUI : MonoBehaviour
{
    public GameObject ActionListUI;
    public GameObject TriggerListUI;
    public GameObject BehaviourPanelTemplate;
    public List<BuilderScrollPanelUI> behavioursPanels = new List<BuilderScrollPanelUI>();
    private int editingIndex;

    void Start()
    {
        BehaviourPanelTemplate.SetActive(false);
        ResetTriggerList();
    }

    public void setIndex(int i)
    {
        this.editingIndex = i;
    }


    public void SelectTriggerWithNameFor(string triggerName)
    {
        Type T = BehaviourBuilder.TriggerDictionary[triggerName];
        behavioursPanels[editingIndex-1].behaviour.TTrigger = T;
        ResetTriggerList();
        behavioursPanels[editingIndex-1].updateInfo();
    }

    public void DisplayTriggerList()
    {
        TriggerListUI.SetActive(true);
    }

    public void ResetTriggerList()
    {
        TriggerListUI.SetActive(false);
    }

    public void NewBehaviour()
    {
        GameObject panel = Instantiate(BehaviourPanelTemplate,BehaviourPanelTemplate.transform.parent);
        panel.GetComponent<BuilderScrollPanelUI>().behaviour = new BehaviourBuilder();
        float panelWidth = panel.GetComponent<RectTransform>().rect.width * panel.transform.localScale.x;
        panel.transform.localPosition = BehaviourPanelTemplate.transform.localPosition + behavioursPanels.ToArray().Length*new Vector3(panelWidth, 0, 0);
        panel.SetActive(true);
        panel.GetComponent<BuilderScrollPanelUI>().BehaviourIndex = behavioursPanels.ToArray().Length+1;
        behavioursPanels.Add(panel.GetComponent<BuilderScrollPanelUI>());
    }





    

    

    

}
