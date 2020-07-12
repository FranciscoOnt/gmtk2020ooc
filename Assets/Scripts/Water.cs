using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float lifetime = 1f;
    private Vector2 velocity;
    public float moveSpeed = 5f;
    public float speedDamping = .9f;
    public ParticleSystem splash;
    private SpriteRenderer drop;
    private bool active = true;

    private void Awake()
    {
        drop = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {

        lifetime -= Time.deltaTime;
        if (lifetime < 0f && active)
        {
            drop.enabled = false;
            splash.Play();
            active = false;
            Destroy(gameObject, .2f);
        }
    }

    private void FixedUpdate()
    {
        if (velocity != Vector2.zero)
        {
            transform.Translate(velocity * Time.deltaTime * moveSpeed);
            velocity *= speedDamping;
        }
    }

    public void SetVelocity(Vector3 speed)
    {
        velocity = speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Stone"))
        {
            Splash();
        }

         if (other.transform.CompareTag("Tree"))
        {
            other.transform.GetComponent<Tree>().Water();
            Splash();
        }

        if (other.transform.CompareTag("Zombi"))
        {
            other.transform.GetComponent<Zombi>().Water();
            Splash(false);
        }
    }

    private void Splash(bool stop = true)
    {
        if (!stop)
            velocity = Vector3.zero;
        lifetime = 2f;
        drop.enabled = false;
        splash.Play();
        Destroy(gameObject, .3f);
    }
}
