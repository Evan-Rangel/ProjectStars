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
    [SerializeField] Material[] brillo;
    [SerializeField] Color[] color_;
    
    BoxCollider2D coll;
    Rigidbody2D rb;
    private void Awake()
    {
        brillo[0] = GetComponent<Renderer>().material;
        attackScript = GetComponent<EnemyAttackScript>();
        movementScript = GetComponent<EnemyMovementScript>();
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameObject.GetComponent<Renderer>().material = brillo[0];

        //animator.runtimeAnimatorController = enemyD.NewController;
    }
    private void Start()
    {
        gameObject.GetComponent<Renderer>().material = brillo[0];

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
        StartCoroutine(Brillo());
        if (healt<=0)
        {
            GameObject.FindGameObjectWithTag("UIPlayer").GetComponent<W_UIPlayer>().SetScore(enemyD.GetScore);
            attackScript.ResetValues();
            movementScript.ResetValues();
            attackScript.StopAllCoroutines();
            movementScript.StopAllCoroutines();
            // gameObject.SetActive(false);
            animator.SetBool("Morir", true);
            StartCoroutine(Deshabilitar());
        }
    }
    IEnumerator Brillo()
    {
        gameObject.GetComponent<Renderer>().material = brillo[1];
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().material = brillo[0];

    }
    IEnumerator Deshabilitar()
    {
        

        yield return new WaitForSeconds(0.46f);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        gameObject.GetComponent<Renderer>().material = brillo[0];
        animator.SetBool("Morir", false);

        WaveController.InstanceW.CheckWave();
    }
}
