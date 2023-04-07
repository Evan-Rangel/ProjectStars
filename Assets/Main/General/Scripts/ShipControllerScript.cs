using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Boundary1
{
    public float xMin, xMax, yMin, yMax;
}
public class ShipControllerScript : MonoBehaviour
{
    // Limits 
    Boundary1 boundary;
    
    // InputSystem PlayerInput
    PlayerInput inputController;
    
    // Data
    ControllerData controllerData;
    
    // Rigybody
    Rigidbody2D rb;


    // Direction (esta debe de tenerla el enemigo pero no el player)
    Vector2 dir;


    // ----------------Variables que deberia tener el scriptableObject
    // If the object is the player
    bool isPlayer;
    

    // Velocity
    float velocity;

    // Contador de tipo de movimiento
    int contMovementType;

    // Bala a disparar
    GameObject bullet;

    // Contador para siguiente movimiento
    float contNextMovement;

    //Contador para siguiente tipo de ataque
    float contNextAttack;


    private void Awake()
    { 
        if(controllerData.IsPlayer)
        {
            inputController= GetComponent<PlayerInput>();
        }
        
        rb = GetComponent<Rigidbody2D>();
        contMovementType = 0;
        contNextMovement = 0;
        contNextAttack = 0;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void MovementPlayer()
    {
        dir = inputController.actions["Move"].ReadValue<Vector2>().normalized;
    }
    void TypeMovementEnemy()
    {
        switch (controllerData.MovementType[contMovementType])
        {
            case 0: // Direccion arriba
                dir = Vector2.up;
                break;
            case 1: // Direccion abajo
                dir = Vector2.down;
                break;
            case 2: // Direccion derecha
                dir = Vector2.right;
                break;
            case 3: // Direccion izquierda
                dir = Vector2.left;
                break;
            case 4: // Direccion arriba derecha
                dir = new Vector2(1, 1).normalized;
                break;
            case 5:// Direccion arriba izquieda
                dir = new Vector2(-1, 1).normalized;
                break;
            case 6:
                // Direccion abajo izquierda
                dir = new Vector2(-1, -1).normalized;
                break;
            case 7:// Direccion abajo derecha
                dir = new Vector2(-1, -1).normalized;
                break;
            case 8:// Sin direccion, estatico
                dir = Vector2.zero;
                break;
            case 9:
                break;
            case 10:
                break;

        }
    }

    void Movement()
    {

        rb.position = new Vector2(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax));
        rb.MovePosition(rb.position + dir * controllerData.Velocity[contMovementType] * Time.fixedDeltaTime );
        contNextMovement += Time.fixedDeltaTime;
        if (contNextMovement>controllerData.NextMovementTypeTime[contMovementType])
        {

        }

    }



}

