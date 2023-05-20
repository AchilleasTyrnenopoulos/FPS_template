using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateMachineBase : MonoBehaviour
{
    [SerializeField]
    protected StateBase _currentState;
    [SerializeField]
    protected bool _isSwitchingState = false;

    [SerializeField]
    protected StateBase _startingState;
    [SerializeField]
    protected List<StateBase> _states = new List<StateBase>();

    private PlayerController _player;
    public NavMeshAgent Agent { get; private set; }
    public Animator Anim { get; private set; }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _currentState = _startingState;
        _player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        ProcessCurrentState(this);
    }

    public virtual void ProcessCurrentState(StateMachineBase sm)
    {
        //check if we are switching states
        if(_isSwitchingState) return;

        //call current state's ProcessState
        _currentState.ProcessState(this);

        //check current state's conditions
        _currentState.EvaluateConditions(this);
    }

    public virtual void ChangeState(StateBase nextState)
    {
        _isSwitchingState = true;

        _currentState.ExitState(this);

        // get next state from the list
        foreach(var state in _states)
        {
            if (state.name == nextState.name)
            {
                _currentState = nextState;
                break;
            }
        }

        _currentState.EnterState(this);

        _isSwitchingState = false;
    }

    public Transform GetPlayerLocation()
    {
        return _player.transform;
    }

}
