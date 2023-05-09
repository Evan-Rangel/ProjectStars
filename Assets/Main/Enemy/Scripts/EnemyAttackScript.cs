using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    [SerializeField] EnemyAttackData[] attackData;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject laser;
    List<GameObject> lasers;
    GameObject laserInstance;
    [SerializeField] int currentShotPattern = 0;
    GameObject objectToRotate;
    bool lasera=false;
    float anglesum=0;
    private void Start()
    {

        //GetShotType();
    }
    private void OnEnable()
    {
        GetShotType();

    }
    private void Update()
    {
        if (lasera)
        {
            Laser(anglesum);
            anglesum +=Time.deltaTime * attackData[currentShotPattern].GetLaserSpeedRotation;
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
                Shooting(anglesum);
                break;
            case EnemyAttackData.AttackType.LASER:
                //Laser();
                switch (attackData[currentShotPattern].GetLaserType)
                {
                    case EnemyAttackData.LaserType.STATIC:
                        break;
                    case EnemyAttackData.LaserType.DINAMIC:
                       // laserInstance = Instantiate(laser,transform.position, Quaternion.identity);
                        for (int i = 0; i < attackData[currentShotPattern].GetLaserPerWave; i++)
                        {
                            lasers.Add(Instantiate(laser, transform.position, Quaternion.identity, transform));
                        }
                        lasera = true;
                        break;
                    case EnemyAttackData.LaserType.SWITCH:
                        break;
                    case EnemyAttackData.LaserType.CUSTOM:
                        break;
                    case EnemyAttackData.LaserType.RANDOM:
                        break;
                }
                break;
        }
    }

    void Laser(float angleSum)
    {
        float angleStep = 360 / attackData[currentShotPattern].GetLaserPerWave;
        float angle = attackData[currentShotPattern].GetLaserAngleInit + angleSum;
        Vector2 startPoint = transform.position;
        float rayDistance;

        
        for (int i = 0; i < attackData[currentShotPattern].GetLaserPerWave; i++)
        {
            Vector2 projectileMoveDirection = GenerateRotation(angle, 1, startPoint).normalized;

            RaycastHit2D hit= Physics2D.Raycast(transform.position, projectileMoveDirection);
            if (hit.collider!=null && hit.distance<10)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    //Funcion del player para hacer danio
                }
                rayDistance = hit.distance;
            }
            else
            {
                rayDistance = 10;
            }


            lasers[i].GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
            lasers[i].GetComponent<LineRenderer>().SetPosition(1, projectileMoveDirection*rayDistance);

            //laserInstance.GetComponent<LineRenderer>().SetPosition(0,startPoint);
            //laserInstance.GetComponent<LineRenderer>().SetPosition(1, projectileMoveDirection * rayDistance);
            Debug.DrawRay(startPoint,projectileMoveDirection*rayDistance, Color.green);
            angle += angleStep;
        }
    }

    //Funcion para projectiles
    void Shooting(float angleSum)
    {
        float angleStep = 360 / attackData[currentShotPattern].GetProjectilesPerWave;
        float angle = attackData[currentShotPattern].GetProjectileAngleInit+anglesum;
        Vector2 startPoint = transform.position;


        for (int i = 0; i < attackData[currentShotPattern].GetProjectilesPerWave; i++)
        {
            angle += angleStep;
            GameObject tmpObj = BulletsPool.Instance.RequestLaser();//Instantiate(projectile, startPoint, Quaternion.identity);
            Vector2 vel = GenerateRotation(angle, attackData[currentShotPattern].GetProjectileSpeed, startPoint);
            tmpObj.GetComponent<Bullets>().SetProps(vel, startPoint, -angle);


            //tmpObj.GetComponent<Rigidbody2D>().velocity = GenerateRotation(angle, attackData[currentShotPattern].GetProjectileSpeed, startPoint);
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

    //Gizmo para ver la direccion de lasers de momento
    private void OnDrawGizmos()
    {
        if (attackData[currentShotPattern].GetAttackType== EnemyAttackData.AttackType.LASER && attackData[currentShotPattern].GetLaserType == EnemyAttackData.LaserType.CUSTOM)
        {
            for (int i = 0; i < attackData[currentShotPattern].GetLaserAngles.Count; i++)
            {
                Gizmos.DrawLine(transform.position, GenerateRotation(attackData[currentShotPattern].GetLaserAngles[i], 10, transform.position));
            }
        }
    }
}
