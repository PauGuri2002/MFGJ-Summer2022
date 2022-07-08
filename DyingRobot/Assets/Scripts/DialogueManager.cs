using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text textObject;
    public GameObject dialogueBox;
    private AudioSource audioSource;
    public float typingDelay = 0.08f;
    DialoguePart[] dialogue;
    int dialogueIndex = 0;
    Coroutine writingCoroutine = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowDialogue(DialoguePart[] _dialogue)
    {
        dialogueIndex = 0;
        dialogue = _dialogue;
        dialogueBox.SetActive(true);
        writingCoroutine = StartCoroutine(WriteText(dialogue[0]));
    }

    private IEnumerator WriteText(DialoguePart dialoguePart)
    {
        char[] textArr = dialoguePart.dialogueText.ToCharArray();
        textObject.text = "";
        audioSource.clip = dialoguePart.dialogueSound;

        while (textObject.text.Length < dialoguePart.dialogueText.Length)
        {
            textObject.text += textArr[textObject.text.Length];
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();
            yield return new WaitForSeconds(typingDelay);
        }
        dialogueIndex++;
        writingCoroutine = null;
    }

    public void Interact()
    {
        if (!dialogueBox.activeSelf || dialogue == null) { return; }

        if(writingCoroutine != null)
        {
            StopCoroutine(writingCoroutine);
            writingCoroutine = null;
            textObject.text = dialogue[dialogueIndex].dialogueText;
            dialogueIndex++;
        } else if(dialogueIndex < dialogue.Length)
        {
            writingCoroutine = StartCoroutine(WriteText(dialogue[dialogueIndex]));
        } else
        {
            dialogueBox.SetActive(false);
        }
    }
}
