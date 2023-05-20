using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTransition", menuName = "ScriptableObjects/StateMachine/Transitions/Transition")]
public class FSMTransition : ScriptableObject
{
    public List<FSMCondition> conditions;

    public StateBase nextState;
}
