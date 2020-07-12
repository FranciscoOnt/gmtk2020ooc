using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject stageSelect;
    public GameObject howtoPlay;

    private void Start() {
        BacktoMain();   
    }

    public void BacktoMain()
    {
        mainMenu.SetActive(true);
        stageSelect.SetActive(false);
        howtoPlay.SetActive(false);
    }

    public void Stageselect()
    {
        mainMenu.SetActive(false);
        stageSelect.SetActive(true);
        howtoPlay.SetActive(false);
    }

    public void howtoPlaymenu()
    {
        mainMenu.SetActive(false);
        stageSelect.SetActive(false);
        howtoPlay.SetActive(true);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene($"stage{level.ToString()}");
    }

}
