using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    // Scriptable object
    [SerializeField] List<EnemyMovementData> movemenData;


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
        //GetTypeMovement();

    }
    private void OnEnable()
    {
        //GetTypeMovement();
    }
    public void StartMovement()
    {
        StopAllCoroutines();
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

    public int TotalMovementData { get { return movemenData.Count; } }
    public int CurrentMovementPattern {  get  {  return currentMovementPattern; } }
    public void SetCurrentMovemetnPattern(int _nextMovementPattern)
    {
        currentMovementPattern = _nextMovementPattern;
        ResetValues();
    
    }
    public void SetMovementData(List<EnemyMovementData> _movementData)
    {
        movemenData = _movementData;
        ResetValues();
    }
    public void ResetValues()
    {
        currentMovement = 0;
        StopAllCoroutines();
       // GetTypeMovement();
    }
    void GetTypeMovement()
    {
        switch (movemenData[currentMovementPattern].GetMovementType)
        {
            case EnemyMovementData.MovType.FOLLOW_PLAYER:
                FollowPlayerMovement();
                break;
            case EnemyMovementData.MovType.ONE_MOV:
                rb.velocity = movemenData[currentMovementPattern].GetDirection_OM * movemenData[currentMovementPattern].GetSpeed_OM;
                break;
            case EnemyMovementData.MovType.CUSTOM_MOVEMENT:
                CustomMovement();
                break;
            case EnemyMovementData.MovType.NONE:
                break;
        }
    }
    void FollowPlayerMovement()
    {
        //rb.AddForce((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * movemenData[currentMovementPattern].GetForce_FP);
        rb.velocity = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * movemenData[currentMovementPattern].GetForce_FP;
        StartCoroutine(FollowPlayerTime()); ;
    }
    IEnumerator FollowPlayerTime()
    {
        yield return new WaitForSeconds(movemenData[currentMovementPattern].GetTime_FP);
        FollowPlayerMovement();
    }
    void CustomMovement()
    {
        if (currentMovement>=movemenData[currentMovementPattern].GetMovementCount)
        {
            currentMovement = 0;
            /*if (movemenData[currentMovementPattern].CycleMovement)
            {
            }
            else
            {
                currentMovement = 0;
                rb.velocity = Vector2.zero;
            }*/
        }
        rb.velocity = movemenData[currentMovementPattern].GetMovement[currentMovement].dir * movemenData[currentMovementPattern].GetMovement[currentMovement].speed;
        StartCoroutine(CustomMovementTime());

    }
    IEnumerator CustomMovementTime()
    {
        yield return new WaitForSeconds(movemenData[currentMovementPattern].GetMovement[currentMovement].time);
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
    public void StopCorrutines()
    {
        StopAllCoroutines();
    }
}
