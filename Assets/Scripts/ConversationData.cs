using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.Animations;
using UnityEngine.Localization;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Conversation Data", menuName = "Create Conversation Data")]
[Serializable]
public class ConversationData : ScriptableObject
{
    public string conversationKey;
    public List<DialogueData> dialogues = new List<DialogueData>();

}
[Serializable]
public class DialogueData
{
    public LocalizedString key;
    public CHARACTERS character;
    public AudioClip audioClip;
    public AnimationClip animation;
    public UnityEvent[] eventAct;
    public ConversationData[] dialogueChoices;
    //public ConversationData nextConversation;
}
[Serializable]
public class DialogueDataContainer<tValue>//, tKey> where tKey : Enum
{
    //[SerializeField] private tValue[] content=null;
    [SerializeField] private tValue[] content;
    //[SerializeField] private tKey key;
    public tValue this[int i]
    {
        get { return content[i]; }
    }
    public int Lenght { get {return content.Length; } }
}
