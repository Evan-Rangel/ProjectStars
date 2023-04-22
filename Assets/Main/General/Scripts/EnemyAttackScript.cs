using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    [SerializeField] EnemyAttackData[] attackData;
    [SerializeField] GameObject projectile;
    [SerializeField] int currentShotPattern = 0;


    private void Start()
    {

        GetShotType();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Shooting(shotData[currentShotPattern].ProjectileAngleInit, shotData[currentShotPattern].ProjectilesPerWave);
        }
    }
    public int TotalShotData { get { return attackData.Length; } }
    public int CurrentShotPattern { get { return currentShotPattern; } }

    public void SetCurrentShotPattern(int _nextShotPattern)
    {
        currentShotPattern = _nextShotPattern;
    }

    void GetShotType()
    {
        switch (attackData[currentShotPattern].GetAttackType)
        {
            case EnemyAttackData.AttackType.NONE:
                break;
            case EnemyAttackData.AttackType.BULLET:
                Shooting();
                break;
            case EnemyAttackData.AttackType.LASER:

                break;
            
        }
    }

    void Laser()
    {
        float angleStep = 360 / attackData[currentShotPattern].GetProjectilesPerWave;
        float angle = attackData[currentShotPattern].GetProjectileAngleInit;
        Vector2 startPoint = transform.position;

        for (int i = 0; i < attackData[currentShotPattern].GetLaserPerWave; i++)
        {

        }
    }

    void Shooting()
    {
        float angleStep = 360 / attackData[currentShotPattern].GetProjectilesPerWave;
        float angle = attackData[currentShotPattern].GetProjectileAngleInit;
        Vector2 startPoint = transform.position;

        for (int i = 0; i < attackData[currentShotPattern].GetProjectilesPerWave; i++)
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180);
            Vector2 projectileVector = new Vector2(projectileDirXPosition, projectileDirYPosition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * attackData[currentShotPattern].GetProjectileSpeed;

            GameObject tmpObj = Instantiate(projectile, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody2D>().velocity = projectileMoveDirection;

            angle += angleStep;
        }
        StartCoroutine(ShootingTimer());
    }

    IEnumerator ShootingTimer()
    {
        yield return new WaitForSeconds(attackData[currentShotPattern].GetProjectileCadence);
        Shooting();
    }
}
