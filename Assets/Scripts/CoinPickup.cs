using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSound;    
    [SerializeField] int pointsForCoinPickup = 100;      

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CoinPick();
            FindObjectOfType<GameSesion>().AddToScore(pointsForCoinPickup);
        }
    }
    void CoinPick()
    {
        AudioSource.PlayClipAtPoint(coinPickupSound, Camera.main.transform.position);
        gameObject.SetActive(false);
        Destroy(gameObject);        
    }
}
