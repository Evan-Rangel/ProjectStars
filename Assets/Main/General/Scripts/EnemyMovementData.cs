using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif


[CreateAssetMenu(fileName = "New Enemy Movement Data", menuName = "Enemy Movement Data")]

public class EnemyMovementData : ScriptableObject
{
    enum MovType
    {
        NORTH,
        SOUTH,
        EAST,
        WEST,
        NORTHEAST,
        NORTHWEST,
        SOUTHEAT,
        SOUTHWEST
    }

    enum MovVel
    {
        ZERO,
        VERY_SLOW,
        SLOW,
        NORMAL,
        FAST,
        VERY_FAST,
        CUSTOM
    }


    // Movement direction (up, down, left, right))
    [SerializeField] List<MovType> movementType;
    // Velocity of determinate movement
    List<MovVel> movementVelocity;
    // Time for the nex type of movement
    [SerializeField, Range(0, 10)] float[] nextMovementTypeTime;

    //Ciclos
    // The position of the array whos start the cicle
    [SerializeField] int startCicle;
    // The position of the array whos finish the cicle
    [SerializeField] int finishCicle;

    int[] _movementType;

    float[] _movementVelocity;

    float[] velocitys = {0, 1, 2, 3, 4, 5, 0};
    int[] ConvertMovementType()
    {
        _movementType = new int[movementType.Count];
        for (int i = 0; i < _movementType.Length; i++)
        {
            _movementType[i] = ((int)movementType[i]); 
        }

        return _movementType;  
    }
    float[] ConvertMovementVelocityType()
    {
        _movementVelocity = new float[movementVelocity.Count];
        for (int i = 0; i < _movementVelocity.Length; i++)
        {

            if (movementVelocity[i]!=MovVel.CUSTOM)
            {
                _movementVelocity[i] = velocitys[((int)movementVelocity[i])];
                Debug.Log("A");
            }
        }
        return _movementVelocity;
    }
    public int[] MovementType { get { ConvertMovementType(); return _movementType; } }
    public float[] MovementVelocity { get  { ConvertMovementVelocityType(); return _movementVelocity; } }
    public float[] NextMovementTypeTime { get { return nextMovementTypeTime; } }
    public int StartCicle { get { return startCicle; } }
    public int FinishCicle { get { return finishCicle; } }



    float algo;
    float algo1;
    float algo2;

    //----------------------------------------------------------------------------------


    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyMovementData))]
    public class EnemyMovementDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            EnemyMovementData enemyMovementData = (EnemyMovementData)target;


            EditorGUILayout.BeginHorizontal();

            enemyMovementData.algo = EditorGUILayout.Slider(enemyMovementData.algo, 0,10);
            enemyMovementData.algo1 = EditorGUILayout.Slider(enemyMovementData.algo1, 0, 10);
            enemyMovementData.algo2 = EditorGUILayout.Slider(enemyMovementData.algo2, 0, 10);

           // EditorGUILayout.dou

            EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();

            for (int i = 0; i < enemyMovementData.movementVelocity.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Velocity " + i, GUILayout.MaxWidth(60));
                enemyMovementData.movementVelocity[i] = (MovVel)EditorGUILayout.EnumPopup(enemyMovementData.movementVelocity[i], GUILayout.MaxWidth(100));

                if (enemyMovementData.movementVelocity[i] == MovVel.CUSTOM)
                {
                    enemyMovementData.ConvertMovementVelocityType();
                    enemyMovementData._movementVelocity[i] = EditorGUILayout.FloatField(enemyMovementData._movementVelocity[i], GUILayout.MaxWidth(40));
                }
                EditorGUILayout.EndHorizontal();
            }
            //EditorGUILayout.EndHorizontal();

        }
    }

#endif
    #endregion
}
