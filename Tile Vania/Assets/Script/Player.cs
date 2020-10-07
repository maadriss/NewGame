using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// Bugs:
// 1- When level 2 starts then there's no coin until player dies.
// 2- In level 2 when player dies then ScenePersist don't work correctly.
// 3- When each level starts life is get started again.

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    CapsuleCollider2D myBody;
    BoxCollider2D myFeet;
    Animator anim;
    float first_gravity;
    bool isShoot = false;
    [SerializeField] float speed = 5f;
    [SerializeField] float jump_speed = 5f;
    [SerializeField] float climb_speed = 5f;
    [SerializeField] bool isAlive = true;
    [SerializeField] GameObject ammo;
    [SerializeField] GameObject start_position;
    RaycastHit2D[] rays;
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject ammoPrefab = Instantiate(ammo, transform.position + new Vector3(0.2f, 0, 0), Quaternion.identity);
            anim.SetBool("Shoot", true);
        }
    }
}