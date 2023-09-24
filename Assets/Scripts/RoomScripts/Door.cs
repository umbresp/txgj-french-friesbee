using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int Side; //not designer friendly, but hey game jam, [top, left, bot, right]
    public Room NewRoom;
    public GameObject Wall;
    public GameObject EnterTrigger; //enternewroom
    public GameObject ExitTrigger; //loadnewroom

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = Wall.GetComponent<SpriteRenderer>();
    }

    public void DoorInit(Sprite spr) {
        Wall.SetActive(true);
        ExitTrigger.SetActive(false);
        EnterTrigger.SetActive(false);
        if (spr) {
            Wall.GetComponent<SpriteRenderer>().sprite = spr;
        }
    }

    //When you are leaving a room, this loads the next room
    public void Exit() {
        Wall.SetActive(false);
        ExitTrigger.SetActive(false);
        EnterTrigger.SetActive(true);
    }

    //When you are entering a room, this sets up that room
    public void Enter() {
        Wall.SetActive(false);
        ExitTrigger.SetActive(true);
        EnterTrigger.SetActive(false);
    }



}
