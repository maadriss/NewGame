using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shoot the bullet by velocity of it.
/// </summary>
public class Ammo : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    public GameObject firePoint;
    public Rigidbody2D rigidBody;
    private int damage = 40;
    void Start()
    {
        rigidBody.velocity = transform.right * 10;
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (_boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Ground", "Hazards")))
        {
            Destroy(gameObject);
        }
    }

    // If bullet or ammo hits enemy then destroy the bullet and damage the enemy.
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy!=null)
        {
            enemy.TakeDamage(damage);
        }
    }
}