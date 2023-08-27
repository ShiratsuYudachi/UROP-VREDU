using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BehaviourBuilder
{
    private Type TTrigger;
    private ActionUpdater[] updaters;

    public BehaviourBuilder(Type TTrigger, ActionUpdater updater)
    {
        this.TTrigger = TTrigger;
        this.updaters = new ActionUpdater[] {updater};
    }
    public BehaviourBuilder(Type TTrigger, ActionUpdater[] updaters)
    {
        this.TTrigger = TTrigger;
        this.updaters = updaters;
    }

    //start to listen trigger for update actions
    public IEnumerator pose()
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

    public static IEnumerator Listen<T>(ActionUpdater[] updaters) where T : Trigger
    {
        BehaviourBuilder builder = new BehaviourBuilder(typeof(T), updaters);
        yield return builder.pose();
    }
    public static IEnumerator Listen<T>(ActionUpdater updater) where T : Trigger
    {
        BehaviourBuilder builder = new BehaviourBuilder(typeof(T), updater);
        yield return builder.pose();
    }


}