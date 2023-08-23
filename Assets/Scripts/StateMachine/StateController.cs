using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController : MonoBehaviour
{
    IsState currentState;
    protected GameObject XrOrigin;

    public virtual void Start()
    {
        XrOrigin = GameManager.Instance.PlayerXrOrigin.gameObject;
    }

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
