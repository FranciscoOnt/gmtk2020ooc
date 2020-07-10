using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputWrapper : MonoBehaviour
{

    PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public void UpdateMovementInput(InputAction.CallbackContext c)
    {
        controller.SetDirection(c.ReadValue<Vector2>());
    }

    public void UpdateJumpInput(InputAction.CallbackContext c)
    {
        // if (c.started) playerController.UpdateJumpInput(true);
        // else if (c.canceled) playerController.UpdateJumpInput(false);
    }

    public void UpdateCrosshairPosition(InputAction.CallbackContext c)
    {
        controller.SetCrosshairPosition(c.ReadValue<Vector2>());
    }
}
