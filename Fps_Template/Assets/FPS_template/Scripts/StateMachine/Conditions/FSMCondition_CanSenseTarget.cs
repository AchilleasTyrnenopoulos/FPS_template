using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CanSenseTarget", menuName = "ScriptableObjects/StateMachine/Conditions/CanSenseTarget")]
public class FSMCondition_CanSenseTarget : FSMCondition
{
    [SerializeField]
    private float _senseDistance = 3f;
    [SerializeField]
    private bool _invertCondition = false;

    public override StateBase EvaluateCondition(StateMachineBase sm)
    {
        if(Vector3.Distance(sm.GetPlayerLocation().position, sm.transform.position) < Mathf.Abs(_senseDistance))
        {
            return trueState;
        }

        return falseState;
    }
}
