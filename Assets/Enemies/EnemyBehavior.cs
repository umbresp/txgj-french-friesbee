using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    [Header("Movement")]
    public float moveSpeed;


    public ParticleSystem gold;
    private Rigidbody2D rb;
    private Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = (playerRB.position - rb.position).normalized;
        rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;

        //Vector3 displacement = player.transform.position -transform.position;
        //displacement = displacement.normalized;
        //if (Vector2.Distance (player.transform.position, transform.position) > 1.0f) {
        //    transform.position += (displacement * moveSpeed * Time.deltaTime);                        
        //}
        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Sign(dir.x) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void OnDestroy()
    {
        Instantiate(gold.gameObject, transform.position, Quaternion.identity, transform.parent);
        

    }

}
