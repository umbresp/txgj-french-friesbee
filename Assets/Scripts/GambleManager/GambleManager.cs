using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleManager : MonoBehaviour
{
    public RectTransform gambleScreen;
    public RectTransform titleScreen;
    // Start is called before the first frame update
    private bool slotsArrive = false;
    private bool titleArrive = false;
    private bool ready = false;

    void Start()
    {
        Vector3 slotsPos = gambleScreen.localPosition;
        gambleScreen.localPosition = new Vector3(slotsPos.x, 1080, slotsPos.z);
        bringInSlots();

        Vector3 titlePos = titleScreen.localPosition;
        titleScreen.localPosition = new Vector3(titlePos.x, -700, titlePos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (slotsArrive) {
            Vector3 interpPos = Vector3.Lerp(gambleScreen.localPosition, Vector3.zero, 0.05f);
            gambleScreen.localPosition = interpPos;
            if (Vector3.Distance(gambleScreen.localPosition, Vector3.zero) <= 10) {
                bringInTitle();
            }
            if (Vector3.Distance(gambleScreen.localPosition, Vector3.zero) <= 5) {
                gambleScreen.localPosition = Vector3.zero;
                slotsArrive = false;
            }
        }

        if (titleArrive) {
            Vector3 dest = new Vector3(0, -360, 0);
            Vector3 interpPos = Vector3.Lerp(titleScreen.localPosition, dest, 0.05f);
            titleScreen.localPosition = interpPos;
            if (Vector3.Distance(titleScreen.localPosition, dest) <= 5) {
                titleScreen.localPosition = dest;
                titleArrive = false;
                ready = true;
            }
        }
    }

    private void bringInSlots() {
        slotsArrive = true;
    }

    private void bringInTitle() {
        titleArrive = true;
    }
}
