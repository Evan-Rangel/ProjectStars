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

    /* Para movimiento en petalos (igualar el "basePos" a la pos inicial del enemigo)
    [SerializeField] GameObject patron;
    Vector2 basePos;
    Vector2 newPos;
    bool startMoving = false;
    float timeCounter=0;
    */
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

        /* Para movimiento en forma de petalo
        newPos = new Vector2(3 * Mathf.Cos(2 * timeCounter) * Mathf.Cos(timeCounter) + basePos.x, 3 * Mathf.Cos(2 * timeCounter) * Mathf.Sin(timeCounter) + basePos.y);
        if (Vector2.Distance(transform.position, newPos) < 0.1f && !startMoving)
        {
            startMoving = true;
            
        }
        if (startMoving)
	    {
            timeCounter += Time.deltaTime;
            //Ecuacion para petalos
            transform.position = newPos;
            Instantiate(patron, transform.position, Quaternion.identity);
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, newPos, Time.deltaTime*2);
        }
        if (timeCounter>6.28f)
        {
            timeCounter = 0;
        }
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // GetTypeMovement();
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
