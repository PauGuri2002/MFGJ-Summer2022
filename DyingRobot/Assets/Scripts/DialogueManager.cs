using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text textObject;
    public GameObject dialogueBox;
    public float typingDelay = 0.08f;
    string[] text;
    int textIndex = 0;
    Coroutine writingCoroutine = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ShowDialogue(string[] textToWrite)
    {
        textIndex = 0;
        text = textToWrite;
        dialogueBox.SetActive(true);
        writingCoroutine = StartCoroutine(WriteText(text[0]));
    }

    private IEnumerator WriteText(string text)
    {
        char[] textArr = text.ToCharArray();
        textObject.text = "";

        while (textObject.text.Length < text.Length)
        {
            textObject.text += textArr[textObject.text.Length];
            yield return new WaitForSeconds(typingDelay);
        }
        textIndex++;
        writingCoroutine = null;
    }

    public void Interact()
    {
        if (!dialogueBox.activeSelf || text == null) { return; }

        if(writingCoroutine != null)
        {
            StopCoroutine(writingCoroutine);
            writingCoroutine = null;
            textObject.text = text[textIndex];
            textIndex++;
        } else if(textIndex < text.Length)
        {
            writingCoroutine = StartCoroutine(WriteText(text[textIndex]));
        } else
        {
            dialogueBox.SetActive(false);
        }
    }
}
