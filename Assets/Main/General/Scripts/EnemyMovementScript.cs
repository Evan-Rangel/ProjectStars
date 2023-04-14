using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    // Scriptable object
    [SerializeField] EnemyMovementData[] movemenData;



    // Rigybody
    Rigidbody2D rb;

    // Direction (esta debe de tenerla el enemigo pero no el player)
    Vector2 dir;

    // Contador para siguiente movimiento
    int contNextMovement;
    int contMovementData;

    private void Awake()
    {
        contNextMovement = 0;
        contMovementData = 0;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetTypeMovement();
        }
    }

    void GetTypeMovement()
    {
        switch (movemenData[contMovementData].MovementType)
        {
            case 0://Follow the player
                rb.velocity = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized*movemenData[contMovementData].Aceleration_FP;
                FollowPlayerMovement();
                break;
            case 1:
                break;
            case 2:
                CustomMovement();
                break;
        }
    }
    void FollowPlayerMovement()
    {
        StartCoroutine(FollowPlayerTime()); ;
    }
    IEnumerator FollowPlayerTime()
    {
        yield return new WaitForSeconds(movemenData[contMovementData].Time_FP);
        Debug.Log("Explotar");
        //StopCoroutine(FollowPlayerTime());
    }
    void CustomMovement()
    {
        if (contNextMovement<movemenData[contMovementData].MovementCount)
        {
            Debug.Log(contNextMovement +",  " + movemenData[contMovementData].MovementCount);
            rb.velocity = movemenData[contMovementData].Direction[contNextMovement] * movemenData[contMovementData].Speed[contNextMovement];
            StartCoroutine(CustomMovementTime());
        }
        else
        {
            if (movemenData.Length> contMovementData)
            {
                contMovementData++;
                contNextMovement = 0;
            }
            else
            {
                contNextMovement++;
            }
        }
    }
    IEnumerator CustomMovementTime()
    {
        Debug.Log(movemenData[contMovementData].Time[contNextMovement]);
        yield return new WaitForSeconds(movemenData[contMovementData].Time[contNextMovement]);
        contNextMovement++;
        CustomMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Limits"))
        {
            dir = Vector2.zero;
        }
    }
}
