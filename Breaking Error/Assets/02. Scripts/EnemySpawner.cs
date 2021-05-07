using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab; // 생성할 적 프리팹
    public Transform[] spawnPoints; // 적을 소환할 위치

    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적을 담는 리스트

    private int enemyCount = 0;
    private int wave;

    void Start()
    {
        
    }

    void Update()
    {
        if(enemies.Count <= 0)
        {
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        wave++;

        // 현재 웨이브 * 2를 반올림한 수만큼 적 생성
        int spawnCount = Mathf.RoundToInt(wave * 2f);

        for (int i = 0; i < spawnCount; i++) // spawnCount만큼 적 생성
        {
            CreateEnemy();
        }
    }

    void CreateEnemy()
    {

    }
}
