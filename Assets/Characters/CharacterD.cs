using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName = "Character Data", menuName = "Create Character Data")]
[Serializable]
public class CharacterD : ScriptableObject
{
    [HideInInspector] public string charName;
    [HideInInspector] public List<string> keys;
    [HideInInspector] public List<DialogueStruct> dialogues;
    public Dictionary<string, DialogueStruct> dialoguesMap = new Dictionary<string, DialogueStruct>();

    [HideInInspector] public int keyIndexDialogue;

    [HideInInspector] public UnityEvent dialogueEndEvent;
    [HideInInspector] public UnityEvent dialogueEvent;

    [HideInInspector] public string tempKey;
    [HideInInspector] public NextDialogueStatus tempStatus;

    private void OnEnable()
    {
        for (int i = 0; i < dialogues.Count; i++)
        {
            dialoguesMap.Add(dialogues[i].key, dialogues[i]);
        }
    }
    //Function to add a new key to the dialogue list, the variables is set in the Editor script in function "CreateDialogue()"
    public void CreateDialogueKey()
    {
        dialogues.Add(new DialogueStruct(tempKey, tempStatus, dialogueEndEvent));
        keys.Add(tempKey);
        keyIndexDialogue = dialogues.Count - 1;
        tempKey = "";
        tempStatus = NextDialogueStatus.CONTINUE;
        dialogueEndEvent = null;
    }
    //Function to delete dialogue from the dialogues list and key list. Are called in the Editor script in function "ChangeDialogue()"
    public void DeleteDialogueKey()
    {
        keys.Remove(keys[keyIndexDialogue]);
        dialogues.Remove(dialogues[keyIndexDialogue]);
        keyIndexDialogue = dialogues.Count - 1;
    }
    //Function to reset all dialogues, cleaning the key and dialogue list. Are called in the Editor script in function "CreateDialogue()"
    public void ResetDialogues()
    {
        keys.Clear();
        dialogues.Clear();
        keyIndexDialogue = 0;
    }
    public void Func1() { Debug.Log("Func1"); }
    public void Func2() { Debug.Log("Func2"); }
}
public enum NextDialogueStatus 
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
    int textWidth = 125;
    SerializedProperty endDialogueEvent;
    SerializedProperty DialogueEvent;

    private void OnEnable()
    {
        endDialogueEvent = serializedObject.FindProperty("dialogueEndEvent");
        DialogueEvent = serializedObject.FindProperty("dialogueEvent");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CharacterD myTarget = (CharacterD)target;
        serializedObject.Update();

        CreateDialogue(myTarget);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        ChangeDialogue(myTarget);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(myTarget);

    }
    //Function to change one of the dialgues selected by a popup. Is called in the current Editor script "OnInspectorGUI()"
    private void ChangeDialogue(CharacterD myTarget)
    {
        if (myTarget.dialogues.Count > 0)
        {
            EditorGUILayout.BeginHorizontal();
            myTarget.keyIndexDialogue = EditorGUILayout.Popup(myTarget.keyIndexDialogue, myTarget.keys.ToArray(), GUILayout.MaxWidth(textWidth * 2));
            if (GUILayout.Button("Delete Dialogue Key", GUILayout.MaxWidth(textWidth * 2)))
            {
                myTarget.DeleteDialogueKey();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Dialogue Key", GUILayout.MaxWidth(textWidth - 25));
                myTarget.dialogues[myTarget.keyIndexDialogue].key = EditorGUILayout.TextField(myTarget.dialogues[myTarget.keyIndexDialogue].key, GUILayout.MaxWidth(textWidth));
                EditorGUILayout.LabelField("Dialogue Status", GUILayout.MaxWidth(textWidth - 25));
                myTarget.dialogues[myTarget.keyIndexDialogue].status = (NextDialogueStatus)EditorGUILayout.EnumPopup(myTarget.dialogues[myTarget.keyIndexDialogue].status, GUILayout.MaxWidth(textWidth));
            EditorGUILayout.EndHorizontal();
            myTarget.dialogueEvent = myTarget.dialogues[myTarget.keyIndexDialogue].endDialogueEvent;
            EditorGUILayout.PropertyField(DialogueEvent, GUILayout.MaxWidth(textWidth * 3));
            myTarget.dialogues[myTarget.keyIndexDialogue].endDialogueEvent = myTarget.dialogueEvent;
        }
    }
    /*Function to create a dialogue, its only for setting the variables in the ScriptableObject, 
     * the addition of the dialogue to the list its setting in functions in the ScriptableObject script.
     * Its called in the current Editor script "OnInspectorGUI()"
    */
    private void CreateDialogue(CharacterD myTarget)
    {
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Dialogue Key", GUILayout.MaxWidth(textWidth - 25));
            myTarget.tempKey = EditorGUILayout.TextField(myTarget.tempKey, GUILayout.MaxWidth(textWidth));
            EditorGUILayout.LabelField("Dialogue Status", GUILayout.MaxWidth(textWidth - 25));
            myTarget.tempStatus = (NextDialogueStatus)EditorGUILayout.EnumPopup(myTarget.tempStatus, GUILayout.MaxWidth(textWidth));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.PropertyField(endDialogueEvent, GUILayout.MaxWidth(textWidth * 3));

        EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Dialogue Key", GUILayout.MaxWidth(textWidth * 2)))
            {
                myTarget.CreateDialogueKey();
            }
            if (GUILayout.Button("Reset Dialogues", GUILayout.MaxWidth(textWidth * 2)))
            {
                myTarget.ResetDialogues();
            }
        EditorGUILayout.EndHorizontal();
    }
}
