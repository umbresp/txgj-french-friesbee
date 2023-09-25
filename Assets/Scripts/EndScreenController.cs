using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    public Dialogue[] dialogues;
    private System.Random random;
    private DialogueManager dialogueManager;
    private bool dialogueStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        random = new System.Random();
        Invoke("DisplayLoseDialogue", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueStarted && Player.move)
        {
            SceneManager.LoadScene("RoomTest2");
        }
    }

    public void DisplayLoseDialogue()
    {
        dialogueManager.StartDialogue(dialogues[random.Next(0, dialogues.Length)]);
        dialogueStarted = true;
    }
}
