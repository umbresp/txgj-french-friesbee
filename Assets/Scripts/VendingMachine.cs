using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{

    public GambleManager gamble;
    private float cd;
    private float cdMax = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cd >= 0) { cd -= Time.deltaTime; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!SettingsManager.gambleToggled) { return;}
        if (cd >= 0 || !collision.gameObject.CompareTag("Player")) { return; }
        //cancel player movement
        gamble.gameObject.SetActive(true);
        gamble.bringInSlots();
        Debug.Log("Machine");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (cd >= 0) { return; }
        cd = cdMax;
    }
}
