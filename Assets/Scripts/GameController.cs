using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private DataInstaller dataInstaller;
    [SerializeField] private GUIManager guiManager;

    private int playerCoin;
    private int healthCastle;

    public void TakeCoin(int count)
    {
        playerCoin += count;
        SetCoin();
    }

    public void SetResetGame()
    {
        guiManager.ShowRestartPanel();
        Time.timeScale = 0;
    }

    public void TakeDamage(int damage)
    {
        healthCastle -= damage;
        if (healthCastle <= 0)
        {
            healthCastle = 0;
            SetResetGame();
        }
        SetHealth();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            Destroy(collision.transform.parent.gameObject);
            TakeDamage(enemy.GetDamageCastle);
        }
    }

    private void SetHealth() => guiManager.SetTextHealth(healthCastle); 
    private void SetCoin() => guiManager.SetTextCoin(playerCoin); 
    public void SetNumberWave(int number) => guiManager.SetTextNumberWave(number, dataInstaller.GetCountWaves);
    public void SetStringNumberWave(int timer) => guiManager.SetTextNumberWaveTimer("Пауза... " + timer);

    public bool TowerIsBuying(int costTower)
    {
        if (playerCoin < costTower)
        {
            return false;
        }

        playerCoin -= costTower;
        SetCoin();
        return true;
    }
    
    private void Awake()
    {
        playerCoin = dataInstaller.playerSettings.startCoins;
        healthCastle = dataInstaller.playerSettings.healthCastle;

        SetCoin();
        SetHealth();
    }
}
