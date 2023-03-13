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
    Rigidbody2D myRigdbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        myRigdbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        
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

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        
    }
    void OnJump(InputValue value)
    {
        if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            Debug.Log("nie taczing");
            return;
        }
        if (value.isPressed)
        {         
            myRigdbody.velocity += new Vector2(0f, jumpSpeed); 
        }
    }
}
