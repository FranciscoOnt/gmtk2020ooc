using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterItem : MonoBehaviour
{
    public float amount = 5f;
    public Vector2 amountRange;
    public Sprite[] sprites = new Sprite[3];
    private SpriteRenderer r;

    private void Awake()
    {
        r = GetComponent<SpriteRenderer>();
        amount = Random.Range(amountRange.x, amountRange.y);
        if (amount < 15f)
            r.sprite = sprites[0];
        if (amount >= 15f && amount < 35f)
            r.sprite = sprites[1];
        if (amount >= 35f)
            r.sprite = sprites[2];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().getWater(amount);
            Destroy(gameObject);
        }
    }
}
