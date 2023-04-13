using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif


[CreateAssetMenu(fileName = "New Enemy Movement Data", menuName = "Enemy Movement Data")]

public class EnemyMovementData : ScriptableObject
{
    //Enums
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

    

    int movementCount = 0;

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

    bool showMovements = false;


    [ContextMenu ("ClearValues()")]
    
    //Resetea la lista por si hay algun bug o algo
    void ClearValues()
    {
        speed.Clear();
        movementSpeed.Clear();
        movementDirection.Clear();
        direction.Clear();
        time.Clear();
        movementTime.Clear();
        movementCount = 0;

    }
    //Remueve en una posicion especifica
    void RemoveElement(int index)
    {
        speed.RemoveAt(index);
        movementSpeed.RemoveAt(index);
        direction.RemoveAt(index);
        movementDirection.RemoveAt(index);
        time.RemoveAt(index);
        movementTime.RemoveAt(index);
    }
    //Crea nuevos valores para la lista
    void NewMovementCreation()
    {
        movementDirection.Add(MovType.NONE);
        direction.Add(Vector2.zero);

        movementSpeed.Add(MovSpeed.ZERO);
        speed.Add(0);

        movementTime.Add(MovTime.NORMAL);
        time.Add(0.6f);
        movementCount++;
    }
    public List<Vector2> Direction { get { return direction; } }
    public List<float> Speed { get  { return speed; } }
    public List<float> Time { get { return time; } }
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
            //Espacio
            EditorGUILayout.Space();
            //Esto es para crear una toggle list de los movimientos y poder ocultarlos
            enemyMovementData.showMovements = EditorGUILayout.Foldout(enemyMovementData.showMovements, "Movements", true);

            //Si showmovements es true se mostraran los movimientos, si es falso, no se muestran
            if (enemyMovementData.showMovements)
            {
                //Para darle una tabulacion
                EditorGUI.indentLevel++;

                //Tamaño del arreglo de movimientos
                enemyMovementData.movementCount = Mathf.Max(0, EditorGUILayout.IntField("Size", enemyMovementData.movementCount));

                //Funcion para crear distintas velocidades
                InspectorCustom(enemyMovementData);
                //Quitar la tabulacion
                EditorGUI.indentLevel++;
            }
        }

        private static void InspectorCustom(EnemyMovementData enemyMovementData)
        {
            //Espacio
            EditorGUILayout.Space();
            //Funcion para crear una nueva lista
            CreateNewMovementButton(enemyMovementData);

            for (int i = 0; i < enemyMovementData.movementCount; i++)
            {
                //Espacio
                EditorGUILayout.Space();
                //Para hacer el inspector horizontal
                EditorGUILayout.BeginHorizontal();
                //Funcion para la direccion
                InspectorShowMovementDirection(enemyMovementData, i);
                //Funcion para la velocidad
                InspectorShowMovementSpeed(enemyMovementData, i);
                //Funcion para el tiempo
                InspectorShowMovementTime(enemyMovementData, i);
                //Boton "Remove" para eliminar algun dato de la lista
                if (GUILayout.Button("Remove", GUILayout.MaxWidth(60)))
                {
                    enemyMovementData.RemoveElement(i);
                    enemyMovementData.movementCount--;
                }
                //Para dejar de hacerlo horizontal
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
                enemyMovementData.NewMovementCreation();
            }
        }

        //Funcion para confirmar que el tamanio de las listas estan bien
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
        
        //Esto es para la direccion
        private static void InspectorShowMovementDirection(EnemyMovementData enemyMovementData, int i)
        {
            //Texto en el inspector
            EditorGUILayout.LabelField("Movement " + i+":", GUILayout.MaxWidth(95));

            //Corrobora si esta bien el tamaño
            if (enemyMovementData.movementDirection.Count > i)
            {
                //Lista de enum
                enemyMovementData.movementDirection[i] = (MovType)EditorGUILayout.EnumPopup(enemyMovementData.movementDirection[i], GUILayout.MaxWidth(100));

                //Si no es custom agarra de la lista de "directions"
                if (enemyMovementData.movementDirection[i] != MovType.CUSTOM)
                {
                    enemyMovementData.direction[i] = (enemyMovementData.directions[((int)enemyMovementData.movementDirection[i])]);
                }//Si es custom, aparece un input de Vector2
                else
                {
                    enemyMovementData.direction[i] = EditorGUILayout.Vector2Field("", enemyMovementData.direction[i], GUILayout.MinWidth(100), GUILayout.MaxWidth(150)).normalized;
                }
            }
        }
        
        //Esto es para la velocidad
        private static void InspectorShowMovementSpeed(EnemyMovementData enemyMovementData, int i)
        {
            EditorGUILayout.LabelField("Speed: ", GUILayout.MaxWidth(55));

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

        //Esto es para el tiempo
        private static void InspectorShowMovementTime(EnemyMovementData enemyMovementData, int i)
        {
            EditorGUILayout.LabelField("Time: ", GUILayout.MaxWidth(55));

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
