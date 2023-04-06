using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Controller Data", menuName = "Controller Data")]
public class ControllerData : ScriptableObject
{
    /*No necesarias de momento
    string name;
    string description
    */

    bool isPlayer;
    [Range(0,10)]int[] attackTypes;
    float velocity;


    public bool IsPlayer { get { return isPlayer; } }
    public int[] AttackTypes { get { return attackTypes; } }
    public float Velocity { get { return velocity; } }

}
