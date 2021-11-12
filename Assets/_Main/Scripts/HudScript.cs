using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudScript : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text waveText;

    public void UpdateMoney(int money)
    {
        moneyText.text = "Coin: " + money;
    }

    public void UpdateWave(int waveIndex)
    {
        waveText.text = "Wave " + waveIndex;
    }
}
