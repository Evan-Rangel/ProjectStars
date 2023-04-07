using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Controller Data", menuName = "Controller Data")]

public class ControllerData : ScriptableObject
{


    [Header ("For Player")]
    [SerializeField] bool isPlayer;
    [SerializeField] float playerVelocity;
    
    [Header ("For Enemy")]
    // Movement direction (up, down, left, right))
    [SerializeField, Range(0, 10)] int[] movementType;
    // Velocity of determinate movement
    [SerializeField, Range (0,10)] float[] velocity;
    // Time for the nex type of movement
    [SerializeField, Range(0, 10)] float[] nextMovementTypeTime;
    // Time for the next type of shoot
    [SerializeField, Range(0, 10)] float[] nextAttackTypeTime;
    // Time for the next shoot
    [SerializeField, Range(0, 10)] float[] attackTime;

    // Pensar alguna manera de generar un ciclo. A partir de cierto rangos de movimientos o de ataques


    [Header("For Player and Enemy")]
    /* Innecesarias de momento
    [SerializeField] string name;
    [SerializeField] string description
    */
    
    // Attack type in the movement
    // Por el momento, no se cuantos tipos de ataques tendra, de momento hay 3, humm a lo mejor si es mejor hacerlo en dos diferentes.
    [SerializeField, Range(0, 2)] int[] attackType;


    /*
     * Cosas que usa el player
     * -Velocidad
     * -Cadencia de disparo
     * -Tipo de bala
     * -Tipo de ataque
     * -Nombre
     * -Descripcion
     * -
     * 
     * 
     * Cosas que usa el enemigo
     * -Velocidad
     * -Cadencia de disparo
     * -Tipo de bala
     * -Tipo de ataque 
     * -Nombre 
     * -Descripcion 
     
     */

    public bool IsPlayer { get { return isPlayer; } }
    public int[] MovementType { get { return movementType; } }
    public float[] Velocity { get { return velocity; } }
    public float[] NextMovementTypeTime { get { return nextMovementTypeTime; } }
    public float[] NextAttackTypeTime { get { return nextAttackTypeTime; } }
    public float[] AttackTime { get { return attackTime; } }
}