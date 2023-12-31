using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    [Header("Movement")]
    public float moveSpeed;
    public static float speedMultiplier = 1f;

    public ParticleSystem gold;
    private Rigidbody2D rb;
    private Rigidbody2D playerRB;
    public bool activate;
    public bool ow;
    public bool noEXP;

    public event Action letTheRoomKnow;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
        moveSpeed *= speedMultiplier;
    }
    
    public static void IncreaseSpeed() {
        speedMultiplier *= 1.2f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!activate || ow) { return; }
        Vector3 dir = (playerRB.position - rb.position).normalized;
        if (moveSpeed != 0) { 
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
        }

        //Vector3 displacement = player.transform.position -transform.position;
        //displacement = displacement.normalized;
        //if (Vector2.Distance (player.transform.position, transform.position) > 1.0f) {
        //    transform.position += (displacement * moveSpeed * Time.deltaTime);                        
        //}
        if (moveSpeed != 0) { 
            if (Mathf.Sign(dir.x) == -1) {
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            } else {
                transform.rotation = Quaternion.identity;
            }
        }
     
    }

    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        if (Room.MostRecentLoadedRoom) {
            letTheRoomKnow?.Invoke();
        }
        if (!noEXP) { 
            Instantiate(gold.gameObject, transform.position, Quaternion.identity, transform.parent);
        }
    }

}
