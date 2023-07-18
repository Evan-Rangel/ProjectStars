using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Background Data", menuName = "Background Data")]
public class BackgroundData : ScriptableObject
{
    [Serializable]
    public class BackgroundSprite
    {
        public enum OrderInPattern
        {
            FIRST,
            MEDIUM,
            LAST,
            CUSTOM
        }
        public Sprite backgroundS;
        public OrderInPattern orderInPattern;
    }

    public enum BackgroundType
    {
        RANDOM,
        WITH_PATTERN,
        START_AND_END,
        //significa que hara los dos anteriores.
        PATTERN_AND_SAE

    }

    [SerializeField, HideInInspector] int orderInLayer;
    [SerializeField] List<BackgroundSprite> backgroundSprite ;
    [SerializeField, HideInInspector] float speed;
    [SerializeField, HideInInspector] Vector2 direction= new Vector2(0,-1);
    bool firts=true;

    [SerializeField, HideInInspector] BackgroundType backgroundType;
    
    bool showSprites;

    public BackgroundType GetBackgroundType { get { return backgroundType; } }
    public int GetOrderInLayer { get { return orderInLayer; } }
    public List<BackgroundSprite> GetBackgroundsSprites { get { return backgroundSprite; } }
    public float GetSpeed { get { return speed; } }
    public Vector2 GetDirection { get { return direction; } }



    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(BackgroundData))]
    public class BackgrounDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            BackgroundData backgroundData = (BackgroundData)target;

            int labelSize = 150;
            int valueSize = 300;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("OrderInLayer: ", GUILayout.MaxWidth(labelSize));
            backgroundData.orderInLayer = EditorGUILayout.IntSlider(backgroundData.orderInLayer, 0,3 , GUILayout.MaxWidth(labelSize));
            EditorGUILayout.EndHorizontal();



            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Background Type: ", GUILayout.MaxWidth(labelSize));
            backgroundData.backgroundType = (BackgroundData.BackgroundType)EditorGUILayout.EnumPopup(backgroundData.backgroundType, GUILayout.MaxWidth(labelSize));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("New Sprite"))
            {
                backgroundData.backgroundSprite.Add(new BackgroundSprite());
            }
            
            if (GUILayout.Button("Remove all sprites"))
            {
                backgroundData.backgroundSprite.Clear();
            }
            EditorGUILayout.EndHorizontal();


            if (backgroundData.backgroundSprite.Count>0) 
            {
                backgroundData.showSprites = EditorGUILayout.Foldout(backgroundData.showSprites, "Sprites", true);
                if (backgroundData.showSprites)
                {
                    for (int i = 0; i < backgroundData.backgroundSprite.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField("Sprite: ", GUILayout.MaxWidth(labelSize));
                        backgroundData.backgroundSprite[i].backgroundS = (Sprite)EditorGUILayout.ObjectField(backgroundData.backgroundSprite[i].backgroundS, typeof(Sprite), allowSceneObjects:true, GUILayout.MaxWidth(labelSize));
                        //EditorGUILayout.LabelField("Order In Pattern: ", GUILayout.MaxWidth(labelSize));
                        //backgroundData.backgroundSprite[i].orderInPattern = (BackgroundSprite.OrderInPattern)EditorGUILayout.EnumPopup(backgroundData.backgroundSprite[i].orderInPattern, GUILayout.MaxWidth(labelSize));
                        if (GUILayout.Button("Remove"))
                        {
                            backgroundData.backgroundSprite.Remove(backgroundData.backgroundSprite[i]);
                        }

                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Speed: ", GUILayout.MaxWidth(labelSize));
            backgroundData.speed = EditorGUILayout.FloatField(backgroundData.speed, GUILayout.MaxWidth(labelSize));
            EditorGUILayout.EndHorizontal();
            /*
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Direction: ", GUILayout.MaxWidth(labelSize));
            backgroundData.direction = EditorGUILayout.Vector2Field("",backgroundData.direction, GUILayout.MaxWidth(labelSize));
            EditorGUILayout.EndHorizontal();
            */
            EditorUtility.SetDirty(backgroundData);
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }
#endif
        }
    }
#endif
    #endregion
}