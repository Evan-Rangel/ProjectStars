using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyShotScript shotScript;
    [SerializeField] EnemyMovementScript movementScript;
    [SerializeField] int healt;
    private void Awake()
    {
        shotScript = GetComponent<EnemyShotScript>();
        movementScript = GetComponent<EnemyMovementScript>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextShotPattern();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            NextMovementPattern();
        }
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
        if (shotScript.TotalShotData > shotScript.CurrentShotPattern + 1)
        {
            shotScript.SetCurrentShotPattern(shotScript.CurrentShotPattern + 1);
        }
        else
        {
            Debug.Log("No more ShotPatterns");
        }
    }
}
