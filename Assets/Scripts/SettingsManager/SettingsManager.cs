using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider narratorSlider;

    public bool gambleToggled = true;
    public bool narratorToggled = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleGambleSetting() {
        gambleToggled = !gambleToggled;
    }

    public void updateNarratorSetting() {
        narratorToggled = narratorSlider.value == 1;
    }
}
