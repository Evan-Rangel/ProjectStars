using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization;
using UnityEngine;


public class npc : Character
{
    
    [SerializeField] DialogueData dialogueD;

    public string currentText;

    
    public void PlayerInteract()
    {
        //currentText= table.GetEntry("Hornet01").ToString();
    }
}
