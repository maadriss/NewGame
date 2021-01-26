using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shooting the bullet.
/// </summary>
public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject ammo;
    public Animator anim;

    private float nextFire = 0;
    private float firerate = 0.3f;

    void Shoot()
    {
        if (Input.GetKey(KeyCode.C) && Time.time > nextFire)
        {
            nextFire = Time.time + firerate;
            Instantiate(ammo, firePoint.transform.position, firePoint.transform.rotation);
            anim.SetBool("Shoot", true);
        }
        else
        {
            anim.SetBool("Shoot", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
}