using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerController _controller;
    [SerializeField] private float _movementSpeed = 1f;

    [Tooltip("Sharpness for the movement when grounded, a low value will make the player accelerate and decelerate slowly, a high value will do the opposite")]
    [SerializeField] private float _movementSharpnessOnGround = 15f;
    private Vector3 _characterVelocity;

    [Header("Air Movement")]
    [SerializeField] private float _airSpeed = 10f;
    [Tooltip("Acceleration speed when in the air")]
    [SerializeField] private float _accelerationSpeedInAir = 25f;

    [Header("Footsteps")]
    [SerializeField] private float _footstepsFrequency = .3f;
    private float _footstepDistanceCounter = 0f;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        HandleCharacterMovement();
    }

    private void HandleCharacterMovement()
    {       
        // converts move input to a worldspace vector based on our character's transform orientation
        Vector3 worldspaceMoveInput = transform.TransformVector(_controller.GetMoveInput());

        if (_controller.isGrounded)
        {
            // calculate the desired velocity from inputs, max speed, and current slope
            Vector3 targetVelocity = worldspaceMoveInput * _movementSpeed;

            targetVelocity = GetDirectionReorientedOnSlope(targetVelocity.normalized, _controller.GetGroundNormal()) * targetVelocity.magnitude;

            // smoothly interpolate between our current velocity and the target velocity based on acceleration speed
            _characterVelocity = Vector3.Lerp(_characterVelocity, targetVelocity, _movementSharpnessOnGround * Time.deltaTime);

            //footsteps             
            if (_footstepDistanceCounter >= 1f / _footstepsFrequency)
            {
                _footstepDistanceCounter = 0f;
                //_footstepsAudioSource.PlayOneShot();
                _controller.FootstepsManager.PlayFootstepSfx();
                Debug.Log("step");
            }
            _footstepDistanceCounter += _characterVelocity.magnitude * Time.deltaTime;

        }
        else
        {
            // add air acceleration
            _characterVelocity += _accelerationSpeedInAir * Time.deltaTime * worldspaceMoveInput;

            // limit air speed to a maximum, but only horizontally
            float verticalVelocity = _characterVelocity.y;
            Vector3 horizontalVelocity = Vector3.ProjectOnPlane(_characterVelocity, Vector3.up);
            horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, _airSpeed);
            _characterVelocity = horizontalVelocity + (Vector3.up * verticalVelocity);

            // apply the gravity to the velocity
            _characterVelocity += _controller.GetGravityDownForce() * Time.deltaTime * Vector3.down;
        }

        // apply the final calculated velocity value as a character movement
        Vector3 capsuleBottomBeforeMove = _controller.GetCapsuleBottomHemisphere();
        Vector3 capsuleTopBeforeMove = _controller.GetCapsuleTopHemisphere(_controller.CharController.height);
        _controller.CharController.Move(_characterVelocity * Time.deltaTime);

        // detect obstructions to adjust velocity accordingly
        //m_LatestImpactSpeed = Vector3.zero;
        if (Physics.CapsuleCast(capsuleBottomBeforeMove, capsuleTopBeforeMove, _controller.CharController.radius,
            _characterVelocity.normalized, out RaycastHit hit, _characterVelocity.magnitude * Time.deltaTime, -1,
            QueryTriggerInteraction.Ignore))
        {
            // We remember the last impact speed because the fall damage logic might need it
            //m_LatestImpactSpeed = CharacterVelocity;

            _characterVelocity = Vector3.ProjectOnPlane(_characterVelocity, hit.normal);
        }
    }

    // Gets a reoriented direction that is tangent to a given slope
    private Vector3 GetDirectionReorientedOnSlope(Vector3 direction, Vector3 slopeNormal)
    {
        Vector3 directionRight = Vector3.Cross(direction, transform.up);
        return Vector3.Cross(slopeNormal, directionRight).normalized;
    }

}
