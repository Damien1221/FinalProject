using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int CoinAmount = 1;
    private ScoreManager _scoreManager;
    public GameObject DeathParticles;
   
    private void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_scoreManager == null)
            return;

        Debug.Log("Score collected");
        if (!col.gameObject.CompareTag("Player"))
            return;

        Destroy(gameObject);
        GameObject.Instantiate(DeathParticles, transform.position, transform.rotation);
        _scoreManager.AddScore(CoinAmount);
    }
      
}
