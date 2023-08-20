using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController : MonoBehaviour
{
    IsState currentState;

    public virtual void Update()
    {
        if (currentState != null)
            currentState.UpdateState(this);
    }

    public void ChangeState(IsState newState) {
        if (currentState != null)
            currentState.OnExit(this);
        currentState = newState;
        currentState.OnEnter(this);
    }
}
