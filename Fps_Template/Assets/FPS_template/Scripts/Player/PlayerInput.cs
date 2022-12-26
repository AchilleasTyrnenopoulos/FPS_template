using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _lookSensitivity = 1f;

    public Vector3 GetMoveInput()
    {
        //if (CanProcessInput())
        {
            Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f,
                Input.GetAxisRaw("Vertical"));

            // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
            move = Vector3.ClampMagnitude(move, 1);

            return move;
        }

        //return Vector3.zero;
    }
    public float GetHorizontalInput()
    {
        float input = Input.GetAxis("Mouse X");
        input *= _lookSensitivity;

        return input;
    }

    public float GetVerticalInput()
    {
        float input = Input.GetAxis("Mouse Y");
        input *= -1f;
        input *= _lookSensitivity;

        return input;
    }
}


