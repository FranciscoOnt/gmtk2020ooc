using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float lifetime = 5f;
    public AudioSource sound;

    private void Start()
    {
        sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * 3f * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime < 0f)
        {
            Destroy(gameObject);
        }
    }
}
