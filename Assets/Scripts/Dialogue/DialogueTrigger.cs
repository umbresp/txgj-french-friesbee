using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;
    // Start is called before the first frame update
    private DialogueManager manager;
    void Start()
    {
        manager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerDialogue()
    {
        manager.StartDialogue(dialogue);
    }
}
