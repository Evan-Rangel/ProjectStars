using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyAttackScript attackScript;
    [SerializeField] EnemyMovementScript movementScript;
    [SerializeField] int healt;
    private void Awake()
    {
        attackScript = GetComponent<EnemyAttackScript>();
        movementScript = GetComponent<EnemyMovementScript>();
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
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        //WaveController.InstanceW.CheckWave();

    }
}
