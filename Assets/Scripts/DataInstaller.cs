using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Art Controller")]
public class DataInstaller : ScriptableObject
{
    public PlayerSettings playerSettings;
    [SerializeField] private Enemy[] enemys;
    public float delaySpawnEnemy;
    [SerializeField] private Tower[] towers;
    [SerializeField] private Wave[] waves;
    
    [Serializable] public class Wave
    {
        public bool[] enemys;
        public int time;
    }

    [Serializable] public class PlayerSettings
    {
        public int startCoins;
        public int healthCastle;
    }

    [Serializable] public class Enemy
    {
        public Sprite alienImage;
        public Sprite imageShip;
        public int health;
        public int damageCastle;
        [Range(1, 10)]public float speedMovement;
        [SerializeField] private int leftBorderReward;
        [SerializeField] private int rightBorderReward;
        public int GetRandomReward => UnityEngine.Random.Range(leftBorderReward, rightBorderReward); 
    }
    private void OnValidate()
    {
        foreach(Wave wave in waves)
        {
            if (wave.enemys.Length != enemys.Length)
            {
                wave.enemys = new bool[enemys.Length];
            }
        }
    }

    [Serializable] public class Tower
    {
        public float speedBullet;
        public Sprite backgroundImage;
        public int damage;
        [Range(0.5f, 3f)]public float delayShot;
        public float radius;
        public int cost;
    }

    public Enemy GetEnemyIndex(int id)
    {
        if (id >= 0 && id < enemys.Length)
            return enemys[id];
        return null;
    }

    public Tower GetTowerIndex(int id)
    {
        if (id >= 0 && id < towers.Length)
            return towers[id];
        return null;
    }

    public int GetCountWaves => waves.Length;
    public Wave GetWaveIndex(int id)
    {
        if (id >= 0 && id < waves.Length)
            return waves[id];
        return null;
    }
}
