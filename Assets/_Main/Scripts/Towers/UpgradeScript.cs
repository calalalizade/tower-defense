using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeScript : MonoBehaviour
{
    Cannon cannon;
    Button button;
    TMP_Text priceText;
    TMP_Text levelText;

    private int level = 1;
    [SerializeField] int maxLevel = 5;

    [SerializeField] int initialUpgradePrice;
    int upgradePrice;

    void Start()
    {
        upgradePrice = initialUpgradePrice;

        cannon = transform.parent.GetChild(0).GetComponent<Cannon>();
        button = transform.Find("UpgradeButton").gameObject.GetComponent<Button>();
        levelText = button.gameObject.transform.Find("Level").gameObject.GetComponent<TMP_Text>();
        priceText = button.gameObject.transform.Find("Price").gameObject.GetComponent<TMP_Text>();

        UpdateUI();
    }

    public void UpgradeTower()
    {
        if (upgradePrice < GameManager.Instance.GetMoney() && level <= maxLevel && GameManager.Instance.isStarted)
        {
            GameManager.Instance.UpgradeTurret(upgradePrice);
            upgradePrice += upgradePrice/2;
            level++;

            // Increase cannon power
            cannon.damageAdd += 5;
            cannon.currentFireRate++;

            UpdateUI();
            if(level == maxLevel)
            {
                priceText.text = "MAX";
                button.interactable = false;
            }
        }
        else
        {
            button.interactable = false;
        }
    }

    void UpdateUI()
    {            
        priceText.text = "$" + upgradePrice;
        levelText.text = level.ToString();
    }

    private void Update()
    {
        button.interactable = GameManager.Instance.isStarted;
    }
}
