using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public bool UseAccumulatingDifficulty = false;

    public float SpawnDifficultyInterval = 1f;
    public float SpawnDifficultyAccuAmount = 0.5f;
    public List<GameObject> Coins = new List<GameObject>();

    public float SpawnInterval = 0.2f;
    public float SpawnIntervalMin = 0.1f;

    private float _spawnTimer = 0f;
    private float _spawnDifficultyTimer = 0f;

    private float _spawnDifficultyAccu = 0f;

    private void Start()
    {
        _spawnTimer = SpawnInterval;
        _spawnDifficultyTimer = SpawnDifficultyInterval;
    }

    void Update()
    {
        DifficultyTimer();

        while (_spawnTimer > 0f)
        {
            _spawnTimer -= Time.deltaTime;
            return;
        }

        int randomPipe = UnityEngine.Random.Range(0, Coins.Count);

        float newSpawnInterval = SpawnInterval - _spawnDifficultyAccu;

        if (newSpawnInterval <= 0)
            newSpawnInterval = SpawnIntervalMin;

        if (UseAccumulatingDifficulty)
            _spawnTimer = newSpawnInterval;
        else
        {
            _spawnTimer = SpawnInterval;
        }

        Debug.Log("should spawn");

        GameObject.Instantiate(Coins[randomPipe], transform.position, Quaternion.identity);
    }

    void DifficultyTimer()
    {
        while (_spawnDifficultyTimer > 0f)
        {
            _spawnDifficultyTimer -= Time.deltaTime;
            // Debug.Log("Counting down");
            return;
        }

        Debug.Log("setting higher difficulty");

        _spawnDifficultyAccu += SpawnDifficultyAccuAmount;
        _spawnDifficultyTimer = SpawnDifficultyInterval;

    }
}
