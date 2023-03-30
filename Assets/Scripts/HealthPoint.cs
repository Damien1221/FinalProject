using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HealthPoint : MonoBehaviour
{
    public delegate void HitEvent(GameObject source);
    public HitEvent OnHit;

    public delegate void ResetEvent();
    public ResetEvent OnHitReset;
    
    public GameObject DeathParticles;
    


    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
    }

   public float _currentHealth = 10f;

    public bool _canDamage = true;

    public CoolDownn Invlarable;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        ResetInvulneble();
    }

    private void ResetInvulneble()
    {
        if (_canDamage)
            return;

        if (Invlarable.IsOnCooldown && _canDamage == false)
            return;

        _canDamage = true;
        OnHitReset?.Invoke();
    }

    public void Damage(float damageAmount, GameObject source)
    {
        if (!_canDamage)
            return;

        _currentHealth -= damageAmount;

        if (_currentHealth <= 0)
        {
            Die();
        }

        Invlarable.StartCoolDown();
        _canDamage = false;

        OnHit?.Invoke(source);
    }

    public void Die()
    {
        if(gameManager != null)
        {
            gameManager.iDie(this.gameObject);
        }

        GameObject.Instantiate(DeathParticles, transform.position, transform.rotation);
        Debug.Log("died");
        Destroy(this.gameObject);

       
    }
}
