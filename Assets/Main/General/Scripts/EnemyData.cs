using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy Data", menuName ="Enemy Data")]
public class EnemyData : ScriptableObject
{
    public enum ShieldType
    {

    }

    [SerializeField] int health;
    [SerializeField] string nameE;
    [SerializeField] int score;
    [SerializeField] bool hasShield;



}
