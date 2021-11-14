using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyBehavior : MonoBehaviour
{
    public float MaxHealth;
    public static float healthAdd;
    public float health;
    public int Money;

    public static float damageAdd;
    public float defaultDamage;
    public float damage;

    private Transform canvas;
    private Slider healthBar;

    CameraShake camShake;


    private void Start()
    {
        camShake = Camera.main.GetComponent<CameraShake>();
    }
    private void OnEnable()
    {
        canvas = transform.Find("Canvas");
        healthBar = canvas.Find("HealthBar").GetComponent<Slider>();
        canvas.gameObject.SetActive(false);

        health = MaxHealth;
        health += healthAdd;
        healthBar.maxValue = health;
        healthBar.value = health;

        damage = defaultDamage;
        damage += damageAdd;
    }

    private void OnDisable()
    {
        health = MaxHealth;
        damage = defaultDamage;
    }

    private void Update()
    {
        canvas.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            GameManager.Instance.EnemyReached(damage);
            Pooling.Instance.DeactivateObject(gameObject);
            EnemySpawner.Instance.activeEnemies.Remove(gameObject);

            camShake.Shk(.1f,.5f);
        }
        else if (collision.CompareTag("Projectile"))
        {
            // Take Damage
            float damage = collision.GetComponent<Projectile>().damage;
            health -= damage;
            healthBar.value = health;
            canvas.gameObject.SetActive(true);
            Destroy(collision.gameObject);

            // Enemy Died
            if (health <= 0)
            {
                GameManager.Instance.EnemyKilled();
                GameManager.Instance.AddMoney(Money);
                Pooling.Instance.DeactivateObject(gameObject);
                EnemySpawner.Instance.activeEnemies.Remove(gameObject);
            }
        }
    }


}
