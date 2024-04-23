using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Dialogue : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] string textToWrite;
    int index = 0;
    char[] text;

    private void Start()
    {
        //StarToWrite();
    }
    public void StarToWrite()
    {
        text= textToWrite.ToCharArray();
        dialogueText.text = "";
        index = 0;
        StartCoroutine(NextLetter());
    }
    IEnumerator NextLetter()
    {
        yield return new WaitForSeconds(0.05f);
        dialogueText.text += text[index]; 
        index++;
        if (index < text.Length)
        {
            StartCoroutine(NextLetter());
        }
    }
}
[Serializable]
public class DialogueData
{ 
    public string keyText;
    public Sprite charSprite;
    public Sprite backgroundSprite;

}