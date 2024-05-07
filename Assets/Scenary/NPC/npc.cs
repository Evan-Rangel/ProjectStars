using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

using UnityEngine.UI;

public class npc : Character
{
    public string dialogue;
    public string dialogueKey;
    //Utilizar un codigo identificador para conversaciones y que el index sea el contador de cada conversacion
    public string dialogueIdf;
    public int dialogueIndex;
    public Sprite spr;



    public void PlayerInteract()
    {
        
    }
    //Se llama en el evento del script "PlayerInteract"
    void DialogueInteract()
    {
        Dialogue.instance.PrintDialogue(spr, Dialogue.ImagePosition.LEFT, dialogueKey + dialogueIndex.ToString());
        dialogueIndex++;
    }
}
