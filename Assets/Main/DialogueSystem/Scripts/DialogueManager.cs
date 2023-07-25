using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string nameString;
        public string dialogueString;
        public string nameAnim;
    }
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text namesText;
    [SerializeField] Animator anim;

    [SerializeField] Dialogue[] dialogues;
    [SerializeField] WaveController waveController;
    PlayerInput input;
    //[SerializeField] Animator animator;
    string currentDialogue;
    int index;
    int stringIndex;
    bool dialogueManagerActive;
    private void Start()
    {
        input = GetComponent<PlayerInput>();
        index = 0;
        stringIndex = 0;
        dialogueManagerActive = true;
        // NextDialogue();
        StartCoroutine(StartDialogues());
        //animator = GetComponent<Animator>();
    }
  
    public void PressedNextDialogueButton()
    {
        if (dialogueManagerActive )
        {
            if (dialogueText.text == currentDialogue)
            {
                anim.SetBool(dialogues[index - 1].nameAnim, false);
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
            currentDialogue = dialogues[index].dialogueString;
            namesText.text = dialogues[index].nameString;
            anim.SetBool(dialogues[index].nameAnim, true);

            index++;

            stringIndex = 0;
            StartCoroutine(LetterPerLetter());
        }
        else
        {
            //animator.SetBool("EndDialogues", true);
            dialogueManagerActive = false;
            dialogueText.text = "";
            StartCoroutine(EndDialogues());
        }
    }
    IEnumerator StartDialogues()
    {
        //GameObject.FindGameObjectWithTag("Background").GetComponent<BackgroundControllerScript>().MultiplySpeed = 2;

        //anim.SetBool(dialogues[index].nameAnim, true);
        yield return new WaitForSeconds(0.2f);
        NextDialogue();
    }

    IEnumerator EndDialogues()
    {
        yield return new WaitForSeconds(0.2f);
        waveController.StartWaves();
        GameObject.FindGameObjectWithTag("Background").GetComponent<BackgroundControllerScript>().MultiplySpeed = 1;
        gameObject.SetActive(false);
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
