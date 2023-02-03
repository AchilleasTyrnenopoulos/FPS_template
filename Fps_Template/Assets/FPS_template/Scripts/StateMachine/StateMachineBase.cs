using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBase : MonoBehaviour
{
    protected StateBase _currentState;
    [SerializeField]
    protected bool _isSwitchingState = false;

    public abstract void SetInitialState(StateBase state);

    public virtual void SwitchState(StateBase state)
    {
        if (_currentState == state) return;
        Debug.Log($"switched to {state.GetName()} state");

        //check if state's condition are met

        //
        _isSwitchingState = true;

        //call current state's ExitState
        _currentState.ExitState();

        //set current state to state
        _currentState = state;

        //call current state's EnterState
        _currentState.EnterState();

        //
        _isSwitchingState = false;
    }

    public virtual void ProcessCurrentState()
    {
        //check if we are switching states
        if(_isSwitchingState) return;

        //call current state's ProcessState
        _currentState.ProcessState();
    }
}
