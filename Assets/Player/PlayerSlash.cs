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
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 playerToMouseDirection = (mousePosition - transform.position);

            Vector3 playerPos = rb.transform.position;
            Vector3 playerToMouseFlat = playerToMouseDirection;
            playerToMouseFlat.z = 0;
            Vector3 playerDirection = playerToMouseFlat.normalized;
            Debug.Log(playerDirection);

            float zRotation = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            Quaternion attackRotation = Quaternion.Euler(0, 0, zRotation);

            Vector3 spawnPos = playerPos + playerDirection*range;

            GameObject slashAttack = Instantiate(slash, spawnPos, attackRotation);
            slashAttack.GetComponent<SlashAttack>().init(attackLifespan, attackDamage);
                
            timeUntilAttack = timePerAttack;
        }
        
    }
}
