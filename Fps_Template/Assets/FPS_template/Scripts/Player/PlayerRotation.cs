using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private PlayerController _controller;

    [Header("Camera")]
    private float _cameraVerticalAngle = 0f;

    [Header("Rotation")]
    [SerializeField] private float _rotationSpeed = 7.5f;
    [Range(.1f, 1f)]
    [SerializeField] private float _rotationMultiplier = .5f;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();       
    }

    // Update is called once per frame
    void Update()
    {
        HandleCharacterRotation();
    }

    private void HandleCharacterRotation()
    {
        //horizontal character rotation
        this.transform.Rotate(new Vector3(0f, (_controller.Input.GetHorizontalInput() * _rotationSpeed * _rotationMultiplier), 0f),
                        Space.Self);

        //vertical character rotation
        _cameraVerticalAngle += _controller.Input.GetVerticalInput() * _rotationSpeed * _rotationMultiplier;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -89f, 89f);
        Camera.main.transform.localEulerAngles = new Vector3(_cameraVerticalAngle, 0, 0);
    }
}
