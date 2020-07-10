using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrossHair : MonoBehaviour
{
    public Camera cam;
    public float distance = 2f;
    private Vector3 newPosition;
    public Transform parent;
    private Vector3 cursorPosition;

    private void Update()
    {
        UpdateCrosshair();
        transform.position = newPosition;
    }

    private void UpdateCrosshair()
    {
        Vector3 diff = cam.ScreenToWorldPoint(cursorPosition) - parent.position;
        diff.z = parent.position.z;
        Vector3 position = Vector3.Normalize(diff);
        Debug.Log(position);
        newPosition = parent.position + new Vector3(position.x * distance, position.y * distance, 0f);
    }

    public void SetPosition(Vector3 direction)
    {
        cursorPosition = direction;
    }
}
