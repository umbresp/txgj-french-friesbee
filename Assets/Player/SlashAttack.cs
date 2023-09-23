using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    
    private float lifespan;
    private float timeRemaining;

    private float damage;

    private HashSet<GameObject> hits = new HashSet<GameObject>();

    public void init(float attackLifespan, float attackDamage) 
    {
        lifespan = attackLifespan;
        damage = attackDamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        Enemy enemyComponent = other.gameObject.GetComponent<Enemy>();
        if(enemyComponent){
            if (!hits.Contains(other.gameObject)) {
                enemyComponent.health -= damage;
                hits.Add(other.gameObject);
            }
        }
    }
}
