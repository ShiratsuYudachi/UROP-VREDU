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
}
