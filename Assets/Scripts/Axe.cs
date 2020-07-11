using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private PlayerController controller;

    public void SetController(PlayerController ctrl)
    {
        controller = ctrl;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.SendMessage("AxeHit", SendMessageOptions.DontRequireReceiver);
    }

    public void FinishAnimation()
    {
        controller.FinishAttack();
        Destroy(gameObject);
    }
}
