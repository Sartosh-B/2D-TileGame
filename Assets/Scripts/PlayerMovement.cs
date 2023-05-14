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
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    


    Rigidbody2D myRigdbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    //PlayerInput input;
    GameSesion gameSesion;

    float gravityScaleAtStart;
    bool isAlive = true;

    void Start()
    {
        myRigdbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigdbody.gravityScale;
        gameSesion = FindObjectOfType<GameSesion>();

        //input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();

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
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {         
            return;
        }
        if (value.isPressed)
        {
            myRigdbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void OnFire(InputValue value)
    {
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            Debug.Log("enemy touched");
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigdbody.velocity += deathKick;
            FindObjectOfType<GameSesion>().ProcessPlayerDeath();
        }
    }
}
