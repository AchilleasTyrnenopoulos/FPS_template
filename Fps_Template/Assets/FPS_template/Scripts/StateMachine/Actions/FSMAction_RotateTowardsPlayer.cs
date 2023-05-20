using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RotateTowardsPlayer", menuName = "ScriptableObjects/StateMachine/Actions/RotateTowardsPlayer")]
public class FSMAction_RotateTowardsPlayer : FSMAction
{
    [SerializeField]
    private float _rotationSpeed = 4f;
    public override void ExecuteAction(StateMachineBase sm)
    {
        //sm?.Agent?.SetDestination(sm.GetPlayerLocation().position);

        //// just rotate the agent
        //sm.Agent.updatePosition = false;
        //sm.Agent.updateRotation = true;

        Vector3 directionToPlayer = sm.GetPlayerLocation().position - sm.transform.position;
        directionToPlayer.y = 0f; // Optional: Set the y component to zero to rotate only on the horizontal plane

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        
        sm.transform.rotation = Quaternion.Lerp(sm.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

    }
}
