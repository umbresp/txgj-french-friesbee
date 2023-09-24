using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider narratorSlider;
    public RectTransform settingsPanel;

    public bool gambleToggled = true;
    public bool narratorToggled = true;

    private bool setttingsArrive = false;
    private bool settingsReady = false;

    // Start is called before the first frame update
    void Start()
    {
        settingsPanel.localPosition = new Vector3(0, 1150, 0);
        bringInSettings();
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

    public void bringInSettings() {
        StopAllCoroutines();
        StartCoroutine(SettingsPanelEnters());
        setttingsArrive = true;
    }

    IEnumerator SettingsPanelEnters() {

        while (Vector3.Distance(settingsPanel.localPosition, Vector3.zero) > 5) {
            Vector3 interpPos = Vector3.Lerp(settingsPanel.localPosition, Vector3.zero, 0.05f);
            settingsPanel.localPosition = interpPos;
            
            if (Vector3.Distance(settingsPanel.localPosition, Vector3.zero) <= 5) {
                settingsPanel.localPosition = Vector3.zero;
            }
            yield return null;
        }
        settingsReady = true;
    }
}
