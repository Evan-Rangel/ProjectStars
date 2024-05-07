using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;


public class DialogueDispatcher : MonoBehaviour
{
    Text text;
    [SerializeField] LocalizeStringEvent localizedStringEvent;
    string tableName = "TutorialCollection";
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public string GetDialogue(string _key)
    {
        localizedStringEvent.StringReference.SetReference(tableName, _key);
        return text.text;
    }
}
