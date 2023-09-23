using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCount : MonoBehaviour
{
    public float coins = 1000;
    public TMPro.TextMeshProUGUI coinCounter;
    // Start is called before the first frame update
    void Start()
    {
        coinCounter.text = coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loseCoins(float loss) {
        coins -= loss;
        updateCounter();
    }

    public void gainCoins(float gain) {
        coins += gain;
        updateCounter();
    }

    public void updateCounter() {
        coinCounter.text = coins.ToString();
    }
}
