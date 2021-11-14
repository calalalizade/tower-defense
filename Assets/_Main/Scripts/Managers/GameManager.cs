using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public GameObject hud;
    public GameObject gameOverScreen;
    public int InitialMoney;

    public int InitialTurretPrice;
    public int InitialRocketPrice;
    public int InitialS400Price;

    private int turretPrice;
    private int rocketPrice;
    private int s400Price;

    public int baseUpgradePrice;

    public float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float healthRestored; //upgraded base
     
    private int money;
    private HealthScript healthScript;
    private HudScript hudScript;
    private EndScreenElements endScreenElements;

    private int killCount;

    public bool isStarted;
    bool isUpgraded;

    void Start()
    {
        Time.timeScale = 1;

        money = InitialMoney;
        health = maxHealth;

        turretPrice = InitialTurretPrice;
        rocketPrice = InitialRocketPrice;
        s400Price = InitialS400Price;

        healthScript = GetComponent<HealthScript>();
        hudScript = GetComponent<HudScript>();
        endScreenElements = GetComponent<EndScreenElements>();

        hudScript.UpdateMoney(InitialMoney);
        hudScript.UpdateBaseUpgrade(baseUpgradePrice);
        healthScript.UpdateHealth(maxHealth);
    }

    public void EnemyReached(float damage)
    {
        health -= damage;
        healthScript.UpdateHealth(health);

        if (health <= 0)
        {
            GameOver();
        }
    }

    public void EnemyKilled()
    {
        killCount++;
        endScreenElements.UpdateKillCount(killCount);
    }
    public void TurretBuilt(GameObject turret)
    {
        if (turret.CompareTag("TurretTower"))
        {
            money -= turretPrice;
        }
        else if(turret.CompareTag("S-400"))
        {
            money -= s400Price;
        }
        else
        {
            money -= rocketPrice;
        }

        hudScript.UpdateMoney(money);
    }

    public void UpgradeTurret(int cost)
    {
        money -= cost;
        hudScript.UpdateMoney(money);
    }

    public bool EnoughMoneyForTurret(string tag)
    {
        if (tag == "TurretTower")
            return money >= turretPrice;

        if (tag == "S-400")
            return money >= s400Price;

        return money >= rocketPrice;
    }
    public int PriceForTurret(string tag)
    {
       // return tag == "TurretTower" ? turretPrice : rocketPrice;

        if (tag == "TurretTower")
            return turretPrice;

        if (tag == "S-400")
            return s400Price;

        return rocketPrice;
    }

    public void AddMoney(int value)
    {
        money += value;
        hudScript.UpdateMoney(money);
    }

    public void GainHealth()
    {
        health = health = Mathf.Clamp(health + health * 0.05f, 0, maxHealth);
        if (isUpgraded) health = Mathf.Clamp(health + healthRestored,0,maxHealth);

        healthScript.UpdateHealth(health);
    }

    public void BaseUpgraded()
    {
        if(money >= baseUpgradePrice && !isUpgraded)
        {
            isUpgraded = !isUpgraded;
            money -= baseUpgradePrice;
            hudScript.UpdateMoney(money);
            hudScript.HideUpgradeButton();
        }
    }
    public int GetMoney()
    {
        return money;
    }

    public void SetLastWaveIndex(int wave)
    {
        endScreenElements.UpdateLastWave(wave);
        hudScript.UpdateWave(wave);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        hud.SetActive(false);
        gameOverScreen.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
