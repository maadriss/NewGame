using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    Rigidbody2D myRigidBody;
    private int health = 100;
    GameSession gameSession;
    public bool enemyDies = false;

    // Get my color, If my color was yellow then set my health to 20;
    
    

    void Start()
    {
        if (GetComponent<Image>().color == Color.yellow)
        {
            Debug.Log("Yellow");
        }
        else { Debug.Log("not yellow"); }

        // Count enemies number for when all the enemies die and next wave should start
        
        gameSession = GameObject.FindObjectOfType<GameSession>();   
        gameSession.CountEnemies();
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