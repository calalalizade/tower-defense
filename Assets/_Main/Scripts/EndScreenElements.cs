using UnityEngine;
using TMPro;

public class EndScreenElements : MonoBehaviour
{
    [SerializeField] TMP_Text killText;
    [SerializeField] TMP_Text waveIndexText;

    public void UpdateKillCount(int killCount)
    {
        killText.text = "Kill: " + killCount.ToString();
    }

    public  void UpdateLastWave(int waveIndex)
    {
        waveIndexText.text = "Wave: " + waveIndex.ToString();
    }
}
