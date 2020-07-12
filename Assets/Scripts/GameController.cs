using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Text fireLabel;
    public Text needLabel;
    public Text zombieLabel;
    public int RequiredTrees = 0;
    public int liveTrees = 0;
    public int burningTrees = 0;
    public int zombies = 0;


    private PlayerController player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckCycle");
    }

    // Update is called once per frame
    void Update()
    {
        if (zombies == 0 && burningTrees <= 0 && RequiredTrees <= liveTrees) {
            Debug.Log("WON!");
        }
        else if (RequiredTrees > liveTrees || player.dead) {
            Debug.Log("Lost");
        }
    }

    private IEnumerator CheckCycle()
    {
        bool execute = true;
        while (execute)
        {
            UpdateMetrics();
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    private void UpdateMetrics()
    {
        burningTrees = 0;
        liveTrees = 0;
        zombies = GameObject.FindGameObjectsWithTag("Zombi").Length;
        Tree[] trees = GameObject.FindObjectsOfType<Tree>();
        foreach (Tree t in trees)
        {
            if (t.burning)
            {
                burningTrees++;
            }
            if (t.life > 10f)
            {
                liveTrees++;
            }
        }
        fireLabel.text = burningTrees.ToString();
        needLabel.text = $"{liveTrees} / {RequiredTrees}";
        zombieLabel.text = zombies.ToString();
    }
}
