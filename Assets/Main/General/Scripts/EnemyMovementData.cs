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

    enum MovSpeed
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
    List<MovSpeed> movementSpeed;
    // Time for the nex type of movement
    [SerializeField, Range(0, 10)] float[] nextMovementTypeTime;

    //Ciclos
    // The position of the array whos start the cicle
    [SerializeField] int startCicle;
    // The position of the array whos finish the cicle
    [SerializeField] int finishCicle;


    int[] _movementType;

    List<float> _movementSpeed;

    float[] velocitys = {0, 1, 2, 3, 4, 5, 10};


    [ContextMenu ("ClearValues()")]
    void ClearValues()
    {
        _movementSpeed.Clear();
        movementSpeed.Clear();
    }
    void RemoveElement(int index)
    {
        _movementSpeed.RemoveAt(index);
        movementSpeed.RemoveAt(index);
    }
    int[] ConvertMovementType()
    {
        _movementType = new int[movementType.Count];
        for (int i = 0; i < _movementType.Length; i++)
        {
            _movementType[i] = ((int)movementType[i]); 
        }

        return _movementType;  
    }

    /*
    List<float> ConvertMovementVelocityType()
    {
        for (int i = 0; i < movementSpeed.Count; i++)
        {
            _movementSpeed[i]=(velocitys[((int)movementSpeed[i])]);
        }
        return _movementSpeed;
    }*/
    public int[] MovementType { get { ConvertMovementType(); return _movementType; } }
    public List<float> MovementVelocity { get  { /*ConvertMovementVelocityType();*/ return _movementSpeed; } }
    public float[] NextMovementTypeTime { get { return nextMovementTypeTime; } }
    public int StartCicle { get { return startCicle; } }
    public int FinishCicle { get { return finishCicle; } }


    bool create;

    //----------------------------------------------------------------------------------
    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyMovementData))]
    public class EnemyMovementDataEditor : Editor
    {
        
        public override void OnInspectorGUI()
        {
            //Necesario par funcionar, si se quita y se carga el unity, se reseta el inspector
            base.OnInspectorGUI();
            //Obtenemos la referencia del inspector que queremos editar, en este caso esta dentro del mismo script
            EnemyMovementData enemyMovementData = (EnemyMovementData)target;

            //Funcion para crear distintas velocidades
            GetMovementSpeed(enemyMovementData);
        }

        private static void GetMovementSpeed(EnemyMovementData enemyMovementData)
        {
            EditorGUILayout.Space();

            if (GUILayout.Button("Create new mov"))
            {
                enemyMovementData.movementSpeed.Add(MovSpeed.NORMAL);
                enemyMovementData._movementSpeed.Add(0);
            }


            for (int i = 0; i < enemyMovementData.movementSpeed.Count; i++)
            {
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Speed " + i, GUILayout.MaxWidth(70));

                if (enemyMovementData.movementSpeed.Count > i)
                {
                    enemyMovementData.movementSpeed[i] = (MovSpeed)EditorGUILayout.EnumPopup(enemyMovementData.movementSpeed[i], GUILayout.MaxWidth(100));
                    if (enemyMovementData.movementSpeed[i]!= MovSpeed.CUSTOM)
                    {
                        enemyMovementData._movementSpeed[i] = (enemyMovementData.velocitys[((int)enemyMovementData.movementSpeed[i])]);
                    }
                    else
                    {
                        enemyMovementData._movementSpeed[i] = EditorGUILayout.FloatField(enemyMovementData._movementSpeed[i], GUILayout.MaxWidth(40));
                    }
                }
                
                if (GUILayout.Button("Remove", GUILayout.MaxWidth(60)))
                {
                    enemyMovementData.RemoveElement(i);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

#endif
    #endregion
}
