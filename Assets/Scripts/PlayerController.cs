using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public CrossHair crossHair;
    public CrossHair waterOrigin;
    public float moveSpeed = 5f;
    public float waterDelay = .2f;
    public float waterConsumptionRate = .2f;
    public Transform water;
    public Transform axe;
    private Vector2 velocity;
    private bool isShotingWater = false;
    private Rigidbody2D rb;
    private float waterTank = 100f;
    public int life = 5;
    public float flinchDelay = .5f;
    public float flinchMultiply = 5f;
    private bool flinching = false;
    private bool attacking = false;

    private Transform axeAttack;

    void FixedUpdate()
    {
        if (!flinching)
        {
            if (attacking)
                rb.velocity = Vector3.zero;
            else
                rb.velocity = velocity * moveSpeed;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(510, 10, 150, 20), "Water: " + waterTank.ToString());
        GUI.Label(new Rect(510, 25, 150, 20), "Life: " + life.ToString());
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
        if (waterTank > 0)
        {
            if (attacking)
                return;
            waterTank -= waterConsumptionRate;
            Transform drop = Instantiate(water, waterOrigin.transform.position, Quaternion.identity);
            drop.GetComponent<Water>().SetVelocity(crossHair.GetCrosshaiPosition() - transform.position);
        }
        else
        {
            waterTank = 0f;
        }
    }

    public void AxeAttack()
    {
        if (attacking)
            return;
        attacking = true;
        axeAttack = Instantiate(axe, transform.position, Quaternion.identity);
        axeAttack.GetComponentInChildren<Axe>().SetController(this);
        RotateAxe();
    }

    public void FinishAttack()
    {
        attacking = false;
        Destroy(axeAttack.gameObject);
        axeAttack = null;
    }

    public void getWater(float amount)
    {
        waterTank += amount;
        if (waterTank > 100f)
            waterTank = 100f;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Tree"))
        {
            if (other.transform.GetComponent<Tree>().burning)
            {
                Damage();
                Vector3 flinchdir = Vector3.Normalize((Vector2)transform.position - (Vector2)other.transform.position);
                StartCoroutine("Flinch", flinchdir);
            }
        }
    }


    public IEnumerator Flinch(Vector3 direction)
    {
        flinching = true;
        rb.velocity = direction * flinchMultiply;
        yield return new WaitForSeconds(flinchDelay);
        flinching = false;
    }

    public void Damage()
    {
        life--;
    }

    // Credits to Kastenessen from https://answers.unity.com/questions/1350050/how-do-i-rotate-a-2d-object-to-face-another-object.html
    private void RotateAxe()
    {
        Vector3 targ = transform.position;
        targ.z = 0f;

        Vector3 objectPos = crossHair.transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        axeAttack.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }
}
