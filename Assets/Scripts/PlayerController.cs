using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public CrossHair crossHair;
    public float moveSpeed = 5f;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (velocity != Vector2.zero)
        {
            transform.Translate(velocity * Time.deltaTime * moveSpeed);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        velocity = direction;
    }

    public void SetCrosshairPosition(Vector2 direction)
    {
        crossHair.SetPosition(direction);
    }
}
