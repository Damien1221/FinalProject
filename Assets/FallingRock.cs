using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public GameObject DeathParticles;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;

        Destroy(gameObject);
        GameObject.Instantiate(DeathParticles, transform.position, transform.rotation);
    }
}
