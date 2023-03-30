using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    protected bool _isTrigger = false; 
   
    // Update is called once per frame
    protected override void HandleInput()
    {
        if (_isTrigger)
            _InputDirection = Vector2.left;
        else
            _InputDirection = Vector2.right;


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Contains("EnemyFliper"))
        {
            _isTrigger = !_isTrigger;
            


        }


    }
}
