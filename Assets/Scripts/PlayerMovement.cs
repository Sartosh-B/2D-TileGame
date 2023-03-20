using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;

    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float climbingSpeed = 1f;

    
    
    Rigidbody2D myRigdbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    PlayerInput input;
    float gravityScaleAtStart;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigdbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigdbody.gravityScale;

        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

   

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = MathF.Abs(myRigdbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigdbody.velocity.x), 1f);
        }       
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, myRigdbody.velocity.y);
        myRigdbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = MathF.Abs(myRigdbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);
       
    }

    private void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Debug.Log("nie taczing climbing");
            myRigdbody.gravityScale = gravityScaleAtStart;
            return;
        }
        Vector2 climbVelocity = new Vector2(myRigdbody.velocity.x, moveInput.y * climbingSpeed);
        myRigdbody.velocity = climbVelocity;
        myRigdbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = MathF.Abs(myRigdbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", playerHasVerticalSpeed);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();       
    }
    void OnJump(InputValue value)
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            Debug.Log("nie taczing");
            return;
        }
        if (value.isPressed)
        {         
            myRigdbody.velocity += new Vector2(0f, jumpSpeed); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        isAlive = false;
        input.DeactivateInput();
    }
}
