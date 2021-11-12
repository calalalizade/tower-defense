using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private PathFollow path;
    private int wpIndex;

    // Start is called before the first frame update
    void Start()
    {
        path = GameObject.FindGameObjectWithTag("Path").GetComponent<PathFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, path.wayPoints[wpIndex].position, speed * Time.deltaTime);
        transform.right = Vector3.Lerp(transform.right, path.wayPoints[wpIndex].position - transform.position, 5f * Time.deltaTime);

        if ((transform.position - path.wayPoints[wpIndex].position).magnitude < 0.1f)
        {
            if (wpIndex < path.wayPoints.Length - 1)
            {
                wpIndex++;
            }
        }
    }
}
