using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Range(0.1f, 1f)]
    [SerializeField] private float _lookSensitivity = 1f;

    private InputActions _input;

    private Vector2 _moveInput = Vector2.zero;
    private Vector2 _lookInput = Vector2.zero;

    private bool _isMoving = false;
    private bool _isRotating = false;
    private bool _isSprinting = false;

    private void OnEnable()
    {
        _input = new InputActions();

        _input.Player.Enable();

        _input.Player.Move.started += StartedMoving;
        _input.Player.Move.performed += SetMoveInput;
        _input.Player.Move.canceled += SetMoveInput;
        _input.Player.Move.canceled += StoppedMoving;

        _input.Player.Rotate.started += StartedRotating;
        _input.Player.Rotate.performed += SetLookInput;
        _input.Player.Rotate.canceled += SetLookInput;
        _input.Player.Rotate.canceled += StoppedRotating;

        _input.Player.Sprint.started += StartedSprinting;
        _input.Player.Sprint.canceled += StoppedSprinting;        
    }

    private void OnDisable()
    {
        _input.Player.Move.started -= StartedMoving;
        _input.Player.Move.performed -= SetMoveInput;
        _input.Player.Move.canceled -= SetMoveInput;
        _input.Player.Move.canceled -= StoppedMoving;

        _input.Player.Rotate.started -= StartedRotating;
        _input.Player.Rotate.performed -= SetLookInput;
        _input.Player.Rotate.canceled -= SetLookInput;
        _input.Player.Rotate.canceled -= StoppedRotating;

        _input.Player.Sprint.started -= StartedSprinting;
        _input.Player.Sprint.canceled -= StoppedSprinting;        

        _input.Player.Disable();
    }

    public void Disable()
    {
        Debug.Log("disable input");
        this.Disable();
    }



    #region Movement

    public Vector3 GetMoveInput()
    {
        return new Vector3(_moveInput.x, 0f, _moveInput.y);
    }
    public bool GetIsMoving() => _isMoving;
    private void SetMoveInput(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    private void StartedMoving(InputAction.CallbackContext ctx)
    {
        _isMoving = true;
    }

    private void StoppedMoving(InputAction.CallbackContext ctx)
    {
        _isMoving = false;
    }
    #endregion

    #region Rotation
    public void SetLookInput(InputAction.CallbackContext ctx)
    {
        _lookInput = ctx.ReadValue<Vector2>();
    }
    public bool GetIsRotating() => _isRotating;

    public float GetHorizontalInput()
    {
        return _lookInput.x;
    }
    public float GetVerticalInput()
    {
        return -_lookInput.y;
    }

    private void StartedRotating(InputAction.CallbackContext ctx)
    {
        _isRotating = true;
    }

    private void StoppedRotating(InputAction.CallbackContext ctx)
    {
        _isRotating = false;
    }


    #endregion

    #region Sprint
    public bool GetIsSprinting() => _isSprinting;

    private void StartedSprinting(InputAction.CallbackContext ctx)
    {
        _isSprinting = true;
    }

    private void StoppedSprinting(InputAction.CallbackContext ctx)
    {
        _isSprinting = false;
    }
    #endregion

    public bool GetInteractTrigger()
    {
        return _input.Player.Interact.triggered;
    }
   
}


