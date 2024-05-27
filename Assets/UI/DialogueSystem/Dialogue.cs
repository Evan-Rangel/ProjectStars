using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    public enum ImagePosition
    {
        RIGHT,
        LEFT,
        TOP,
        DOWN
    }
    [SerializeField] Image characterImageLeft;
    [SerializeField] Image characterImageRight;
    [SerializeField] Image backgroundImage;
    [SerializeField] TMP_Text dialogueText;
    public static Dialogue instance;
    int index = 0;
    char[] text;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PrintDialogue(Sprite _spr, ImagePosition _imgPos, string _key)
    {
        switch(_imgPos)
        { case ImagePosition.LEFT:
                characterImageLeft.gameObject.SetActive(true);
                characterImageRight.gameObject.SetActive(false);
                characterImageLeft.sprite = _spr;
                break; 
            case ImagePosition.RIGHT:
                characterImageLeft.gameObject.SetActive(false);
                characterImageRight.gameObject.SetActive(true);
                characterImageRight.sprite = _spr;
                break;
        }

        text= GameManager.instance.RequestDialogue(_key).ToCharArray();
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
[Serializable]
public class DialogueStruct
{
    public string key;
    public NextDialogueStatus status;
    public UnityEvent endDialogueEvent;
    public DialogueStruct(string _key, NextDialogueStatus _status, UnityEvent _unityEvent)
    { 
        key = _key;
        status = _status;
        endDialogueEvent = _unityEvent;
    }
}