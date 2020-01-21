using UnityEngine;
using UnityEngine.EventSystems;

public class TowerController : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Bullet prefab;

    private TowerCellManager cellManager;

    private Vector3 castlePosition;
    private int damage;
    private float delayShot;
    private float radius;
    private Transform targetEnemy;
    private float timeToNextShot = 0;
    private float speedBullet;

    public void OnPointerDown(PointerEventData eventData) => cellManager.SetPositionDestroyCursor(transform.position); 
    public void OnPointerUp(PointerEventData eventData) {}

    public void InitializedTower(DataInstaller.Tower tower, Vector3 castlePosition)
    {
        this.castlePosition = castlePosition;
        damage = tower.damage;
        delayShot = tower.delayShot;
        radius = tower.radius;
        GetComponentInChildren<SpriteRenderer>().sprite = tower.backgroundImage;
        speedBullet = tower.speedBullet;
    }

    private void Update()
    {
        if (targetEnemy != null)
        {
            if (Vector2.Distance(transform.position, targetEnemy.position) <= radius && timeToNextShot <= 0)
            {
                ShotBullet();
                timeToNextShot = delayShot;
            }
            else if (Vector2.Distance(transform.position, targetEnemy.position) > radius)
            {
                targetEnemy = null;
            }
        }
        else
        {
            targetEnemy = FindNewTarget();
        }
        if (timeToNextShot > 0)
        {
            timeToNextShot -= Time.deltaTime;
        }
    }

    private void ShotBullet() => Instantiate(prefab, transform.position, new Quaternion()).InitializedBullet(targetEnemy, damage, speedBullet);

    private Transform FindNewTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        float distance = int.MaxValue;
        Transform selectedTarget = null;
        foreach (Collider2D enemy in colliders)
        {
            float distanceEnemy = Vector2.Distance(enemy.transform.position, castlePosition);
            if (distance > distanceEnemy)
            {
                distance = distanceEnemy;
                selectedTarget = enemy.transform.parent;
            }
        }
        return selectedTarget;
    }

    private void Awake()
    {
        cellManager = transform.parent.GetComponent<TowerCellManager>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}