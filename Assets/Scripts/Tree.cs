using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public bool randomizeLife = true;
    public float fireRate = 100f;
    public bool burning = true;
    public ParticleSystem fireEfect;
    public ParticleSystem burstEffect;
    public float burnRate = 1f;

    public Vector2 burnRateRange = new Vector2(0f, 1f);
    public Vector2 igniteRateRange = new Vector2(0f, 1f);
    public float burnMax = 149f;

    public Sprite[] sprites = new Sprite[5];
    private SpriteRenderer rend;
    public float life = 1f;
    public Vector2 liferange;
    private Animator anim;
    public Transform waterText;
    // Start is called before the first frame update

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        if (randomizeLife)
            life = Random.Range(liferange.x, liferange.y);
        anim.enabled = false;
    }

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

        if (burning && fireRate > 1f && fireRate % 25 < 0.01f)
        {
            Collider2D[] burnarea = Physics2D.OverlapCircleAll(transform.position, 1.2f);
            foreach (Collider2D c in burnarea)
            {
                if (c.transform != transform)
                    c.SendMessage("Ignite", SendMessageOptions.DontRequireReceiver);
            }
        }

        if (burning && life > 0f)
            life -= Time.deltaTime;

        if (life <= 0f)
        {
            burning = false;
            fireRate = 0f;
        }

        UpdateSprite();

        var emission = fireEfect.emission;
        emission.rateOverTime = fireRate / 2;
    }

    public void Ignite()
    {
        if (burning || life <= 0f)
            return;
        burning = true;
        burnRate = Random.Range(burnRateRange.x, burnRateRange.y);
        fireRate = Random.Range(igniteRateRange.x, igniteRateRange.y);
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

    private void UpdateSprite()
    {
        if (life > 10f)
        {
            rend.sprite = sprites[burning ? 1 : 0];
        }
        else if (life > 0f && life <= 10f)
        {
            rend.sprite = sprites[burning ? 3 : 2];
        }
        else if (life <= 0f)
        {
            rend.sprite = sprites[4];
        }
    }

    private IEnumerator Die()
    {
        Collider2D c = GetComponent<BoxCollider2D>();
        c.enabled = false;
        anim.enabled = true;
        yield return new WaitForSeconds(.8f);
        Destroy(gameObject);
    }

    public void AxeHit()
    {
        PlayerController p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (burning)
        {
            burstEffect.Play();
            Collider2D[] burnarea = Physics2D.OverlapCircleAll(transform.position, 3f);
            foreach (Collider2D c in burnarea)
            {
                if (c.transform != transform)
                    c.SendMessage("Ignite", SendMessageOptions.DontRequireReceiver);
            }
            Vector3 flinchdir = Vector3.Normalize((Vector2)p.transform.position - (Vector2)transform.position);
            p.StartCoroutine("Flinch", flinchdir);
            p.Damage();
        }
        if (life > 10f)
        {
            Transform t = Instantiate(waterText, transform.position, Quaternion.identity);
            p.getWater(-10f);
        }
        life -= 5f;
        if (life <= 0f)
            StartCoroutine("Die");
    }
}
