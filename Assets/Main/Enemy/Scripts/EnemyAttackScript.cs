using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    [SerializeField] List<EnemyAttackData> attackData;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject laser;
    [SerializeField] List<GameObject> lasers;
    GameObject laserInstance;
    [SerializeField] int currentShotPattern = 0;
    GameObject objectToRotate;
    bool lasera = false;
    float anglesum = 0;
    Collider2D coll;
    List<float> laserAngles;
    public int TotalShotData { get { return attackData.Count; } }
    public int CurrentShotPattern { get { return currentShotPattern; } }



    public void SetCurrentShotPattern(int _nextShotPattern)
    {
        currentShotPattern = _nextShotPattern;
    }
    public void SetAttackData(List<EnemyAttackData> _attackData)
    {
        attackData = _attackData;
        ResetValues();
    }
    public void StopCorrutines()
    {
        StopAllCoroutines();
    }
    public void StartAttack()
    {
        if (attackData.Count > 0)
        {
            coll = GetComponent<Collider2D>();
            StopAllCoroutines();
            GetShotType();
        }
    }
    public void ResetValues()
    {
        currentShotPattern = 0;
        anglesum = 0;
        if (lasers.Count > 0)
        {
            for (int i = 0; i < lasers.Count; i++)
            {
                Destroy(lasers[i]);
            }
            lasers.Clear();
        }
        StopAllCoroutines();
        //GetShotType();
    }
    void GetShotType()
    {
        switch (attackData[currentShotPattern].GetAttackType)
        {
            case EnemyAttackData.AttackType.NONE:
                break;
            case EnemyAttackData.AttackType.BULLET:
                Shooting(anglesum);
                break;
            case EnemyAttackData.AttackType.LASER:
                //Laser();
                switch (attackData[currentShotPattern].GetLaserType)
                {
                    case EnemyAttackData.LaserType.STATIC:
                        for (int i = 0; i < attackData[currentShotPattern].GetLaserPerWave; i++)
                        {
                            GameObject _laser = Instantiate(laser, transform.position, Quaternion.identity, transform);
                            lasers.Add(_laser);
                            _laser.GetComponent<LaserController>().CastLaserFunc(attackData[currentShotPattern].GetLaserCastSpeed);
                        }
                        Laser(anglesum);

                        break;
                    case EnemyAttackData.LaserType.DINAMIC:
                        for (int i = 0; i < attackData[currentShotPattern].GetLaserPerWave; i++)
                        {
                            if (i >= lasers.Count)
                            {
                                lasers.Add(Instantiate(laser, transform.position, Quaternion.identity, transform));
                            }
                        }
                        Laser(anglesum);
                        break;
                    case EnemyAttackData.LaserType.SWITCH:
                        for (int i = 0; i < attackData[currentShotPattern].GetLaserPerWave; i++)
                        {
                            GameObject _laser = Instantiate(laser, transform.position, Quaternion.identity, transform);
                            lasers.Add(_laser);
                            _laser.GetComponent<LaserController>().SwtichLaserFunc(attackData[currentShotPattern].GetLaserOffDuration, attackData[currentShotPattern].GetLaserOnDuration, attackData[currentShotPattern].GetLaserCastSpeed);

                        }
                        Laser(anglesum);
                        break;
                    case EnemyAttackData.LaserType.CUSTOM:
                        for (int i = 0; i < attackData[currentShotPattern].GetLaserPerWave; i++)
                        {
                            GameObject _laser = Instantiate(laser, transform.position, Quaternion.identity, transform);
                            lasers.Add(_laser);
                            _laser.GetComponent<LaserController>().CastLaserFunc(attackData[currentShotPattern].GetLaserCastSpeed);

                        }
                        LaserCustom(anglesum);
                        break;
                    case EnemyAttackData.LaserType.RANDOM:
                        laserAngles = new List<float>();
                        CreateRandomLaser();
                        LaserRandom();
                        break;
                    case EnemyAttackData.LaserType.CAST_TO_PLAYER:
                        LaserCastToPlayer();
                        StartCoroutine( LaserToPlayer());
                        break;

                }
                break;
        }
    }

    void LaserCastToPlayer()
    {
        GameObject _laser = Instantiate(laser, transform.position, Quaternion.identity, transform);
        lasers.Add(_laser);
        _laser.GetComponent<LaserController>().StartFPCorr(attackData[currentShotPattern].GetLaserOffDuration, attackData[currentShotPattern].GetLaserRandomOnTime, attackData[currentShotPattern].GetLaserCastSpeed);
        StartCoroutine(LaserCastToPlayerTimer());
    }
    IEnumerator LaserToPlayer()
    {
        float rayDistance;
        bool isHit = false;
        Vector2 startPoint = transform.position;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 playerPos = ( (Vector2)player.transform.position- startPoint).normalized;
        for (int i = 0; i < lasers.Count; i++)
        {
            if (lasers[i].activeSelf)
            {
                Vector2 laserDirection = lasers[i].GetComponent<LaserController>().FPRaycastDirection(playerPos);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, laserDirection);
                if (hit.collider != null && hit.distance < attackData[currentShotPattern].GetLaserDistance)
                {
                    if (hit.transform.CompareTag("Player") && lasers[i].GetComponent<LaserController>().GetCanDamage)
                    {
                        hit.transform.gameObject.GetComponent<Player>().RecibirDanio(attackData[currentShotPattern].GetLaserDamage);

                        Debug.Log("Damage to player");
                    }
                    rayDistance = hit.distance;
                    isHit = true;
                }
                else
                {
                    isHit = false;
                    rayDistance = attackData[currentShotPattern].GetLaserDistance;
                }
                lasers[i].GetComponent<LaserController>().LaserRaycast(laserDirection * rayDistance, isHit);

            }
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine(LaserToPlayer());
    }
    
    IEnumerator LaserCastToPlayerTimer()
    {
        yield return new WaitForSeconds(attackData[currentShotPattern].GetLaserRandomTime);
        LaserCastToPlayer();
    }
    void LaserRandom()
    {
        float rayDistance;
        bool isHit;
        Vector2 startPoint = transform.position;

        for (int i = 0; i < lasers.Count; i++)
        {
            if (lasers[i].activeSelf)
            {

                Vector2 projectileMoveDirection = GenerateRotation(laserAngles[i], 1, startPoint).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, projectileMoveDirection);

                if (hit.collider != null && hit.distance < attackData[currentShotPattern].GetLaserDistance)
                {
                    if (hit.transform.CompareTag("Player") && lasers[i].GetComponent<LaserController>().GetCanDamage)
                    {
                        hit.transform.gameObject.GetComponent<Player>().RecibirDanio(attackData[currentShotPattern].GetLaserDamage);
                        Debug.Log("Damage to player");
                    }
                    rayDistance = hit.distance;
                    isHit = true;
                }
                else
                {
                    isHit = false;
                    rayDistance = attackData[currentShotPattern].GetLaserDistance;
                }
                lasers[i].GetComponent<LaserController>().LaserRaycast(projectileMoveDirection * rayDistance, isHit);
            }
        }
     
        StartCoroutine(LaserRandomUpdate());
   

    }

    void CreateRandomLaser()
    {
       
        GameObject _laser = Instantiate(laser, transform.position, Quaternion.identity, transform);
        lasers.Add(_laser);
        float angle = Random.Range(0, 360);
        laserAngles.Add(angle);
        _laser.GetComponent<LaserController>().LaserRandomFunc(attackData[currentShotPattern].GetLaserRandomOnTime, attackData[currentShotPattern].GetLaserOffDuration, attackData[currentShotPattern].GetLaserCastSpeed);
        StartCoroutine(LaserRandomTimer());
    }


    IEnumerator LaserRandomUpdate()
    {
        yield return new WaitForEndOfFrame();
        LaserRandom();
    }
    IEnumerator LaserRandomTimer()
    {
        yield return new WaitForSeconds(attackData[currentShotPattern].GetLaserRandomTime);
        CreateRandomLaser();
    }
    void LaserCustom(float _angleSum)
    {
        Vector2 startPoint = transform.position;
        float rayDistance;
        bool isHit;
        float angle = _angleSum;

        for (int i = 0; i < lasers.Count; i++)
        {
            Vector2 projectileMoveDirection = GenerateRotation(attackData[currentShotPattern].GetLaserAngles[i]+angle, 1, startPoint).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, projectileMoveDirection);
            if (hit.collider != null && hit.distance < attackData[currentShotPattern].GetLaserDistance)
            {
                if (hit.transform.CompareTag("Player") && lasers[i].GetComponent<LaserController>().GetCanDamage)
                {
                    hit.transform.gameObject.GetComponent<Player>().RecibirDanio(attackData[currentShotPattern].GetLaserDamage);

                    Debug.Log("Damage to player");
                }
                rayDistance = hit.distance;
                isHit = true;
            }
            else
            {
                isHit = false;
                rayDistance = attackData[currentShotPattern].GetLaserDistance;
            }
            lasers[i].GetComponent<LaserController>().LaserRaycast(projectileMoveDirection * rayDistance, isHit);
        }
        StartCoroutine(LaserCustomTimerRotation());

    }

    void Laser(float _angleSum)
    {
        float angleStep = 360 / attackData[currentShotPattern].GetLaserPerWave;
        float angle = attackData[currentShotPattern].GetLaserAngleInit + _angleSum;
        Vector2 startPoint = transform.position;
        float rayDistance;
        bool isHit;
        
        for (int i = 0; i < lasers.Count; i++)
        {
            Vector2 projectileMoveDirection = GenerateRotation(angle, 1, startPoint).normalized;
            RaycastHit2D hit= Physics2D.Raycast(transform.position, projectileMoveDirection);
            
            if (hit.collider!=null && hit.distance<attackData[currentShotPattern].GetLaserDistance )
            {
                if (hit.transform.CompareTag("Player") && lasers[i].GetComponent<LaserController>().GetCanDamage)
                {
                    hit.transform.gameObject.GetComponent<Player>().RecibirDanio(attackData[currentShotPattern].GetLaserDamage);

                    Debug.Log("Damage to player");
                }
                rayDistance = hit.distance;
                isHit = true;
            }
            else
            {
                isHit = false;
                rayDistance = attackData[currentShotPattern].GetLaserDistance;
            }
            lasers[i].GetComponent<LaserController>().LaserRaycast(projectileMoveDirection * rayDistance, isHit);


           // Debug.DrawRay(startPoint,projectileMoveDirection*rayDistance, Color.green);
            angle += angleStep;
        }
        StartCoroutine(LaserTimerRotation());
    }

    //Funcion para projectiles
    void Shooting(float _angleSum)
    {
        float angleStep = attackData[currentShotPattern].GetAngleToShot / attackData[currentShotPattern].GetProjectilesPerWave;
        float angle = attackData[currentShotPattern].GetProjectileAngleInit + _angleSum;
        Vector2 startPoint = transform.position;

        GameObject bullet;
        for (int j = 0; j < attackData[currentShotPattern].GetAnglesSteps; j++)
        {
            for (int i = 0; i < attackData[currentShotPattern].GetProjectilesPerWave; i++)
            {
                angle += angleStep;
                Vector2 vel = GenerateRotation(angle, attackData[currentShotPattern].GetProjectileSpeed, startPoint);
                bullet = BulletsPool.Instance.RequestEnemyBullet();
                bullet.GetComponent<Bullets>().SetProps(vel, startPoint, -angle);
                if (attackData[currentShotPattern].GetProjectileRotation != 0)
                {
                    bullet.GetComponent<Bullets>().GenerateRotation(attackData[currentShotPattern].GetProjectileRotation + angle, attackData[currentShotPattern].GetProjectileTimeRot, attackData[currentShotPattern].GetProjectileSpeed, attackData[currentShotPattern].GetProjectileRotation);
                }
            }
        }
        
        StartCoroutine(ShootingTimer());
    }

    //Funcion para la rotacion (en el vectorlenght se pone ya sea la velocidad del projectil o la distancia del laser)
    private Vector2 GenerateRotation(float _angle, float vectorLength, Vector2 _startPoint)
    {
        float DirXPosition = _startPoint.x + Mathf.Sin((_angle * Mathf.PI) / 180);
        float DirYPosition = _startPoint.y + Mathf.Cos((_angle * Mathf.PI) / 180);
        Vector2 Vector = new Vector2(DirXPosition, DirYPosition);
        Vector2 MoveDirection = (Vector - _startPoint).normalized * vectorLength;
        return MoveDirection;
    }

    //Corrutina para la cadencia de disparo
    IEnumerator ShootingTimer()
    {
        yield return new WaitForSeconds(attackData[currentShotPattern].GetProjectileCadence);
        anglesum += attackData[currentShotPattern].GetProjectileAngleSum;
        
        Shooting(anglesum);
    }
    IEnumerator LaserTimerRotation()
    {
        yield return new WaitForEndOfFrame();
        anglesum += Time.deltaTime * attackData[currentShotPattern].GetLaserSpeedRotation;
        Laser(anglesum);

    }
    IEnumerator LaserCustomTimerRotation()
    {
        yield return new WaitForEndOfFrame();
        anglesum += Time.deltaTime * attackData[currentShotPattern].GetLaserSpeedRotation;
        LaserCustom(anglesum);

    }
    /*
    //Gizmo para ver la direccion de lasers de momento
    private void OnDrawGizmos()
    {
        if (attackData[currentShotPattern].GetAttackType== EnemyAttackData.AttackType.LASER && attackData[currentShotPattern].GetLaserType == EnemyAttackData.LaserType.DINAMIC)
        {
            for (int i = 0; i < attackData[currentShotPattern].GetLaserAngles.Count; i++)
            {
                Gizmos.DrawLine(transform.position, GenerateRotation(attackData[currentShotPattern].GetLaserAngles[i], 10, transform.position));
            }
        }
    }*/
}
