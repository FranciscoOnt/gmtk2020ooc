using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public CrossHair crossHair;
    public CrossHair waterOrigin;
    public float moveSpeed = 5f;
    public float waterDelay = .2f;
    private float waterTimer = 0f;
    public Transform water;
    private Vector2 velocity;
    private bool isShotingWater = false;
    private Rigidbody2D rb;

    void FixedUpdate()
    {
        rb.velocity = velocity * moveSpeed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator ShotWaterRoutine()
    {
        while (isShotingWater)
        {
            ShotWater();
            yield return new WaitForSeconds(waterDelay);
        }
        yield return null;
    }

    public void ShotWater()
    {
        Transform drop = Instantiate(water, waterOrigin.transform.position, Quaternion.identity);
        drop.GetComponent<Water>().SetVelocity(crossHair.GetCrosshaiPosition() - transform.position);
    }

    public void SetDirection(Vector2 direction)
    {
        velocity = direction;
    }

    public void SetWaterState(bool state)
    {
        isShotingWater = state;
        if (isShotingWater)
        {
            StartCoroutine("ShotWaterRoutine");
        }
    }

    public void SetCrosshairPosition(Vector2 direction)
    {
        crossHair.SetPosition(direction);
        waterOrigin.SetPosition(direction);
    }
}
