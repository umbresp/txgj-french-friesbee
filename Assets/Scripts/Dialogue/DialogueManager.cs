using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    [SerializeField]
    public GameObject dialogueCanvas;

    private Queue<string> sentences;
    private bool isTyping;
    private string curSentence;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueCanvas.SetActive(true);
        nameText.text = dialogue.characterName;
        sentences = new Queue<string>();
        isTyping = false;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void TryDisplayNextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = curSentence;
            isTyping = false;
        }
        else
        {
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        curSentence = sentence;
        // sentence = Regex.Replace(sentence, "<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>", "");
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        for (int index = 0; index < sentence.Length; ++index)
        {
            char letter = sentence[index];
            if (letter == '<')
            {
                index = processTaggedLines(sentence, index);
            }
            else
            {
                dialogueText.text += letter;
                // if (!textSound.isPlaying)
                //     textSound.Play();
            }
            if (dialogueText.text == sentence)
            {
                isTyping = false;
            }
            yield return null;
        }
    }

    private int processTaggedLines(string sentence, int currIndex)
    {
        // Processes tags so that the tags themselves don't auto-fill with the scrolling text.
        int frontTagEndIndex = sentence.IndexOf(">", currIndex) + 1;
        string frontTag = sentence.Substring(currIndex, frontTagEndIndex - currIndex);

        string backTag = frontTag[0] + "/" + frontTag.Substring(1);
        string content = sentence.Substring(frontTagEndIndex, sentence.IndexOf(backTag, frontTagEndIndex) - frontTagEndIndex);

        dialogueText.text += frontTag + backTag;
        int newIndex = dialogueText.text.Length - backTag.Length;

        int count = 0;
        for (; count < content.Length; ++newIndex, ++count)
        {
            dialogueText.text = dialogueText.text.Insert(newIndex, content[count] + "");
        }

        return currIndex + frontTag.Length + content.Length + backTag.Length - 1;
    }

    void EndDialogue()
    {
        dialogueCanvas.SetActive(false);
    }

}
