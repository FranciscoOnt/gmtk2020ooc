using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputWrapper : MonoBehaviour
{

    PlayerController controller;
    GameController gcon;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        gcon = FindObjectOfType<GameController>();
    }

    public void UpdateMovementInput(InputAction.CallbackContext c)
    {
        controller.SetDirection(c.ReadValue<Vector2>());
    }

    public void UpdateMouseLClick(InputAction.CallbackContext c)
    {
        if (c.started) controller.SetWaterState(true);
        else if (c.canceled) controller.SetWaterState(false);
    }

    public void UpdateMouseRClick(InputAction.CallbackContext c)
    {
        if (c.started) controller.AxeAttack();
    }

    public void UpdateEccKey(InputAction.CallbackContext c)
    {
        if (c.started) gcon.Pause();
    }

    public void UpdateCrosshairPosition(InputAction.CallbackContext c)
    {
        controller.SetCrosshairPosition(c.ReadValue<Vector2>());
    }
}
