using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject slash;
    public float range;
    public float size;
    public float timePerAttack;
    public float attackLifespan;
    public float attackDamage;

    private Rigidbody2D rb;

    public float horizontalInput;
    public float verticalInput;

    private float timeUntilAttack = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilAttack -= Time.deltaTime;
        if (timeUntilAttack <= 0.0) {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput != 0 || verticalInput != 0) {
                Vector3 playerPos = rb.transform.position;
                Vector3 playerDirection = rb.transform.right;
                Quaternion playerRotation = rb.transform.rotation;

                Vector3 spawnPos = playerPos + playerDirection*range;

                GameObject slashAttack = Instantiate(slash, spawnPos, playerRotation);
                slashAttack.GetComponent<SlashAttack>().init(attackLifespan, attackDamage);
                
                timeUntilAttack = timePerAttack;
            }
        }
        
    }
}
