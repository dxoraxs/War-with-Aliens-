using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer alinesImage;
    [SerializeField] private SpriteRenderer shipImage;
    private Sprite[] spritesShipDamage;

    private Vector3 targetMovement;
    private float speed;
    private int health;
    private int damageCastle;
    private int coinRandom;
    private EnemysSpawner enemysSpawner;

    private int countTravelPoint;

    public int GetDamageCastle => damageCastle;
    public float GetDistanceToNextPoint => Vector3.Distance(transform.position, targetMovement);
    public int GetRewardEnemy => coinRandom;

    public void InitializedEnemy(DataInstaller.Enemy enemy, EnemysSpawner enemysSpawner)
    {
        speed = enemy.speedMovement;
        health = enemy.health;
        damageCastle = enemy.damageCastle;
        alinesImage.sprite = enemy.alienImage;
        shipImage.sprite = enemy.imageShip;
        this.enemysSpawner = enemysSpawner;
        coinRandom = enemy.GetRandomReward;

        targetMovement = enemysSpawner.GetPointMovement(countTravelPoint);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            EnemyDied();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetMovement) < 0.1f)
        {
            countTravelPoint++;
            targetMovement = enemysSpawner.GetPointMovement(countTravelPoint);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetMovement, speed * Time.deltaTime);
    }

    private void EnemyDied()
    {
        enemysSpawner.EnemyDied(coinRandom);
        Destroy(gameObject);
    }
}
