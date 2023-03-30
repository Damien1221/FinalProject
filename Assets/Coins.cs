using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int CoinAmount = 1;
    private ScoreManager _scoreManager;

    private void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_scoreManager == null)
            return;

        Debug.Log("Score collected");

        Destroy(gameObject);

        _scoreManager.AddScore(CoinAmount);
    }
}
