using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollector : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        
    }
}
