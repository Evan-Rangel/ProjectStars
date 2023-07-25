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
        EASY = 1,
        NORMAL,
        HARD,
    }

    [SerializeField] bool isBoss;
    [SerializeField] int health;
    [SerializeField] string nameE;
    [SerializeField] int score;
    [SerializeField] bool hasShield;
    [SerializeField] EnemyDifficult enemyDifficult;
    [SerializeField] List<EnemyAttackData> enemyAttackData;
    [SerializeField] List<EnemyMovementData> enemyMovementData;
    [SerializeField] Color color;
    [SerializeField] RuntimeAnimatorController newController;
    public List<EnemyMovementData> GetEnemyMovementData { get { return enemyMovementData; } }
    public List<EnemyAttackData> GetEnemyAttackData { get { return enemyAttackData; } }
    public EnemyDifficult GetEnemyDifficulty { get { return enemyDifficult; } }
    public int GetHealth { get { return health; } }
    public Color GetColor { get { return color; } }
    public RuntimeAnimatorController NewController { get { return newController; } } 
    public int GetScore { get { return score; } }
    public bool GetIsBoss { get { return isBoss; } }
    
}
