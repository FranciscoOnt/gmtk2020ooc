using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterUI : MonoBehaviour
{
    public PlayerController player;
    public Image fill;
    public Image hpfill;

    // Update is called once per frame
    void Update()
    {
        fill.fillAmount = player.waterTank / 100f;
        hpfill.fillAmount = player.life / 5f;
    }
}
