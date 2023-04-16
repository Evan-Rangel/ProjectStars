using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

using UnityEditor;

#endif

[CreateAssetMenu(fileName = "New Enemy Shot Data", menuName = "Enemy Shot Data")]

public class EnemyShotData : ScriptableObject
{
    [SerializeField] int projectilesPerWave;
    [SerializeField] int attackType;
    [SerializeField] int projectileAngleInit;
    [SerializeField] float projectileAngleSum;
    [SerializeField] float projectileSpeed;


    public int ProjectilesPerWave { get { return projectilesPerWave; } }
    public int AttackType { get { return attackType; } }
    public int ProjectileAngleInit { get { return projectileAngleInit; } }
    public float ProjectileAngleSum { get { return projectileAngleSum; } }
    public float ProjectileSpeed { get { return projectileSpeed; } }


    #region Editor
#if UNITY_EDITOR

    [CustomEditor(typeof(EnemyShotData))]
    public class EnemyShotDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //Necesario par funcionar, si se quita y se carga el unity, se reseta el inspector
            base.OnInspectorGUI();
            EnemyShotData enemyShotData = (EnemyShotData)target;

        }
    }


#endif
    #endregion

}
