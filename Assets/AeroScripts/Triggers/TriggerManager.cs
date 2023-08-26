using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger
{
    private bool last_condition = false;
    public bool is_rising_edge()
    {
        bool i = isTriggered();
        bool toreturn = i && !last_condition;
        last_condition = i;
        return toreturn;
    }

    public abstract bool isTriggered();

    public static IEnumerator Listen(Trigger trigger, ActionUpdater[] updaterList)
    {
        while(true)
        {
            if(trigger.is_rising_edge())
            {
                foreach (ActionUpdater updater in updaterList)
                {
                    updater.update();
                }
            }
            yield return null;
        }
    }
}
