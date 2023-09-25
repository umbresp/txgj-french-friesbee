using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    [SerializeField]
    public GameObject dialogueCanvas;
    [SerializeField]
    public GameObject dialogueCharacter;

    private Queue<string> sentences;
    private bool isTyping;
    private string curSentence;

    private AudioSource[] aus;
    private int cur;
    // Start is called before the first frame update
    void Start()
    {
        aus = GetComponents<AudioSource>();

    }

    public void StartDialogueWithoutNarrator(Dialogue dialogue)
    {
        if (aus == null) aus = GetComponents<AudioSource>();
        Player.move = false;
        dialogueCanvas.SetActive(true);
        dialogueCharacter.SetActive(false);
        nameText.text = dialogue.characterName;
        sentences = new Queue<string>();
        isTyping = false;
        cur = 0;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (aus == null) aus = GetComponents<AudioSource>();
        Player.move = false;
        dialogueCanvas.SetActive(true);
        dialogueCharacter.SetActive(true);
        nameText.text = dialogue.characterName;
        sentences = new Queue<string>();
        isTyping = false;
        cur = 0;
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
        if (curSentence[0] == '!')
        {
            curSentence = sentence.Substring(1);
            cur = 1;
        }
        else
        {
            cur = 0;
        }
        // sentence = Regex.Replace(sentence, "<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>", "");
        StopAllCoroutines();
        StartCoroutine(TypeSentence(curSentence));
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
                if (!aus[cur].isPlaying) aus[cur].Play();
                Debug.Log("Played " + cur);
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
            if (!aus[cur].isPlaying) aus[cur].Play();
        }

        return currIndex + frontTag.Length + content.Length + backTag.Length - 1;
    }

    void EndDialogue()
    {
        dialogueCanvas.SetActive(false);
        if (!SettingsManager.gambleToggled && !SettingsManager.narratorToggled)
        {
            SceneManager.LoadScene("MainMenu");
        }
        Player.move = true; //bro LOL if this causes problems in the future im sorry - kenneth
    }

}
