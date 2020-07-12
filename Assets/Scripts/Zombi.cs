using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombi : MonoBehaviour
{
    public float walkingSpeed = 3f;
    public float fireRate = 100f;
    public bool burning = true;
    public ParticleSystem fireEfect;
    public float burnRate = 1f;
    public float axePower = 20f;
    public Image lifebar;

    public Vector2 burnRateRange = new Vector2(0f, 1f);
    public Vector2 igniteRateRange = new Vector2(0f, 1f);
    public float burnMax = 149f;
    private Transform player;
    private Rigidbody2D rb;
    public Vector2 lifeRange;
    private float life = 1f;
    private float maxLife = 50f;
    public float flinchTime = 1f;
    private bool flinching = false;
    public ParticleSystem bleedEffect;
    private bool dead = false;
    public Transform waterDrop;
    public Transform spriteBody;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj)
            player = obj.transform;
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        maxLife = Random.Range(lifeRange.x, lifeRange.y);
        life = maxLife;
    }

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
        lifebar.fillAmount = life / maxLife;
        if (burning && fireRate <= burnMax)
        {
            fireRate += Time.deltaTime * burnRate;
            life -= Time.deltaTime;
        }

        if (burning && fireRate % 25f == 0f)
        {
            Collider2D[] burnarea = Physics2D.OverlapCircleAll(transform.position, 3f);
            foreach (Collider2D c in burnarea)
            {
                if (c.transform != transform)
                    c.SendMessage("Ignite", SendMessageOptions.DontRequireReceiver);
            }
        }


        var emission = fireEfect.emission;
        emission.rateOverTime = fireRate / 2;
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3();
        if (player)
            direction = Vector3.Normalize(player.position - transform.position);
        if (flinching || dead)
            direction = Vector3.zero;
        rb.velocity = direction * walkingSpeed;
        if (rb.velocity.x < 0f)
            spriteBody.localScale = new Vector3(-1f, 1f, 1f);
        else
            spriteBody.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Tree"))
        {
            Tree t = other.transform.GetComponent<Tree>();
            if (t.burning)
            {
                Ignite();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.CompareTag("Tree"))
        {
            Tree t = other.transform.GetComponent<Tree>();
            if (t.burning)
            {
                Ignite();
            }
        }
    }

    public void Ignite()
    {
        if (burning)
            return;
        burning = true;
        burnRate = Random.Range(burnRateRange.x, burnRateRange.y);
        fireRate = Random.Range(igniteRateRange.x, igniteRateRange.y);
    }

    public void Water()
    {
        if (fireRate > 0)
        {
            fireRate -= 2f;
        }
        else if (fireRate <= 0 && burning)
        {
            burning = false;
        }
        life--;
        if (life <= 0f)
            Death();
    }

    public void AxeHit()
    {
        StartCoroutine("Flinch");
        life -= axePower;
        if (life <= 0f)
            Death();
    }

    public IEnumerator Flinch()
    {
        Animator anim = spriteBody.GetComponent<Animator>();
        bleedEffect.Play();
        anim.enabled = false;
        flinching = true;
        yield return new WaitForSeconds(flinchTime);
        flinching = false;
        anim.enabled = true;
    }

    private void Death()
    {
        if (dead)
            return;
        dead = true;
        boxCollider.enabled = false;
        spriteBody.gameObject.SetActive(false);
        flinching = true;
        bleedEffect.Play();
        Transform w = Instantiate(waterDrop, transform.position, Quaternion.identity);
        Destroy(gameObject, .4f);
    }
}
