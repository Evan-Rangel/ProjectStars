using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    // Scriptable object
    [SerializeField] EnemyMovementData movemenData;
    [SerializeField] List<float> movSpeeds;
    [SerializeField] List<Vector2> movDirections;
    [SerializeField] List<float> movTimes;


    // Rigybody
    Rigidbody2D rb;

    // Direction (esta debe de tenerla el enemigo pero no el player)
    Vector2 dir;

    // Contador para siguiente movimiento
    int contNextMovement;
    [ContextMenu("ObtenerDatos()")]
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetTypeMovement();
        }
    }
    void ObtenerDatos()
    {
        Debug.Log(movemenData.Speed.Count);
        movSpeeds = movemenData.Speed;
        movDirections = movemenData.Direction;
        movTimes = movemenData.Time;
    }
    void GetTypeMovement()
    {
        Debug.Log("Mov");
        switch (movemenData.MovementType)
        {
            case 0://Follow the player
                rb.velocity = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
                //rb.AddForce((GameObject.FindGameObjectWithTag("Player").transform.position-transform.position)/0.01f);
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
    void Movement()
    {
        rb.velocity = dir * movemenData.Speed[contNextMovement];
    }
    IEnumerator NextMovement()
    {
        yield return new WaitForSeconds(movemenData.Time[contNextMovement]);
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
