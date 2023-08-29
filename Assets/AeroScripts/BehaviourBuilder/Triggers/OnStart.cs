using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart : Trigger
{
    //name = "On Start";
    public override bool isTriggered()
    {
        return true;
    }

    public override bool isOnetime()
    {
        return true;
    }
}
