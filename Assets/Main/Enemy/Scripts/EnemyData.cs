using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public enum ShieldType
    {

    }

    public enum EnemyDifficult
    {
        EASY=1,
        NORMAL,
        HARD,
    }


    [SerializeField] int health;
    [SerializeField] string nameE;
    [SerializeField] int score;
    [SerializeField] bool hasShield;
    [SerializeField] EnemyDifficult enemyDifficult;
    [SerializeField] List< EnemyAttackData> enemyAttackData;
    [SerializeField] List<EnemyMovementData> enemyMovementData;

    public List<EnemyMovementData> GetEnemyMovementData { get { return enemyMovementData; } }
    public List<EnemyAttackData> GetEnemyAttackData { get { return enemyAttackData; } }
    public EnemyDifficult GetEnemyDifficulty { get { return enemyDifficult; } }
    public int GetHealth { get { return health; } }

}
