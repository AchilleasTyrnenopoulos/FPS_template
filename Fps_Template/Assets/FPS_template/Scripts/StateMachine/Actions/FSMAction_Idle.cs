using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleAction", menuName = "ScriptableObjects/StateMachine/Actions/IdleAction")]
public class FSMAction_Idle : FSMAction
{
    private float _counter = 0f;
    private float _rate = 2f;
    
    public override void ExecuteAction(StateMachineBase sm)
    {
        // make sure that the agent doesn't move
        sm.Agent.isStopped = true;

        _counter += Time.deltaTime;
        if (_counter >= _rate)
        {
            //Debug.Log($"{sm.gameObject.name} is in Idle state");
            _counter = 0f;
        }
    }
}
