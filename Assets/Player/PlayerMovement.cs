using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Rigidbody2D rb;
    
    private float horizontalInput;
    private float verticalInput;

    public static bool move;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!move) { rb.velocity = Vector2.zero; return; }
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
 
        //Rotation for attack direction, not character sprites
	    if (rb.velocity != Vector2.zero) 
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        
    }


}
