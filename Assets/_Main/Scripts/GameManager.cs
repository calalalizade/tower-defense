using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    public static GameManager Instance;

    private void Awake()
    {
        Time.timeScale = 1;
        Instance = this;
    }
    #endregion

    public GameObject hud;
    public GameObject gameOverScreen;
    public int InitialMoney;

    public int InitialTurretPrice;
    public int InitialRocketPrice;

    private int turretPrice;
    private int rocketPrice;

    public float maxHealth;
    private float health;
     
    private int money;
    private HealthScript healthScript;
    private HudScript hudScript;
    private EndScreenElements endScreenElements;

    private int killCount;

    public bool isStarted;


    void Start()
    {
        money = InitialMoney;
        health = maxHealth;

        turretPrice = InitialTurretPrice;
        rocketPrice = InitialRocketPrice;

        healthScript = GetComponent<HealthScript>();
        hudScript = GetComponent<HudScript>();
        endScreenElements = GetComponent<EndScreenElements>();

        hudScript.UpdateMoney(InitialMoney);
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

        return money >= rocketPrice;
    }
    public int PriceForTurret(string tag)
    {
        return tag == "TurretTower" ? turretPrice : rocketPrice;
    }

    public void AddMoney(int value)
    {
        money += value;
        hudScript.UpdateMoney(money);
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
