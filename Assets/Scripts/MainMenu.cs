using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.Image;

public class Quit : MonoBehaviour
{
    public GameObject screenshot;
    public GameObject trans;
    private float END_LOC = 6.417928f;
    private Vector3 origin;
    private Vector3 destination;
    private Vector3 offScreen = new Vector3(0, 1150, 0);
    public RectTransform creditsPanel;
    // Start is called before the first frame update
    void Start()
    {
        origin = new Vector3(-17.59759f, 480f, 0.8791972f);
        creditsPanel.localPosition = offScreen;
        destination = origin;
        destination.y = END_LOC;
        trans.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        trans.SetActive(true);
        StartCoroutine(moveObject());
    }

    public IEnumerator moveObject()
    {
        float totalMovementTime = 1f; //the amount of time you want the movement to take
        float currentMovementTime = 0f;//The amount of time that has passed
        while (Vector3.Distance(screenshot.transform.localPosition, destination) > 0.1f)
        {
            currentMovementTime += Time.deltaTime;
            screenshot.transform.localPosition = Vector3.Lerp(origin, destination, currentMovementTime / totalMovementTime);
            yield return null;
        }
        SceneManager.LoadScene("RoomTest2");
    }

    public void ShowCredits()
    {
        StopAllCoroutines();
        StartCoroutine(CreditsEnters());
    }

    IEnumerator CreditsEnters()
    {

        while (Vector3.Distance(creditsPanel.localPosition, Vector3.zero) > 5)
        {
            Vector3 interpPos = Vector3.Lerp(creditsPanel.localPosition, Vector3.zero, 0.05f);
            creditsPanel.localPosition = interpPos;

            if (Vector3.Distance(creditsPanel.localPosition, Vector3.zero) <= 5)
            {
                creditsPanel.localPosition = Vector3.zero;
            }
            yield return null;
        }
    }

    public void HideCredits()
    {
        Player.move = true;
        StopAllCoroutines();
        StartCoroutine(CreditsLeaves());

    }

    IEnumerator CreditsLeaves()
    {
        while (Vector3.Distance(creditsPanel.localPosition, offScreen) > 5)
        {
            Vector3 interpPos = Vector3.Lerp(creditsPanel.localPosition, offScreen, 0.05f);
            creditsPanel.localPosition = interpPos;

            if (Vector3.Distance(creditsPanel.localPosition, offScreen) <= 5)
            {
                creditsPanel.localPosition = offScreen;
            }
            yield return null;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
