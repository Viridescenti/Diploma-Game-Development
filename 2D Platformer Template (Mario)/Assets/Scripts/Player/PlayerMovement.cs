using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    public SpriteRenderer sprite;
    public Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX;

    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float walkSpeed = 7f;
    [SerializeField] private float runSpeed = 12f;

    public bool isJumping;
    public bool blockToggle;

    public bool isFloating;

    Vector2 respawnPos;
    public bool isAlive;



    private enum MovementState { idle, jogging, jumping, falling, fallFlipping, death, running, dJumping, floating }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        respawnPos = transform.position;
        isAlive = true;
    }

    private void Update()
    {
        UpdateAnimationState();

        if(isAlive)
        {
            dirX = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector2(dirX * runSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(dirX * walkSpeed, rb.velocity.y);
            }
            


            if (Input.GetButtonDown("Jump") && (IsGrounded() || isFloating))
            {
                if (isFloating) 
                {
                    anim.Play("Jump");
                    isFloating = false;
                }
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                isJumping = true;
                
            }

            if (isJumping) 
            { 
                if (blockToggle)
                {
                    blockToggle = false;
                }
                else
                {
                    blockToggle = true;
                }
                isJumping = false;
            }

            if (isFloating)
            {
                rb.gravityScale = 0.7f;
            }
            else
            {
                rb.gravityScale = 2.7f;
            }

            if (IsGrounded())
            {
                isFloating = false;
            }

            if (Input.GetKey("r"))
            {
                StartCoroutine(Death());
            }

        }
      
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {          
            sprite.flipX = false;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                state = MovementState.running;
            }
            else
            {
                state = MovementState.jogging;
            }
                
        }
        else if (dirX < 0f)
        {
            sprite.flipX = true;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                state = MovementState.running;
            }
            else
            {
                state = MovementState.jogging;
            }
            
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.fallFlipping;
        }

        if (!isAlive)
        {
            state = MovementState.death;
        }

        if (isFloating)
        {
            state = MovementState.floating;
        }


        anim.SetInteger("state", (int)state); 

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Enemy")))
        {
            isAlive = false;
            isJumping = false;
            isFloating = false;
            StartCoroutine(Death());

        }
        if ((other.CompareTag("Spring")))
        {
            rb.velocity = new Vector2(rb.velocity.x, 18f);
        }

        if ((other.CompareTag("Super Spring")))
        {
            rb.velocity = new Vector2(rb.velocity.x, 12f);
            isFloating = true;
        }

    }
    private bool IsGrounded()
    {
       
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
   
    }
    //Checkpoints
    public void UpdateCheckpoint(Vector2 pos)
    {
        respawnPos = pos;
    }
    IEnumerator Death()
    {
        isAlive = false;
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;
        yield return new WaitForSeconds(1.2f);
        transform.position = respawnPos;      
        isAlive = true;
        rb.isKinematic = false;
          
    }
}


