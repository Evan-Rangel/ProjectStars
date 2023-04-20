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
    int currentMovement;
    [SerializeField]int currentMovementPattern;

    private void Awake()
    {
        currentMovement = 0;
        currentMovementPattern = 0;
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Start()
    {
        GetTypeMovement();

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

    public int TotalMovementData { get { return movemenData.Length; } }
    public int CurrentMovementPattern {  get  {  return currentMovementPattern; } }
    public void SetCurrentMovemetnPattern(int _nextMovementPattern)
    {
        currentMovement = 0;
        currentMovementPattern = _nextMovementPattern;
        StopAllCoroutines();
        GetTypeMovement();
    }


    void GetTypeMovement()
    {
        switch (movemenData[currentMovementPattern].GetMovementType)
        {
            case 0://Follow the player
                Vector2 posRelativeToPlayer= GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
                float aceleration = movemenData[currentMovementPattern].GetAceleration_FP;
                rb.velocity = posRelativeToPlayer.normalized * aceleration;
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
        yield return new WaitForSeconds(movemenData[currentMovementPattern].GetTime_FP);
    }
    void CustomMovement()
    {
        if (currentMovement>=movemenData[currentMovementPattern].GetMovementCount)
        {
            currentMovement = 0;
        }
        Vector2 direction = movemenData[currentMovementPattern].GetDirection[currentMovement];
        float speed = movemenData[currentMovementPattern].GetSpeed[currentMovement];
        rb.velocity = direction * speed;
        StartCoroutine(CustomMovementTime());
    }
    IEnumerator CustomMovementTime()
    {
        yield return new WaitForSeconds(movemenData[currentMovementPattern].GetTime[currentMovement]);
        currentMovement++;
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
