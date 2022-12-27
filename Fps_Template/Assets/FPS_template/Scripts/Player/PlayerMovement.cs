using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementStates _currentMovementState;

    public PlayerController Controller { get; private set; }
    [SerializeField] private float _movementSpeed = 1f;

    [Tooltip("Sharpness for the movement when grounded, a low value will make the player accelerate and decelerate slowly, a high value will do the opposite")]
    [SerializeField] private float _movementSharpnessOnGround = 15f;
    public Vector3 characterVelocity { get; set; }


    [Header("Camera")]
    private float _cameraVerticalAngle = 0f;

    [Header("Air Movement")]
    [SerializeField] private float _airSpeed = 10f;
    [Tooltip("Acceleration speed when in the air")]
    [SerializeField] private float _accelerationSpeedInAir = 25f;

    [Header("Rotation")]
    [SerializeField] private float _rotationSpeed = 200f;
    [Range(.1f, 1f)]
    [SerializeField] private float _rotationMultiplier = 1f;

    [Header("Footsteps")]
    [SerializeField] private float _footstepsFrequency = .3f;
    private float _footstepDistanceCounter = 0f;

    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _currentMovementState = PlayerMovementStates.IDLE;
    }

    private void Update()
    {
        HandleCharacterMovement();
    }

    private void HandleCharacterMovement()
    {
        #region Rotation
        //horizontal character rotation
        transform.Rotate(new Vector3(0f, (Controller.Input.GetHorizontalInput() * _rotationSpeed * _rotationMultiplier), 0f),
                        Space.Self);

        //vertical character rotation
        _cameraVerticalAngle += Controller.Input.GetVerticalInput() * _rotationSpeed * _rotationMultiplier;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -89f, 89f);
        Camera.main.transform.localEulerAngles = new Vector3(_cameraVerticalAngle, 0, 0);
        #endregion

        #region Set movement state / Needs refactoring
        Vector3 moveInput = Controller.Input.GetMoveInput();
        if (Controller.isGrounded)
        {
            if (moveInput == Vector3.zero && _currentMovementState != PlayerMovementStates.IDLE)
            {
                //set idle state
                _currentMovementState = PlayerMovementStates.IDLE;
                Debug.Log("is idle");
            }
            else if (moveInput != Vector3.zero && _currentMovementState != PlayerMovementStates.MOVING)
            {
                //set moving
                _currentMovementState = PlayerMovementStates.MOVING;
                Debug.Log("is moving");
            }
        }
        else
        {
            if(_currentMovementState != PlayerMovementStates.ON_AIR)
            {
                _currentMovementState = PlayerMovementStates.ON_AIR;
                Debug.Log("is on air");
            }
        }
        #endregion

        // converts move input to a worldspace vector based on our character's transform orientation
        Vector3 worldspaceMoveInput = transform.TransformVector(moveInput);

        if (Controller.isGrounded)
        {
            // calculate the desired velocity from inputs, max speed, and current slope
            Vector3 targetVelocity = worldspaceMoveInput * _movementSpeed;

            targetVelocity = GetDirectionReorientedOnSlope(targetVelocity.normalized, Controller.GetGroundNormal()) * targetVelocity.magnitude;

            // smoothly interpolate between our current velocity and the target velocity based on acceleration speed
            characterVelocity = Vector3.Lerp(characterVelocity, targetVelocity, _movementSharpnessOnGround * Time.deltaTime);

            //footsteps             
            if (_footstepDistanceCounter >= 1f / _footstepsFrequency)
            {
                _footstepDistanceCounter = 0f;
                //_footstepsAudioSource.PlayOneShot();
                Controller.FootstepsManager.PlayFootstepSfx();
                Debug.Log("step");
            }
            _footstepDistanceCounter += characterVelocity.magnitude * Time.deltaTime;

        }
        else
        {
            // add air acceleration
            characterVelocity += _accelerationSpeedInAir * Time.deltaTime * worldspaceMoveInput;

            // limit air speed to a maximum, but only horizontally
            float verticalVelocity = characterVelocity.y;
            Vector3 horizontalVelocity = Vector3.ProjectOnPlane(characterVelocity, Vector3.up);
            horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, _airSpeed);
            characterVelocity = horizontalVelocity + (Vector3.up * verticalVelocity);

            // apply the gravity to the velocity
            characterVelocity += Controller.GetGravityDownForce() * Time.deltaTime * Vector3.down;
        }

        // apply the final calculated velocity value as a character movement
        Vector3 capsuleBottomBeforeMove = Controller.GetCapsuleBottomHemisphere();
        Vector3 capsuleTopBeforeMove = Controller.GetCapsuleTopHemisphere(Controller.CharController.height);
        Controller.CharController.Move(characterVelocity * Time.deltaTime);

        // detect obstructions to adjust velocity accordingly
        //m_LatestImpactSpeed = Vector3.zero;
        if (Physics.CapsuleCast(capsuleBottomBeforeMove, capsuleTopBeforeMove, Controller.CharController.radius,
            characterVelocity.normalized, out RaycastHit hit, characterVelocity.magnitude * Time.deltaTime, -1,
            QueryTriggerInteraction.Ignore))
        {
            // We remember the last impact speed because the fall damage logic might need it
            //m_LatestImpactSpeed = CharacterVelocity;

            characterVelocity = Vector3.ProjectOnPlane(characterVelocity, hit.normal);
        }
    }



    // Gets a reoriented direction that is tangent to a given slope
    public Vector3 GetDirectionReorientedOnSlope(Vector3 direction, Vector3 slopeNormal)
    {
        Vector3 directionRight = Vector3.Cross(direction, transform.up);
        return Vector3.Cross(slopeNormal, directionRight).normalized;
    }

}
