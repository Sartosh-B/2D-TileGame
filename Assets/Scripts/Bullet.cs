using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float bulletspeed = 5f;
    Rigidbody2D myRigidbody2D;
    PlayerMovement player;
    float xSpeed;


    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletspeed;
    }

   
    void Update()
    {
        myRigidbody2D.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }        
        Destroy(gameObject);           
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       Destroy(gameObject);
    }
}
