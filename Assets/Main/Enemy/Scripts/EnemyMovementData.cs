using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor.SceneManagement;

using UnityEditor;

#endif


[CreateAssetMenu(fileName = "New Enemy Movement Data", menuName = "Enemy Movement Data")]
[Serializable]

public class EnemyMovementData : ScriptableObject
{
    //Enums
    public enum MovDirection
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

    public enum MovSpeed
    {
        ZERO,
        VERY_SLOW,
        SLOW,
        NORMAL,
        FAST,
        VERY_FAST,
        CUSTOM
    }

    public enum MovTime
    {
        VERY_SHORT,
        SHORT,
        NORMAL,
        LONG,
        VERY_LONG,
        CUSTOM
    }

    public enum MovType
    {
        FOLLOW_PLAYER,
        ONE_MOV,
        CUSTOM_MOVEMENT,
        NONE
    }

    [Serializable]
    public class Movement
    {
        public MovDirection movD;
        public Vector2 dir;
        public MovSpeed movS;
        public float speed;
        public MovTime movT;
        public float time;

        public Movement()
        {
            movD = MovDirection.NONE;
            movS = MovSpeed.ZERO;
            movT = MovTime.NORMAL;
            dir = Vector2.zero;
            speed = 0;
            time = 0;
        }
    }
    [SerializeField, HideInInspector] List<Movement> movements;

    [SerializeField, HideInInspector] MovType movType;

    //Aceleration for FOLLOW_PLAYER
    [SerializeField, HideInInspector] float force_FP;
    [SerializeField, HideInInspector] float time_FP;

    //Variables para ONLY_ONE
    [SerializeField, HideInInspector] Vector2 direction_OM;
    [SerializeField, HideInInspector] float speed_OM;

    //Todas las variables a partir de aqui, son para el SET_MOVEMENT
    [SerializeField, HideInInspector] int movementCount = 0;

    // direcciones 
    [SerializeField, HideInInspector]
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


    // velocidades 
    [SerializeField, HideInInspector] float[] speeds = {0, 1, 2, 3, 4, 5, 0};

    // tiempos
    [SerializeField, HideInInspector] float[] times = {0.2f,0.4f,0.6f,0.8f,1,0 };

    [SerializeField, HideInInspector] bool showMovements = false;
    [SerializeField, HideInInspector] bool cycleMovements = false;

    [ContextMenu ("ClearValues()")]
    
    //Resetea la lista por si hay algun bug o algo
    void ClearValues()
    {

        movementCount = 0;
        movements.Clear();

    }
    //Remueve en una posicion especifica
    void RemoveElement(int index)
    {
        movements.RemoveAt(index);


    }
    //Crea nuevos valores para la lista
    void NewMovementCreation()
    {
        movements.Add(new Movement());

        movementCount++;
    }

    //Getter para el tipo de movimiento
    public MovType GetMovementType { get { return movType; } }


    //Getters si es FOLLOW_PLAYER
    public float GetForce_FP { get { return force_FP; } }
    public float GetTime_FP { get { return time_FP; } }

    //Getters si es ONLY_ONE_MOV
    public Vector2 GetDirection_OM { get { return direction_OM; } }
    public float GetSpeed_OM { get { return speed_OM; } }


    //Getters si es CUSTOM_MOVEMENT
    public List<Movement> GetMovement { get { return movements; } }
    public int GetMovementCount { get { return movementCount; } }
    public bool CycleMovement { get { return cycleMovements; } }

   
  

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

            enemyMovementData.movType = (MovType)EditorGUILayout.EnumPopup(enemyMovementData.movType, GUILayout.MaxWidth(250));
            //Switch para determinar el tipo de movimiento. Pede haber mas o cambiar en el futuro
            switch (enemyMovementData.movType)
            {
                case MovType.FOLLOW_PLAYER:
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Aceleration: ", GUILayout.MaxWidth(100));

                    enemyMovementData.force_FP = EditorGUILayout.FloatField(enemyMovementData.force_FP, GUILayout.MaxWidth(30));
                    EditorGUILayout.LabelField("Time: ", GUILayout.MaxWidth(100));
                    enemyMovementData.time_FP = EditorGUILayout.FloatField(enemyMovementData.time_FP, GUILayout.MaxWidth(30));

                    GUILayout.EndHorizontal();
                    break;
                case MovType.ONE_MOV:
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Speed: ", GUILayout.MaxWidth(60)) ;
                    enemyMovementData.speed_OM = EditorGUILayout.FloatField(enemyMovementData.speed_OM, GUILayout.MaxWidth(30));
                    EditorGUILayout.LabelField("Direction: ", GUILayout.MaxWidth(60));
                    enemyMovementData.direction_OM = EditorGUILayout.Vector2Field("",enemyMovementData.direction_OM, GUILayout.MaxWidth(125));
                    GUILayout.EndHorizontal();
                    break;
                case MovType.CUSTOM_MOVEMENT:
                    SetMovement(enemyMovementData);
                    break;
            }
            EditorUtility.SetDirty(enemyMovementData);
#if UNITY_EDITOR

            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }
#endif

        }

        //Funcion para la opcion de SET_MOVEMENT
        private static void SetMovement(EnemyMovementData enemyMovementData)
        {
            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Cycle: ", GUILayout.MaxWidth(50));
            enemyMovementData.cycleMovements = EditorGUILayout.Toggle(enemyMovementData.cycleMovements);
            GUILayout.EndHorizontal();

            //Esto es para crear una toggle list de los movimientos y poder ocultarlos
            enemyMovementData.showMovements = EditorGUILayout.Foldout(enemyMovementData.showMovements, "Movements", true);

            //Si showmovements es true se mostraran los movimientos, si es falso, no se muestran
            if (enemyMovementData.showMovements)
            {
                //Para darle una tabulacion
                EditorGUI.indentLevel++;

                GUILayout.BeginHorizontal();
                //Tamaño del arreglo de movimientos
                EditorGUILayout.LabelField("Size: ",GUILayout.MaxWidth(50));
                enemyMovementData.movementCount = Mathf.Max(0, EditorGUILayout.IntField(enemyMovementData.movementCount, GUILayout.MaxWidth(60)));
                GUILayout.EndHorizontal();
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
                
                InspectorShowMovement(enemyMovementData, i);
                
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
        //Boton para nuevo movimiento
        private static void CreateNewMovementButton(EnemyMovementData enemyMovementData)
        {
            //Checa si falta o sobra algun dato
            CheckForData(enemyMovementData);

            GUILayout.BeginHorizontal();
            //Crea un nuevo movimiento
            if (GUILayout.Button("Create new mov", GUILayout.MaxWidth(250)))
            {
              
                enemyMovementData.NewMovementCreation();
            }
            //Crea un nuevo movimiento
            if (GUILayout.Button("Delete all mov", GUILayout.MaxWidth(250)))
            {
                enemyMovementData.ClearValues();
            }
            GUILayout.EndHorizontal();
        }

        //Funcion para confirmar que el tamanio de las listas estan bien
        private static void CheckForData(EnemyMovementData enemyMovementData)
        {
            //Checar si falta
            if (enemyMovementData.movements.Count<enemyMovementData.movementCount)
            {
                enemyMovementData.movements.Add(new Movement());
                //CheckForData(enemyMovementData);
            }

            //Checar si sobra
            if (enemyMovementData.movements.Count > enemyMovementData.movementCount)
            {
                enemyMovementData.RemoveElement(enemyMovementData.movements.Count);
                //CheckForData(enemyMovementData);
            }
        }

        //Nuevo movimiento
        private static void InspectorShowMovement(EnemyMovementData enemyMovementData, int i)
        {
            if (enemyMovementData.movements.Count>i)
            {
                EditorGUILayout.LabelField("Movement " + i + ":", GUILayout.MaxWidth(95));
                enemyMovementData.movements[i].movD = (MovDirection)EditorGUILayout.EnumPopup(enemyMovementData.movements[i].movD, GUILayout.MaxWidth(100));
                if (enemyMovementData.movements[i].movD!= MovDirection.CUSTOM)
                {
                    enemyMovementData.movements[i].dir = enemyMovementData.directions[((int)enemyMovementData.movements[i].movD)];
                }
                else
                {
                    enemyMovementData.movements[i].dir= EditorGUILayout.Vector2Field("", enemyMovementData.movements[i].dir, GUILayout.MinWidth(75), GUILayout.MaxWidth(125)).normalized;
                }

                EditorGUILayout.LabelField("Speed: ", GUILayout.MaxWidth(55));
                enemyMovementData.movements[i].movS = (MovSpeed)EditorGUILayout.EnumPopup(enemyMovementData.movements[i].movS, GUILayout.MaxWidth(100));
                if (enemyMovementData.movements[i].movS != MovSpeed.ZERO)
                {
                    enemyMovementData.movements[i].speed = enemyMovementData.speeds[((int)enemyMovementData.movements[i].movS)];
                }
                else
                {
                    enemyMovementData.movements[i].speed = EditorGUILayout.FloatField(enemyMovementData.movements[i].speed, GUILayout.MaxWidth(40));
                }

                EditorGUILayout.LabelField("Speed: ", GUILayout.MaxWidth(55));
                enemyMovementData.movements[i].movT = (MovTime)EditorGUILayout.EnumPopup(enemyMovementData.movements[i].movT, GUILayout.MaxWidth(100));
                if (enemyMovementData.movements[i].movT != MovTime.NORMAL)
                {
                    enemyMovementData.movements[i].time = enemyMovementData.times[((int)enemyMovementData.movements[i].movT)];
                }
                else
                {
                    enemyMovementData.movements[i].time = EditorGUILayout.FloatField(enemyMovementData.movements[i].time, GUILayout.MaxWidth(40));
                }
            }
        }
    }

#endif
    #endregion
}
