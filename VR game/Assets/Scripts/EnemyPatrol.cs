using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform target;

    void Start()
    {
        target = pointA;
        transform.LookAt(target);
    }

    void Update()
    {
        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        
        // Check if the enemy has reached the target
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            // Switch the target to the other point
            target = (target == pointA) ? pointB : pointA;
            transform.LookAt(target);
        }
    }
}
