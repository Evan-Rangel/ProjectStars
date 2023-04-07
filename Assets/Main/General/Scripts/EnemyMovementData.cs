using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Movement Data", menuName = "Enemy Movement Data")]

public class EnemyMovementData : ScriptableObject
{
    // Movement direction (up, down, left, right))
    [SerializeField, Range(0, 10)] int[] movementType;
    // Velocity of determinate movement
    [SerializeField, Range(0, 10)] float[] movementVelocity;
    // Time for the nex type of movement
    [SerializeField, Range(0, 10)] float[] nextMovementTypeTime;

    //Ciclos
    // The position of the array whos start the cicle
    [SerializeField] int startCicle;
    // The position of the array whos finish the cicle
    [SerializeField] int finishCicle;

    public int[] MovementType { get { return movementType; } }
    public float[] MovementVelocity { get { return movementVelocity; } }
    public float[] NextMovementTypeTime { get { return nextMovementTypeTime; } }
    public int StartCicle { get { return startCicle; } }
    public int FinishCicle { get { return finishCicle; } }

}
