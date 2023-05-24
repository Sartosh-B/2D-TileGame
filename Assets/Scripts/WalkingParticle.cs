using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingParticle : MonoBehaviour
{
    ParticleSystem walkParticle;
    private void OnCollisionEnter2D(Collision2D other)
    {
        walkParticle.Play();
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        walkParticle.Stop();
    }
}
