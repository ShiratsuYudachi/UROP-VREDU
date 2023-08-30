using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BehaviourBuilder
{
    public Type TTrigger;
    
    public List<ActionUpdater> updaters = new List<ActionUpdater>();

    public BehaviourBuilder(Type TTrigger, ActionUpdater updater)
    {
        this.TTrigger = TTrigger;
        this.updaters = new List<ActionUpdater> {updater};
    }
    public BehaviourBuilder(Type TTrigger, List<ActionUpdater> updaters)
    {
        this.TTrigger = TTrigger;
        this.updaters = updaters;
    }
    public BehaviourBuilder()
    {
        this.TTrigger = null;
        this.updaters = new List<ActionUpdater>();
    }

    //Register Trigger here
    public static Dictionary<string, Type> TriggerDictionary = new Dictionary<string, Type>()
    {
        {"OnStart", typeof(OnStart)}
    };

    //start to listen trigger for update actions
    public IEnumerator enable()
    {
        Trigger trigger = (Trigger)Activator.CreateInstance(this.TTrigger);
        while(true)
        {
            if(trigger.is_rising_edge())
            {
                foreach (ActionUpdater updater in this.updaters)
                {
                    while (!updater.doneCreate) yield return null;
                    updater.update();
                }
                if (trigger.isOnetime()) break;
            }
            yield return null;
        }
    }

    public static IEnumerator Listen(Type T, List<ActionUpdater> updaters)
    {
        BehaviourBuilder builder = new BehaviourBuilder(T, updaters);
        yield return builder.enable();
    }
    public static IEnumerator Listen(Type T, ActionUpdater updater)
    {
        BehaviourBuilder builder = new BehaviourBuilder(T, updater);
        yield return builder.enable();
    }


}