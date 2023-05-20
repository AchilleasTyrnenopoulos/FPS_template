using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMCondition : ScriptableObject
{
    [SerializeField]
    public StateBase trueState;
    [SerializeField]
    public StateBase falseState;
    public virtual StateBase EvaluateCondition(StateMachineBase sm)
    {
        /* if true we will return the true state
        *  if false we will return the false state
        *  if we dont set the true or false states from the Editor 
        *   we will essentially return null
        */

        // for now just return null
        return null;
    }
}
