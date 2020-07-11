using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public float fireRate = 100f;
    public bool burning = true;
    public ParticleSystem fireEfect;
    public float burnRate = 1f;

    public Vector2 burnRateRange = new Vector2(0f, 1f);
    public Vector2 igniteRateRange = new Vector2(0f, 1f);
    public float burnMax = 149f;
    private float burnAmount = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if (burning)
        {
            burnRate = Random.Range(burnRateRange.x, burnRateRange.y);
            fireRate = Random.Range(igniteRateRange.x, igniteRateRange.y);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (burning && fireRate <= burnMax)
        {
            fireRate += Time.deltaTime * burnRate;

        }

        if (fireRate % 25f == 0f)
        {
            Collider2D[] burnarea = Physics2D.OverlapCircleAll(transform.position, 3f);
            foreach (Collider2D c in burnarea)
            {
                if (c.transform != transform)
                    c.SendMessage("Ignite");
            }
        }


        var emission = fireEfect.emission;
        emission.rateOverTime = fireRate / 2;
    }

    public void Ignite()
    {
        if (burning)
            return;
        burning = true;
        burnRate = Random.Range(.8f, 5f);
        fireRate = Random.Range(20f, 40f);
    }

    public void Water()
    {
        if (fireRate > 0)
        {
            fireRate--;
        }
        else if (fireRate <= 0 && burning)
        {
            burning = false;
        }
    }
}
