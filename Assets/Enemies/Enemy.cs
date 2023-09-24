using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10;
    public float damage = 5;
    public GameObject gameManager;
    
    private RoomManager roomManager;
    // Start is called before the first frame update
    void Start()
    {
        roomManager = gameManager.GetComponent<RoomManager>();
        roomManager.enemiesRemaining++;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            death();
        }
    }

    void death() {
        roomManager.enemiesRemaining--;
        Destroy(gameObject);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        Player enemyComponent = other.gameObject.GetComponent<Player>();
        if(enemyComponent){
            enemyComponent.receiveDamage(damage);
        }
    }
}
