using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeartPickup : MonoBehaviour
{
    [SerializeField] AudioClip heartPickupSound;    
    [SerializeField] int livesForHeartPickup = 1;    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameSesion>().IncreseLife();
            HeartPick();            
        }
    }
    void HeartPick()
    {
        //AudioSource.PlayClipAtPoint(heartPickupSound, Camera.main.transform.position);
        gameObject.SetActive(false);
        Destroy(gameObject);        
    }
}
