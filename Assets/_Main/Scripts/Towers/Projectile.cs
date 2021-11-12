using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    [SerializeField] float speed;
    public float damage;
    
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float DTF = speed * Time.deltaTime;

        if(dir.magnitude <= DTF)
            return;

        transform.Translate(dir.normalized * DTF, Space.World);
    }
}
