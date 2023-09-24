using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public GameObject gameController;

    [SerializeField] AudioClip[] sounds;
    AudioSource coinAudioSource;

    private CoinCount coinCounter;
    private RoomManager roomManager;

    private Rigidbody2D rb;

    

    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coinCounter = gameController.GetComponent<CoinCount>();
        roomManager = gameController.GetComponent<RoomManager>();
        
        coinAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
{
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
        int floor = ((int) roomManager.levelNumber) / 5;
        coinCounter.gainCoins(5 * Mathf.Pow(2, floor));

        coinSounds();
    }
    
    void coinSounds()
    {
        AudioClip clip = sounds[UnityEngine.Random.Range(0, sounds.Length)];
        coinAudioSource.PlayOneShot(clip);
    }

    public void receiveDamage(float damage) {
        coinCounter.loseCoins(damage);
    }

}
