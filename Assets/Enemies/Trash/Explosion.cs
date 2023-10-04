using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime = 2f;
    public float explosiveDmg = 200f;
    public float explosiveKnockback = 3000f;
    [SerializeField] AudioClip[] sounds;
    AudioSource expAudioSource;

    private GameObject player;
    //might do the hashset thing
    HashSet<GameObject> explodedOn;

    private Collider2D coll;
    void Start()
    {
        expAudioSource = GetComponent<AudioSource>();
        AudioClip clip = sounds[UnityEngine.Random.Range(0, sounds.Length)];
        expAudioSource.PlayOneShot(clip);
        coll = GetComponent<Collider2D>();
        explodedOn = new HashSet<GameObject>();
    }

    public void SetPlayer(GameObject p) {
        player = p;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        //if (lifeTime <= 1.5f) {
        //    coll.enabled = false;
        //}
        if (lifeTime <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!explodedOn.Add(collision.gameObject)) { return; }
        Vector2 dir = (collision.transform.position - transform.position).normalized;
        collision.attachedRigidbody.AddForce(dir * explosiveKnockback);
        if (collision.gameObject.CompareTag("Player")) {
            player.GetComponent<Player>().receiveDamage(explosiveDmg);
        }
    }
}
