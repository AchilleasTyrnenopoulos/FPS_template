using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentCanMove", menuName = "ScriptableObjects/StateMachine/Actions/AgentCanMove")]
public class FSMAction_AgentCanMove : FSMAction
{
    public override void ExecuteAction(StateMachineBase sm)
    {
        Debug.Log("Agent can move");
        sm.Agent.isStopped = false;
        sm.Agent.updatePosition = true;
        sm.Agent.updateRotation = true;
    }
}
