using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*
     * Things to do:
     * 1- Enemy moves to player.
     * 2- Each wave more powerful enemies instantiate.
     * 3- Each wave shows with a number.
     * 4- Instantiation time is random.
     * 5- Some of the enemies are faster randomly.
     * 6- Three powerups instantianted: Health(Added on heart),
     * Speed(Added 5 units to speed and remains 10 second), 
     * Shield(Protect from 3 enemies and remains 10 second).
     * 
     * 
     * 
     *
     *
     */
    [SerializeField] private float speed = 1f;
    Rigidbody2D myRigidBody;
    private int health = 100;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {                
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(speed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-speed, 0f);
        }        
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {        
        // if bullet hit enemy dont turn back.
        if (collision.tag != "Ammo")
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
        }                        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}