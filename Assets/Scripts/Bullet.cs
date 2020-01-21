using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Transform target;
    private int damage;

    public void InitializedBullet(Transform target, int damage, float speed)
    {
        this.target = target;
        this.damage = damage;
        this.speed = speed;
    }

    private void Update()
    {
        if (target!=null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            target.GetComponentInParent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        }
        else if (speed != 0 && target == null)
        {
            Destroy(gameObject);
        }
        
    }
}
