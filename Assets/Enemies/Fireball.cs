using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float fireballSpeed = 1f;
    public float damage = 2f;
    private Player player; //LOL
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetPlayer(Player p) {
        player = p;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.right * fireballSpeed;
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) { return; }
        if (collision.gameObject.CompareTag("Player")) {
            player.receiveDamage(damage);
        }
        Destroy(gameObject);
    }
}
