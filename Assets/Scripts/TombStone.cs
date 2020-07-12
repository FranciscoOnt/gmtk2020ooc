using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombStone : MonoBehaviour
{
    public Transform zombi;
    public Vector2 timetoSpawn;
    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        recalcTImer();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0f)
        {
            SpawnZombi();
            recalcTImer();
        }
    }

    private void SpawnZombi()
    {
        Transform z = Instantiate(zombi, transform.position, Quaternion.identity);
    }

    private void recalcTImer()
    {
        spawnTime = Random.Range(timetoSpawn.x, timetoSpawn.y);
    }
}
