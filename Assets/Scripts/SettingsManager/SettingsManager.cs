using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private Vector3 offScreen = new Vector3(0, 1150, 0);

    public GameObject bgm;
    private AudioSource[] audioSources;
    private float vol;

    // Start is called before the first frame update
    void Start()
    {
        vol = 0.5f;
        settingsPanel.localPosition = offScreen;
        audioSources = bgm.GetComponents<AudioSource>();
        audioSources[0].volume = vol;
        audioSources[1].volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setVolume(float a)
    {
        vol = a / 2;
        audioSources[1].volume = vol;
    }

    public void toggleGambleSetting() {
        gambleToggled = !gambleToggled;
    }

    public void updateNarratorSetting() {
        narratorToggled = narratorSlider.value == 1;
    }

    public void bringInSettings() {
        audioSources[0].volume = 0;
        audioSources[1].volume = vol;
        StopAllCoroutines();
        StartCoroutine(SettingsPanelEnters());
        setttingsArrive = true;
        Player.move = false;
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

    public void bringOutSettings()
    {
        Player.move = true;
        StopAllCoroutines();
        StartCoroutine(SettingsPanelLeaves());
        setttingsArrive = false;
        audioSources[1].volume = 0;
        audioSources[0].volume = vol;
    }

    IEnumerator SettingsPanelLeaves()
    {
        settingsReady = false;
        while (Vector3.Distance(settingsPanel.localPosition, offScreen) > 5)
        {
            Vector3 interpPos = Vector3.Lerp(settingsPanel.localPosition, offScreen, 0.05f);
            settingsPanel.localPosition = interpPos;

            if (Vector3.Distance(settingsPanel.localPosition, offScreen) <= 5)
            {
                settingsPanel.localPosition = offScreen;
            }
            yield return null;
        }
    }
}
