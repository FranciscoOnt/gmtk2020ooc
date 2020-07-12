using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public float shakeDuration = .6f;
    public Vector2 shakeIntensity;
    private Vector3 originalPosition;
    private Vector3 shakeOffset;
    private bool isShaking = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isShaking)
        {
            shakeOffset = new Vector3(Random.Range(shakeIntensity.x, shakeIntensity.y), Random.Range(shakeIntensity.x, shakeIntensity.y), 0f);
            transform.position = originalPosition + shakeOffset;
        }

    }

    public void Shake()
    {
        StartCoroutine("DoShake");
    }

    public IEnumerator DoShake()
    {
        isShaking = true;
        yield return new WaitForSeconds(shakeDuration);
        isShaking = false;
        transform.position = originalPosition;
    }
}
