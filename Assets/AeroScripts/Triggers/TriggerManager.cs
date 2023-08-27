using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Trigger
{
    private bool last_condition = false;
    public abstract bool isOnetime();
    public abstract bool isTriggered();
    

    public bool is_rising_edge()
    {
        bool i = isTriggered();
        bool toreturn = i && !last_condition;
        last_condition = i;
        return toreturn;
    }

    

    public static IEnumerator Listen(Type T, ActionUpdater[] updaterList)
    {
        Trigger trigger = (Trigger)Activator.CreateInstance(T);
        while(true)
        {
            if(trigger.is_rising_edge())
            {
                foreach (ActionUpdater updater in updaterList) updater.update();
                if (trigger.isOnetime()) break;
            }
            yield return null;
        }
    }

    public static IEnumerator Listen(Type T, ActionUpdater updater)
    {
        Trigger trigger = (Trigger)Activator.CreateInstance(T);
        while(true)
        {
            if(trigger.is_rising_edge() && updater.doneCreate)
            {
                updater.update();
                if (trigger.isOnetime()) break;
            }
            yield return null;
        }
    }
}