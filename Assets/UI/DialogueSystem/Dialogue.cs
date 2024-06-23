using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using System.Data;

public class Dialogue : MonoBehaviour
{

    private Dictionary<string, ConversationData> conversations;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text[] dialogueChoicesButtons;
    [SerializeField] Button[] buttonsChoices;
    [SerializeField] Animator anim;
    [SerializeField] float letterSpeed;
    float letterSpeedMultiplier=1;
    [SerializeField] UnityEvent closeConversation;
    [SerializeField] UnityEvent openConversation;

    private IEnumerator printDialogue;

    public static Dialogue instance;
    int index = 0;
    char[] text;
    bool choiceChoosed;
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
        GetAllConversations();
    }
    
    public void GetAllConversations()
    {
        conversations = new Dictionary<string, ConversationData>();
        foreach (ConversationData conversationD in Resources.FindObjectsOfTypeAll(typeof(ConversationData)))
        {
            conversations.Add(conversationD.conversationKey, conversationD);
        }
    }
   /* public void StarConversation(string _conversationKey)
    {

        if (conversations.ContainsKey(_conversationKey))
        {
            Debug.Log("cadadasdas");

            openConversation.Invoke();
            StartCoroutine(ConversationCicle(_conversationKey));
        }
    }*/
    public void StartConversation(ConversationData _conv)
    {
        openConversation.Invoke();
        StartCoroutine(ConversationCicle(_conv));
    }
    IEnumerator ConversationCicle(ConversationData _conv)
    {

        foreach (DialogueData _key in _conv.dialogues)
        {
            AudioManagerScript.instance.PlaySFX(_key.audioClip);

            printDialogue = PrintDialogue(_key.key.GetLocalizedString());
            StartCoroutine(printDialogue);

            yield return new WaitUntil(() => UserInput.instance.FireInput && dialogueText.text.Length > 0);
            letterSpeedMultiplier /= 50;

           
            yield return new WaitUntil(() => UserInput.instance.FireInput && dialogueText.text == text.ArrayToString());

            if (_key.dialogueChoices.Length > 0)
            {
                dialogueText.text = "";
                choiceChoosed = false;

                for (int i = 0; i < _key.dialogueChoices.Length; i++)
                {
                    buttonsChoices[i].gameObject.SetActive(true);

                   // buttonsChoices[i].onClick.AddListener(StartConversation(_key.dialogueChoices[i]));
                    //dialogueChoicesButtons[i].text = _key.dialogueChoices[i].GetLocalizedString();

                }
                
                yield return new WaitUntil(() => choiceChoosed);
            }
        }
        


        yield return new WaitUntil(() => UserInput.instance.FireInput);
        closeConversation.Invoke();
    }

    IEnumerator PrintDialogue(string _key)
    {
        //text = GameManager.instance.RequestDialogue(_key).ToCharArray();
        text = _key.ToCharArray();
        dialogueText.text = "";
        letterSpeedMultiplier = 1;
        foreach (char _char in text)
        {
            yield return (Helpers.WaitForSeconds(letterSpeed*letterSpeedMultiplier));
            dialogueText.text += _char;
        }

    }
}

