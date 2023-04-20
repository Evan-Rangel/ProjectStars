using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

using UnityEditor;

#endif
[CreateAssetMenu(fileName = "New Enemy Shot Data", menuName = "Enemy Shot Data")]

public class EnemyAttackData : ScriptableObject
{
    enum AttackType
    {
        NONE,
        BULLET,
        LASER
    }
    [SerializeField] AttackType attackType;

    [SerializeField] int projectilesPerWave;
    [SerializeField] int projectileAngleInit;
    [SerializeField] float projectileAngleSum;
    [SerializeField] float projectileSpeed;
    [SerializeField] float shotCadence;



    public int GetProjectilesPerWave { get { return projectilesPerWave; } }
    public int GetAttackType { get { return (int)attackType; } }
    public int GetProjectileAngleInit { get { return projectileAngleInit; } }
    public float GetProjectileAngleSum { get { return projectileAngleSum; } }
    public float GetProjectileSpeed { get { return projectileSpeed; } }
    public float GetShotCadence { get { return shotCadence; } }

    #region Editor
#if UNITY_EDITOR

    [CustomEditor(typeof(EnemyAttackData))]
    public class EnemyShotDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //Necesario par funcionar, si se quita y se carga el unity, se reseta el inspector
            base.OnInspectorGUI();
            EnemyAttackData enemyAttackData = (EnemyAttackData)target;

        }
    }


#endif
    #endregion

}
