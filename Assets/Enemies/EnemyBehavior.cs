using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    [Header("Movement")]
    public float moveSpeed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        Vector3 displacement = player.transform.position -transform.position;
        displacement = displacement.normalized;
        if (Vector2.Distance (player.transform.position, transform.position) > 1.0f) {
            transform.position += (displacement * moveSpeed * Time.deltaTime);                        
        }
    }
}
