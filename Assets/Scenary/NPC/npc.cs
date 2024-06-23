using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Events;
using System.Collections.Generic;

public class npc : Character
{
    public string dialogue;
    public string dialogueKey;
    //Utilizar un codigo identificador para conversaciones y que el index sea el contador de cada conversacion
    public string dialogueIdf;
    public int dialogueIndex;
    public Sprite spr;
    public LocalizedString testKey;
    //[SerializeField] DialogueDataContainer<DialogueData,CHARACTERS> dialoguesDADSD;
    public GUIContent ads;
    private void OnEnable()
    {
        testKey.Arguments = new[] { this };
       
    }

    public void PlayerInteract()
    {
        
    }
    //Se llama en el evento del script "PlayerInteract"

    public void DialogueInteract(ConversationData _conversation) 
    {
        Dialogue.instance.StartConversation(_conversation);
    }


}
