using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform pPoint;

    [SerializeField] float shootRange = 5.0f;
    [SerializeField] float OverlapRadius = 10.0f;
    [SerializeField] float _rotMultiplier;

    private Transform nearestEnemy;
    private int enemyLayer;
    private int airForceLayer;

    [SerializeField] float fireRateInit = 1f;
    [HideInInspector] public float currentFireRate;
    private float fireCountdown;

    [HideInInspector] public float damageAdd;

    [SerializeField] bool isAir;

    public int targetLayer;

    private void Start()
    {
        currentFireRate = fireRateInit;
        enemyLayer = LayerMask.NameToLayer("LandForce");
        airForceLayer = LayerMask.NameToLayer("AirForce");
    }
    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, OverlapRadius, 1 << enemyLayer | 1 << airForceLayer);
        float minimumDistance = Mathf.Infinity;
        foreach (Collider2D collider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < minimumDistance)
            {
                minimumDistance = distance;

                if (nearestEnemy == null)
                    nearestEnemy = collider.transform;

                if (nearestEnemy.gameObject.layer != targetLayer)
                    nearestEnemy = null;
            }
        }
        if (nearestEnemy != null && Vector3.Distance(transform.position, nearestEnemy.position) < shootRange && nearestEnemy.gameObject.activeSelf)
        {
            TurnToEnemy(nearestEnemy.position);

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1 / currentFireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
        else
        {
            nearestEnemy = null;
        }
    }

    private void Shoot()
    {
        GameObject clone = Instantiate(projectile, pPoint.position, Quaternion.identity);
        Projectile proj = clone.GetComponent<Projectile>();
        proj.damage += damageAdd;

        if (proj != null) proj.SetTarget(nearestEnemy);
    }

    private void TurnToEnemy(Vector2 position)
    {
        var direction = position - (Vector2)transform.position;
        var angle = Angle(direction, transform.up);
        transform.Rotate(0, 0, Mathf.Clamp(angle, -10, 10) * _rotMultiplier * Time.deltaTime);
    }

    // Reference
    public float Angle(Vector2 a, Vector2 b)
    {
        var an = a.normalized;
        var bn = b.normalized;
        var x = an.x * bn.x + an.y * bn.y;
        var y = an.y * bn.x - an.x * bn.y;
        return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
    }
}
