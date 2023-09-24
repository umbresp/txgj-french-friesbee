using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    public float moveSpeed = 0.75f;
    private float lifespan;
    private float timeRemaining;

    private float damage;
    private float knockbackF;

    private HashSet<GameObject> hits = new HashSet<GameObject>();

    public void init(float attackLifespan, float attackDamage, float knockbackForce) 
    {
        lifespan = attackLifespan;
        damage = attackDamage;
        knockbackF = knockbackForce;
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

        Vector3 direction = transform.right.normalized * moveSpeed;
        transform.position += direction * Time.deltaTime;
        
        if (timeRemaining <= 0.0f)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            Debug.Log("Hey!");
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            other.attachedRigidbody.AddForce(transform.right.normalized * knockbackF);
            
        }
    }
}
