using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyAttackScript attackScript;
    [SerializeField] EnemyMovementScript movementScript;
    [SerializeField] int healt;
    [SerializeField] EnemyData enemyD;
    [SerializeField] Animator animator; 
    BoxCollider2D coll;
    Rigidbody2D rb;
    private void Awake()
    {
        attackScript = GetComponent<EnemyAttackScript>();
        movementScript = GetComponent<EnemyMovementScript>();
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //animator.runtimeAnimatorController = enemyD.NewController;
    }
    public Rigidbody2D GetRB { get { return rb; } }
    public void SetEnemyData(EnemyData _data)
    {
        enemyD = _data;
        attackScript.SetAttackData(enemyD.GetEnemyAttackData);
        movementScript.SetMovementData(enemyD.GetEnemyMovementData);
        animator.runtimeAnimatorController = enemyD.NewController;

        coll.enabled = false;
    }
    public void ActiveComponents()
    {
        healt = enemyD.GetHealth;
        attackScript.StartAttack();
        movementScript.StartMovement();
        coll.enabled = true;
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            NextShotPattern();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            NextMovementPattern();
        }*/
    }

    void NextMovementPattern()
    {
        if (movementScript.TotalMovementData> movementScript.CurrentMovementPattern + 1)
        {
            movementScript.SetCurrentMovemetnPattern(movementScript.CurrentMovementPattern + 1);
        }
        else
        {
            Debug.Log("No more MovementPatterns");
        }
    }
    void NextShotPattern()
    {
        if (attackScript.TotalShotData > attackScript.CurrentShotPattern + 1)
        {
            attackScript.SetCurrentShotPattern(attackScript.CurrentShotPattern + 1);
        }
        else
        {
            Debug.Log("No more ShotPatterns");
        }
    }
    public void DealDamage(int _damage)
    {
        healt -= _damage;
        if (healt<=0)
        {
            attackScript.ResetValues();
            movementScript.ResetValues();
            attackScript.StopAllCoroutines();
            movementScript.StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        WaveController.InstanceW.CheckWave();
    }
}
