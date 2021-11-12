using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    public static float healthAdd;
    public float health;
    public int Money;

    public static float damageAdd;
    public float damage;

    private Transform canvas;
    private Slider healthBar;

    private void OnEnable()
    {
        canvas = transform.Find("Canvas");
        healthBar = canvas.Find("HealthBar").GetComponent<Slider>();
        canvas.gameObject.SetActive(false);

        health += healthAdd;
        healthBar.maxValue = health;
        healthBar.value = health;

        damage += damageAdd;
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
            Destroy(gameObject);
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
                Destroy(gameObject);
            }
        }
    }
}
