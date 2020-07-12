using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject stageSelect;
    public GameObject howtoPlay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene($"stage{level.ToString()}");
    } 

}
