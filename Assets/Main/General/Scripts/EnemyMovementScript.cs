using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    // Scriptable object
    [SerializeField] EnemyMovementData movemenD;
    [SerializeField] List<float> movSpeeds;

    // Rigybody
    Rigidbody2D rb;

    // Direction (esta debe de tenerla el enemigo pero no el player)
    Vector2 dir;

    // Contador para siguiente movimiento
    int contNextMovement;
    [ContextMenu("ObtenerDatos()")]

    void ObtenerDatos()
    {
        Debug.Log(movemenD.Speed.Count);
        movSpeeds = movemenD.Speed;
    }
    void GetTypeMovement()
    {
        /*switch (movemenD.MovementType[contNextMovement])
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
                dir = new Vector2(1, -1).normalized;
                break;
            case 7:// Direccion abajo derecha
                dir = new Vector2(-1, -1).normalized;
                break;
            case 8:// Sin direccion, estatico
                dir = Vector2.zero;
                break;
        }*/
    }
    void Movement()
    {
        rb.velocity = dir * movemenD.Speed[contNextMovement];
    }
    IEnumerator NextMovement()
    {
        yield return new WaitForSeconds(movemenD.NextMovementTypeTime[contNextMovement]);
        contNextMovement++;
        //StopCoroutine(NextMovement());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Limits"))
        {
            dir = Vector2.zero;
        }
    }
}
