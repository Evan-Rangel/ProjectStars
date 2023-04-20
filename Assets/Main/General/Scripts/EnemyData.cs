using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy Data", menuName ="Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] int health;
    [SerializeField] string nameE;
    [SerializeField] int score;
}
