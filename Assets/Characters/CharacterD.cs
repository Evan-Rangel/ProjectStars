using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
[CreateAssetMenu(fileName = "Character Data", menuName = "Create Character Data")]
[Serializable]
public class CharacterD : ScriptableObject
{
 
    
    [HideInInspector] public string charName;
    [HideInInspector] public List<string> keys;
    [HideInInspector] public List<Conversation> conversations;
     public Dictionary<string, Conversation> dialogueStore = new Dictionary<string, Conversation>();

    [HideInInspector] public string currentConversationKey;

    [HideInInspector] public DialogueStruct tempDialogue;

    [HideInInspector] public int keyIndex;

    [HideInInspector] public class DialogueEndEvent : UnityEvent { }
    [HideInInspector] public DialogueEndEvent dialogueEndEvent;

    [HideInInspector] public string tempKey;
    [HideInInspector] public NexDialogueStatus tempStatus;

    [HideInInspector] public string dialogueToAdd;
    [HideInInspector] public string conversationKeyToCreate;

    public void InitializeDialogueStore()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (!dialogueStore.ContainsKey(keys[i]))
            {
                dialogueStore.Add(keys[i], conversations[i]);
            }
        }
    }
    //Crea conversacion en el inspector
    public void CreateConversation()
    {
        Conversation tempConv= new Conversation();
        conversations.Add(tempConv);
        keys.Add(conversationKeyToCreate);
        dialogueStore.Add(conversationKeyToCreate, tempConv);
        currentConversationKey = conversationKeyToCreate;
        conversationKeyToCreate = "";
        keyIndex = dialogueStore.Count - 1;
    }
    //Crea dialogo en el inspector
    public void CreateDialogue()
    {
        dialogueStore[currentConversationKey].dialogues.Add(tempDialogue);
        tempDialogue.key = "";
       // conversations[keyIndex].dialogues.Add(tempDialogue);
        //conversations = dialogueStore.Values.ToList<Conversation>();
        Debug.Log(conversations[keyIndex].dialogues.Count);
    }


}
public enum NexDialogueStatus 
{ 
    CONTINUE,
    OTHER_CONTINUE,
    PAUSE,
    END
}
[CustomEditor(typeof(CharacterD))]
[Serializable]
public class CharacterDataEditor : Editor 
{

    bool initializeStore=true;
    //SerializedProperty endDialogueEvent;
    //
    private void OnEnable()
    {
       // endDialogueEvent = serializedObject.FindProperty("endDialogueEvent");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CharacterD myTarget = (CharacterD)target;
        
        myTarget.charName=EditorGUILayout.TextField("Character Name ", myTarget.charName);
        if(initializeStore)
        {
            myTarget.InitializeDialogueStore();
            initializeStore=false;
        }



       
        GUILayout.BeginHorizontal();

        myTarget.conversationKeyToCreate = EditorGUILayout.TextField("Conversation Key ", myTarget.conversationKeyToCreate);
        if (GUILayout.Button("Create Conversation ") )
        {
            myTarget.CreateConversation();
        }
        GUILayout.EndHorizontal();




        //En este if a traves de un popup se setea la conversacion
        if (myTarget.keys.Count > 0)
        {
            myTarget.keyIndex = EditorGUILayout.Popup("Test ", myTarget.keyIndex, myTarget.keys.ToArray());
            myTarget.currentConversationKey = myTarget.keys[myTarget.keyIndex];
        }
        if (myTarget.keys.Contains(myTarget.currentConversationKey))
        {
            GUILayout.BeginHorizontal();
            myTarget.tempDialogue.key = EditorGUILayout.TextField("Dialogue Key ", myTarget.tempDialogue.key);
            myTarget.tempDialogue.status = (NexDialogueStatus)EditorGUILayout.EnumPopup("Dialogue Status: ", myTarget.tempDialogue.status);
            // myTarget.tempDialogue.endDialogueEvent = EditorGUILayout.PropertyField(endDialogueEvent, new GUIContent("Event Object"));
            //EditorGUILayout.PropertyField(this.serializedObject.FindProperty("dialogueEndEvent"), true);
            GUILayout.EndHorizontal();
            //Crea un dialogo si no existe
            if (GUILayout.Button("Create Dialogue") )
            {
                myTarget.CreateDialogue();
            }
        }
        if (GUILayout.Button("Reset Dialogues"))
        {
            myTarget.dialogueStore.Clear();
            myTarget.keys.Clear();
            myTarget.conversations.Clear();
        }


        string[] _tempKey = myTarget.dialogueStore.Keys.ToArray();

        for (int i = 0; i < myTarget.dialogueStore.Count; i++)
        {
            EditorGUILayout.LabelField(_tempKey[i] + "   "+myTarget.dialogueStore[_tempKey[i]].dialogues.Count.ToString());

            for (int j = 0; j < myTarget.conversations[i].dialogues.Count; j++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Dialogue " + j, GUILayout.Width(75));
                myTarget.conversations[i].dialogues[j].key = EditorGUILayout.TextField("Key",myTarget.conversations[i].dialogues[j].key, GUILayout.Width(800));
                myTarget.conversations[i].dialogues[j].status = (NexDialogueStatus)EditorGUILayout.EnumPopup("Status: ", myTarget.conversations[i].dialogues[j].status, GUILayout.Width(200));
                EditorGUILayout.EndHorizontal();

            }
            //myTarget.dialogueStore[temp[i]].dialogues


            //GUILayout.Toggle(false);

            // EditorGUILayout.TextField("Temp Dialogue ", temp[i]);
        }
        //
        /*
        for (int i = 0; i < myTarget.dialogueStore.Count; i++)
        {
            string _currentKey = keys[i];
            string _tempKey = EditorGUILayout.TextField("Conversation Key ", _currentKey);
            if (_currentKey!=_tempKey)
            {

            }
            GUILayout.BeginVertical();

            //EditorGUILayout.LabelField(keys[i]);
            Conversation _tempDialogues = myTarget.dialogueStore[keys[i]];
            EditorGUILayout.Popup("Test ", 0, _tempDialogues.dialogues.ToArray());

            for (int j = 0; j < _tempDialogues.dialogues.Count; j++)
            {
                EditorGUILayout.LabelField(_tempDialogues.dialogues[i]);
            }
            GUILayout.EndVertical();

        }*/
       // if (EditorGUI.EndChangeCheck())
        {
            // This code will unsave the current scene if there's any change in the editor GUI.
            // Hence user would forcefully need to save the scene before changing scene
         //   EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        //serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(myTarget);
       
        //AssetDatabase.SaveAssets();
        // AssetDatabase.Refresh();
        //
    }
}
