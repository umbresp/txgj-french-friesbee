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
    // Start is called before the first frame update
    void Start()
    {
        origin = new Vector3(-17.59759f, 480f, 0.8791972f);
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
        while (Vector3.Distance(screenshot.transform.localPosition, destination) > 0)
        {
            currentMovementTime += Time.deltaTime;
            screenshot.transform.localPosition = Vector3.Lerp(origin, destination, currentMovementTime / totalMovementTime);
            yield return null;
        }
        SceneManager.LoadScene("RoomTest2");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
