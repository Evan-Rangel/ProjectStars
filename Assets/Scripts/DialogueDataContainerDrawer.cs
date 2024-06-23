using UnityEditor;
using UnityEngine;
[CustomPropertyDrawer(typeof(DialogueDataContainer<>))]
public class DialogueDataContainerDrawer : PropertyDrawer
{
    private const float FOLDOUT_HEIGHT = 16f;

    private SerializedProperty content;
    private SerializedProperty key;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (content == null)
        {
            content = property.FindPropertyRelative("content");
        }
        float height = FOLDOUT_HEIGHT;
        content.arraySize = 1;
        for (int i = 0; i < content.arraySize; ++i)
        {
            height += EditorGUI.GetPropertyHeight(content.GetArrayElementAtIndex(i));
        }
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        Rect foldoutRect = new Rect(position.x, position.y, position.width, FOLDOUT_HEIGHT);
      
        EditorGUI.indentLevel++;
        float addY = FOLDOUT_HEIGHT;
          
        for (int i = 0; i < content.arraySize; i++)
        {
            Rect rect = new Rect(position.x, position.y+addY, position.width, EditorGUI.GetPropertyHeight(content.GetArrayElementAtIndex(i)));
            addY += rect.height;
            EditorGUI.PropertyField(rect, content.GetArrayElementAtIndex(i),new GUIContent("Dialogue Class"), true);
        }
        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }

}
