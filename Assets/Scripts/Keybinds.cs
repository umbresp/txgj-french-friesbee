using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keybinds : MonoBehaviour
{
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public TextMeshProUGUI up, down, left, right, gamble;
    public SettingsManager settings;
    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Up", KeyCode.W);
        keys.Add("Down", KeyCode.S);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Gamble", KeyCode.G);
        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        gamble.text = keys["Gamble"].ToString();
        Debug.Log(settings.gambleToggled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GambleKeybindClick()
    {
        settings.toggleGambleSetting();
        gamble.text = settings.gambleToggled ? keys["Gamble"].ToString() : "None";
    }
}
