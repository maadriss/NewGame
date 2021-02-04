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
    GameSession gameSession;
    public bool enemyDies = false;

    [SerializeField] uint enemies_number;
    // Get enemies number for other classes
    public uint EnemiesNumber { get { return enemies_number; } }
    void CountEnemies() 
    {
        enemies_number++;
    }
    

    void Start()
    {
        // Count enemies number for when all the enemies die and next wave should start
        CountEnemies();        

        gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
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
        // if bullet hit enemy dont turn back and player.
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
            enemyDies = true;
            print(enemyDies);
        }
    }
    public void Die()
    {
        gameSession.AddToScore(10);
        Destroy(gameObject, 0.1f);
    }
}