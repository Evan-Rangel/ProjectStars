using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] string[] dialogues;
    string currentDialogue;
    int index;
    int stringIndex;
    bool dialogueManagerActive;
    private void Start()
    {
        index = 0;
        stringIndex = 0;
        dialogueManagerActive = true;
        NextDialogue();
    }
    private void Update()
    {
        if (dialogueManagerActive && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueText.text == currentDialogue)
            {
                NextDialogue();
            }
            else
            {
                StopCoroutine(LetterPerLetter());
                dialogueText.text = currentDialogue;
                stringIndex = currentDialogue.Length;
            }
        }
    }
    public void NextDialogue()
    {
        if (index<dialogues.Length)
        {
            dialogueText.text = "";
            currentDialogue = dialogues[index];
            index++;
            stringIndex = 0;
            StartCoroutine(LetterPerLetter());
        }
        else
        {
            dialogueManagerActive = false;
            dialogueText.text = "";
        }
    }
    IEnumerator LetterPerLetter()
    {
        if (stringIndex < currentDialogue.Length)
        {
            dialogueText.text += currentDialogue[stringIndex];
            yield return new WaitForSeconds(0.05f);
            stringIndex++;
            StartCoroutine(LetterPerLetter());
        }
    }
}
