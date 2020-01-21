using System.Collections.Generic;
using UnityEngine;

public class EnemysSpawner : MonoBehaviour
{
	[SerializeField] private EnemyController prefabEnemy;
	[SerializeField] private DataInstaller dataInstaller;
	[SerializeField] private PointsMovement pointsMovement;
	[SerializeField] private Transform spawnPosition;
	[SerializeField] private GameController gameController;

	private float timeWave;
	private int numberWave;

	private float delaySpawnEnemy;
	private float timeSpawnNext;
	private float timePauseWave = 10;

	public Vector3 GetPointMovement(int id) => pointsMovement.GetPoint(id);
	public void EnemyDied(int reward)
	{
		gameController.TakeCoin(reward);
	}

	private void SpawnEnemy()
	{
		List<int> randomEnemy = new List<int>();

		for (int i = 0; i < dataInstaller.GetWaveIndex(numberWave-1).enemys.Length; i++)
		{
			if (dataInstaller.GetWaveIndex(numberWave - 1).enemys[i]) randomEnemy.Add(i);
		}

		Instantiate(prefabEnemy, spawnPosition.position, new Quaternion()).InitializedEnemy(dataInstaller.GetEnemyIndex(randomEnemy[Random.Range(0, randomEnemy.Count)]), this);
	}

	private void Update()
	{
		if (timeWave > 0)
		{
			timeWave -= Time.deltaTime;
			timeSpawnNext -= Time.deltaTime;

			if (timeSpawnNext < 0)
			{
				SpawnEnemy();
				timeSpawnNext = delaySpawnEnemy;
			}
			if (timeWave < 0)
			{
				timePauseWave = 10;
				if (numberWave >= dataInstaller.GetCountWaves)
				{
					gameController.SetResetGame();
				}
			}
		}
		else if (timePauseWave > 0)
		{
			timePauseWave -= Time.deltaTime;
			gameController.SetStringNumberWave((int)timePauseWave);
			if (timePauseWave < 0)
			{
				NextWave();
			}
		}
	}

	private void NextWave()
	{
		enabled = true;
		timeWave = dataInstaller.GetWaveIndex(numberWave).time;
		numberWave++;
		gameController.SetNumberWave(numberWave);
		delaySpawnEnemy = dataInstaller.delaySpawnEnemy;
	}
}
