using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float Damage = 1f;

    public LayerMask TargetLayerMask;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;



        HealthPoint targetHealth = col.gameObject.GetComponent<HealthPoint>();

        if (targetHealth == null)
            return;

        targetHealth.Damage(Damage, transform.gameObject);
    }
}
