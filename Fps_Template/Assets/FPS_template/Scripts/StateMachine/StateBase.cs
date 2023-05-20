using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewState", menuName = "ScriptableObjects/StateMachine/State")]
public class StateBase : ScriptableObject
{
    [SerializeField]
    protected string _name;

    [SerializeField]
    public FSMAction startingAction;
    [SerializeField]
    public FSMAction exitingAction;
    [SerializeField]
    public List<FSMAction> actions = new List<FSMAction>();
    [SerializeField]
    private string _animParameterName = "";

    [SerializeField]
    public List<FSMCondition> conditions = new List<FSMCondition>();   

    // when we enter the state 
    // usually set the animation, 
    public virtual void EnterState(StateMachineBase sm)
    {
        if (startingAction == null) return;

        startingAction.ExecuteAction(sm);

        // maybe check that the parameter name is not null, empty or invalid
        // ... 
        if (string.IsNullOrWhiteSpace(_animParameterName)) return;

        sm.Anim.SetTrigger(_animParameterName);
    }

    // we loop through all the actions that happen in the state
    public virtual void ProcessState(StateMachineBase sm)
    {
        // check state machine 
        if (sm == null) return;

        // check actions list 
        if (actions == null || actions.Count <= 0) return;

        // execute all the actions
        for (int i = 0; i < actions.Count; i++)
        {
            //Debug.Log(actions[i]?.name);
            actions[i]?.ExecuteAction(sm);            
        }
    }

    // we loop through all the conditions of this state 
    // to check if we need to change the current state
    public virtual void EvaluateConditions(StateMachineBase sm)
    {
        // check conditions list 
        if (conditions == null || conditions.Count <= 0) return;

        // check all the conditions
        for (int i = 0; i < conditions.Count; i++)
        {
            StateBase nextState = conditions[i].EvaluateCondition(sm);

            if (nextState == null || nextState == this) continue;
            else if (nextState != null && nextState != this)
            {
                sm.ChangeState(nextState);
            }    
        }
    }

    // usually reset the animation
    public virtual void ExitState(StateMachineBase sm)
    {
        if (exitingAction == null) return;

        exitingAction.ExecuteAction(sm);
    }

    public string GetName()
    {
        return _name;
    }
}
