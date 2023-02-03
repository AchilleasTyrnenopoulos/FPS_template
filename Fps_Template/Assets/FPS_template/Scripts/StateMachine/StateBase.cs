using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : ScriptableObject
{
    [SerializeField]
    protected string _name;

    //public List<FSMAction> Action = new List<FSMAction>();
    //public List<Transition> Transitions = new List<Transition>();

    public abstract void EnterState();
    
    public abstract void ProcessState();
    public abstract void ExitState();
    public string GetName()
    {
        return _name;
    }
}
