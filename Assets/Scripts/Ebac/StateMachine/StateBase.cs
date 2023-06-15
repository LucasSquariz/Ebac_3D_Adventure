using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    public virtual void OnStateEnter()
    {
        Debug.Log("OnStateEnter");
    }

    public virtual void OnStateStay()
    {
        Debug.Log("OnStateExit");
    }

    public virtual void OnStateExit()
    {
        Debug.Log("OnStateExit");
    }
}
