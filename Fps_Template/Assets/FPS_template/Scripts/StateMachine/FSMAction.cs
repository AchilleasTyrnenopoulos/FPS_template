using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FSMAction : ScriptableObject
{
    public virtual void ExecuteAction(StateMachineBase sm)
    {

    }
}
