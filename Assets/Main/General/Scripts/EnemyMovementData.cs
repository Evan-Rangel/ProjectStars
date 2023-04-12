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
        NONE,
        NORTH,
        SOUTH,
        EAST,
        WEST,
        NORTHEAST,
        NORTHWEST,
        SOUTHEAT,
        SOUTHWEST,
        CUSTOM
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

    enum MovTime
    {
        VERY_SHORT,
        SHORT,
        NORMAL,
        LONG,
        VERY_LONG,
        CUSTOM
    }


    [SerializeField] int movementCount = 0;

    // Movement direction (up, down, left, right))
    List<MovType> movementDirection;
    List<Vector2> direction;
    Vector2[] directions =
        {
        Vector2.zero,
        Vector2.up,
        Vector2.down,
        Vector2.right,
        Vector2.left,
        new Vector2 (1,1).normalized,
        new Vector2 (-1,1).normalized,
        new Vector2 (1,-1).normalized,
        new Vector2 (-1,-1).normalized,
        Vector2.zero

        };


    // Velocity of determinate movement
    List<MovSpeed> movementSpeed;
    List<float> speed;
    float[] speeds = {0, 1, 2, 3, 4, 5, 0};


    // Time for the nex type of movement
    List<MovTime> movementTime;
    List<float> time;
    float[] times = {0.2f,0.4f,0.6f,0.8f,1,0 };


    //Ciclos
    // The position of the array whos start the cicle
    [SerializeField] int startCicle;
    // The position of the array whos finish the cicle
    [SerializeField] int finishCicle;

    [ContextMenu ("ClearValues()")]
    void ClearValues()
    {
        speed.Clear();
        movementSpeed.Clear();
        movementDirection.Clear();
        direction.Clear();

    }
    void RemoveElement(int index)
    {
        speed.RemoveAt(index);
        movementSpeed.RemoveAt(index);
        direction.RemoveAt(index);
        movementDirection.RemoveAt(index);
    }
    /*
    List<Vector2> ConvertMovementType()
    {
        for (int i = 0; i < _movementType.Count; i++)
        {
            _movementType[i] = ((int)movementType[i]); 
        }

        return direction;  
    }

    List<float> ConvertMovementVelocityType()
    {
        for (int i = 0; i < movementSpeed.Count; i++)
        {
            _movementSpeed[i]=(velocitys[((int)movementSpeed[i])]);
        }
        return _movementSpeed;
    }*/
    public List<Vector2> Direction { get { return direction; } }
    public List<float> Speed { get  { return speed; } }
    public List<float> NextMovementTypeTime { get { return time; } }
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
            InspectorCustom(enemyMovementData);
        }

        private static void InspectorCustom(EnemyMovementData enemyMovementData)
        {
            EditorGUILayout.Space();
            CreateNewMovementButton(enemyMovementData);

            for (int i = 0; i < enemyMovementData.movementSpeed.Count; i++)
            {
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();

                InspectorShowMovementDirection(enemyMovementData, i);

                InspectorShowMovementSpeed(enemyMovementData, i);
                InspectorShowMovementTime(enemyMovementData, i);

                if (GUILayout.Button("Remove", GUILayout.MaxWidth(60)))
                {
                    enemyMovementData.RemoveElement(i);
                    enemyMovementData.movementCount--;
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private static void CreateNewMovementButton(EnemyMovementData enemyMovementData)
        {
            //Checa si falta o sobra algun dato
            CheckForData(enemyMovementData);

            //Crea un nuevo movimiento
            if (GUILayout.Button("Create new mov"))
            {
                enemyMovementData.movementDirection.Add(MovType.NONE);
                enemyMovementData.direction.Add(Vector2.zero);

                enemyMovementData.movementSpeed.Add(MovSpeed.ZERO);
                enemyMovementData.speed.Add(0);

                enemyMovementData.movementTime.Add(MovTime.NORMAL);
                enemyMovementData.time.Add(0.6f);
                enemyMovementData.movementCount++;
            }
        }

        private static void CheckForData(EnemyMovementData enemyMovementData)
        {
            for (int i = 0; i < enemyMovementData.movementCount; i++)
            {
                //Checar si falta
                if (enemyMovementData.direction.Count < enemyMovementData.movementCount)
                {
                    enemyMovementData.direction.Add(Vector2.zero);
                }
                if (enemyMovementData.movementDirection.Count < enemyMovementData.movementCount)
                {
                    enemyMovementData.movementDirection.Add(MovType.NONE);
                }

                if (enemyMovementData.speed.Count < enemyMovementData.movementCount)
                {
                    enemyMovementData.speed.Add(0);
                }
                if (enemyMovementData.movementSpeed.Count < enemyMovementData.movementCount)
                {
                    enemyMovementData.movementSpeed.Add(MovSpeed.ZERO);
                }

                if (enemyMovementData.time.Count < enemyMovementData.movementCount)
                {
                    enemyMovementData.time.Add(0.6f);
                }
                if (enemyMovementData.movementTime.Count < enemyMovementData.movementCount)
                {
                    enemyMovementData.movementTime.Add(MovTime.NORMAL);

                }

                //Checar si sobra
                if (enemyMovementData.direction.Count > enemyMovementData.movementCount)
                {
                    enemyMovementData.direction.RemoveAt(i);
                }
                if (enemyMovementData.movementDirection.Count > enemyMovementData.movementCount)
                {
                    enemyMovementData.movementDirection.RemoveAt(i);
                }

                if (enemyMovementData.speed.Count > enemyMovementData.movementCount)
                {
                    enemyMovementData.speed.RemoveAt(i);
                }
                if (enemyMovementData.movementSpeed.Count > enemyMovementData.movementCount)
                {
                    enemyMovementData.movementSpeed.RemoveAt(i);
                }

                if (enemyMovementData.time.Count > enemyMovementData.movementCount)
                {
                    enemyMovementData.time.RemoveAt(i);
                }
                if (enemyMovementData.movementTime.Count > enemyMovementData.movementCount)
                {
                    enemyMovementData.movementTime.RemoveAt(i);
                }
            }
        }

        private static void InspectorShowMovementDirection(EnemyMovementData enemyMovementData, int i)
        {
            EditorGUILayout.LabelField("Movement: " + i, GUILayout.MaxWidth(85));

            if (enemyMovementData.movementDirection.Count > i)
            {
                enemyMovementData.movementDirection[i] = (MovType)EditorGUILayout.EnumPopup(enemyMovementData.movementDirection[i], GUILayout.MaxWidth(100));

                if (enemyMovementData.movementDirection[i] != MovType.CUSTOM)
                {
                    enemyMovementData.direction[i] = (enemyMovementData.directions[((int)enemyMovementData.movementDirection[i])]);
                }
                else
                {
                    enemyMovementData.direction[i] = EditorGUILayout.Vector2Field("", enemyMovementData.direction[i], GUILayout.MinWidth(100), GUILayout.MaxWidth(150)).normalized;
                }
            }
        }

        private static void InspectorShowMovementSpeed(EnemyMovementData enemyMovementData, int i)
        {
            EditorGUILayout.LabelField("Speed: ", GUILayout.MaxWidth(50));

            if (enemyMovementData.movementSpeed.Count > i)
            {
                enemyMovementData.movementSpeed[i] = (MovSpeed)EditorGUILayout.EnumPopup(enemyMovementData.movementSpeed[i], GUILayout.MaxWidth(100));

                if (enemyMovementData.movementSpeed[i] != MovSpeed.CUSTOM)
                {
                    enemyMovementData.speed[i] = (enemyMovementData.speeds[((int)enemyMovementData.movementSpeed[i])]);
                }
                else
                {
                    enemyMovementData.speed[i] = EditorGUILayout.FloatField(enemyMovementData.speed[i], GUILayout.MaxWidth(40));
                }
            }
        }


        private static void InspectorShowMovementTime(EnemyMovementData enemyMovementData, int i)
        {
            EditorGUILayout.LabelField("Time: ", GUILayout.MaxWidth(50));

            if (enemyMovementData.movementTime.Count > i)
            {
                enemyMovementData.movementTime[i] = (MovTime)EditorGUILayout.EnumPopup(enemyMovementData.movementTime[i], GUILayout.MaxWidth(100));

                if (enemyMovementData.movementTime[i] != MovTime.CUSTOM)
                {
                    enemyMovementData.time[i] = (enemyMovementData.speeds[((int)enemyMovementData.movementTime[i])]);
                }
                else
                {
                    enemyMovementData.time[i] = EditorGUILayout.FloatField(enemyMovementData.time[i], GUILayout.MaxWidth(40));
                }
            }
        }
    }

#endif
    #endregion
}
