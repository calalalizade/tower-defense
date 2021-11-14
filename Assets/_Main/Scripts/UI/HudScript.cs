using UnityEngine;
using TMPro;

public class HudScript : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text baseUpgPrice;

    public void UpdateMoney(int money)
    {
        moneyText.text = "Coin: " + money;
    }

    public void UpdateWave(int waveIndex)
    {
        waveText.text = "Wave " + waveIndex;
    }

    public void UpdateBaseUpgrade(int price)
    {
        baseUpgPrice.text = "Upgrade/$" + price;
    }

    public void HideUpgradeButton()
    {
        baseUpgPrice.transform.parent.gameObject.SetActive(false);
    }
}
