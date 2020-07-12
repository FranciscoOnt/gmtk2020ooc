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

    public Text saveLabel;

    public GameObject MIssinBriefPanel;
    public GameObject FailurePanel;
    public Text reasonText;
    public int RequiredTrees = 0;
    public int liveTrees = 0;
    public int burningTrees = 0;
    public int zombies = 0;


    private PlayerController player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        saveLabel.text = $"Save at Least {RequiredTrees} trees";
        Time.timeScale = 0f;
        FailurePanel.SetActive(false);
    }

    public void StartLevel()
    {
        MIssinBriefPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckCycle");
    }

    // Update is called once per frame
    void Update()
    {
        if (zombies == 0 && burningTrees <= 0 && RequiredTrees <= liveTrees)
        {
            Debug.Log("WON!");
        }
        else if (RequiredTrees > liveTrees || !player)
        {
            reasonText.text = player.dead ? "You died!" : "You couldn't save the trees!";
            Time.timeScale = 0f;
            FailurePanel.SetActive(true);
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
