using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterItem : MonoBehaviour
{
    public float amount = 5f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().getWater(amount);
            Destroy(gameObject);
        }
    }
}
