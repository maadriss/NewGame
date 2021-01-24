using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator anim;

    public static Player _player;
    
    CapsuleCollider2D myBody;
    BoxCollider2D myFeet;
    
    float first_gravity;
    float nextFire = 0, firerate = 0.3f;
    bool isShoot = false;
    [SerializeField] float speed = 5f;
    [SerializeField] float jump_speed = 5f;
    [SerializeField] float climb_speed = 5f;
    [SerializeField] bool isAlive = true;
    [SerializeField] GameObject ammo;
    [SerializeField] GameObject start_position;
    RaycastHit2D[] rays;

    private void Awake()
    {
        _player = this;
    }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myBody = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        first_gravity = rigidbody2d.gravityScale;
        // Sets the start position of player to the start_position gameobject.
        transform.position = start_position.transform.position;
    }
    void Update()
    {        
        if (!isAlive) { return; }
        Run();
        Die();
        Flip_Player();
        Jump();
        Climb_Ladder();
        Shoot();
    }

    private void Run()
    {
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        rigidbody2d.velocity = new Vector2(horizontal * speed, rigidbody2d.velocity.y);
    }

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            rigidbody2d.velocity += new Vector2(0f, jump_speed);
        }
    }

    private void Flip_Player()
    {
        bool isPlayerRunning = Mathf.Abs(rigidbody2d.velocity.x) > Mathf.Epsilon;
        anim.SetBool("Run", isPlayerRunning);
        if (isPlayerRunning)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2d.velocity.x), 1f);
        }
    }

    private void Climb_Ladder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rigidbody2d.gravityScale = first_gravity;
            anim.SetBool("Climb", false);
            return;
        }
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbLadder = new Vector2(rigidbody2d.velocity.x, vertical * climb_speed);
        rigidbody2d.velocity = climbLadder;
        rigidbody2d.gravityScale = 0f;
        bool vertical_move = Mathf.Abs(rigidbody2d.velocity.y) > Mathf.Epsilon;
        anim.SetBool("Climb", vertical_move);
    }

    private void Die()
    {
        if (myBody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            anim.SetTrigger("Dying");
            rigidbody2d.velocity = new Vector2(5f, 10f);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void Shoot()
    {
        bool c_down = false;
        if (Input.GetKeyDown(KeyCode.C) && Time.time > nextFire)
        {
            nextFire = Time.time + firerate;
            c_down = true;
            Instantiate(ammo, transform.position + new Vector3(0.2f, -0.262f, 0), Quaternion.identity);
            anim.SetBool("Shoot", c_down);
        }
        else
        {
            c_down = false;
            anim.SetBool("Shoot", c_down);
        }
    }
}