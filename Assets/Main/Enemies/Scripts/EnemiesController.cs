using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    //EnemiesData
    [Header("Enemy Data")]
    [Tooltip("Aqui va el Scriptable object del enemigo correspondiente")]
    [SerializeField] EnemiesData enemyData;
    //Enemy Atributes
    [Header("Enemy Atributes")]
    [SerializeField] public int enemyHealth;
    //Variables de los tipos de disparos
    [SerializeField] public int bulletInitialAngle;
    [SerializeField] public int angleSum;
    Vector2 startPoint;
    const float radius = 1;
    float timer;
    int cont = 0;
    int mult = 1;
    //PolygonCollider del enemigo
    private PolygonCollider2D colliderEnemigo;
    //Animator y animacion de morir
    public Animator animator;
    [SerializeField] private AnimationClip animacionMorir;
    //Animacion de Parpadeo
    public float tiempo_brillo;
    public SpriteRenderer[] spr;
    public bool cambio;
    public Color[] color_;
    public float speed_shine;
    public float cronometro;

    //Mamadas
    public GameObject[] arregleoFE;

    private void Start()
    {
        //Variables para uso
        enemyHealth = enemyData.EnemyHealth;
        bulletInitialAngle = enemyData.BulletInitialAngle;
        angleSum = enemyData.BulletAngleSum;
        timer = enemyData.BulletTimer;
        //Tomar componentes
        colliderEnemigo = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Parpadeo de recibir daño
        Brillo();
        //Enemigo Muere
        EnemyDie();
    }

    private void FixedUpdate()
    {
        //Tipos de disparo Enemy
        EnemyProyectileTypes();       
    }

    //Funciones Enemy
    //Enemy Recibir Daño Funcion
    public void RecibirDanio(int danio)
    {
        enemyHealth = enemyHealth - danio;
        cronometro = 0.3f;
    }

    //Enemy Die Funcion
    private void EnemyDie()
    {
        //Enemy Die
        if (enemyData.EnemyHealth <= 0)
        {
            animator.SetBool("Muerto", true);
            colliderEnemigo.enabled = false;
            StartCoroutine(EnemyDesactivar());
        }
    }

    private void EnemyProyectileTypes()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0)
        {

            switch (enemyData.AttackType)
            {
                case 1:
                case 2://Ataque 1 y 2, solo dependen del bulletAngleSum
                    SpawnProjectiles(enemyData.BulletInitialAngle, 360 / enemyData.NumberOfProjectiles);
                    bulletInitialAngle += angleSum;
                    timer = enemyData.BulletTimer;
                    break;
                case 3://Ataque 3 es petalo

                    SpawnProjectiles(enemyData.BulletInitialAngle, 360 / enemyData.NumberOfProjectiles);
                    SpawnProjectiles(-enemyData.BulletInitialAngle, 360 / enemyData.NumberOfProjectiles);
                    bulletInitialAngle += angleSum;

                    //angleSumI -= enemyData.BulletAngleSum;
                    timer = enemyData.BulletTimer;
                    break;
                case 4: //Ataque 4 es vuelta y regreso
                    if (cont == 6)
                    {
                        mult = -mult;
                        cont = 0;
                    }
                    SpawnProjectiles(enemyData.BulletInitialAngle, 360 / enemyData.DistanceBetweenProjectiles);
                    bulletInitialAngle += mult * angleSum;
                    cont++;
                    timer = enemyData.BulletTimer;
                    break;
                default:
                    break;
            }
            timer = enemyData.BulletTimer;
        }
    }

    //Movimiento, rotacion, posicion y Spawneo de balas Funcion
    private void SpawnProjectiles(int addAngle, float _angleStep)
    {
        float angleStep = _angleStep;
        float angle = addAngle;
        startPoint = transform.position;

        for (int i = 0; i < enemyData.NumberOfProjectiles; i++)
        {

            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
            Vector2 projectileVector = new Vector2(projectileDirXPosition, projectileDirYPosition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * enemyData.ProjectileSpeed;

            GameObject tmpObj = BulletsPool.Instance.RequestLaser();
            tmpObj.GetComponent<Bullets>().SetProps(projectileMoveDirection, transform.position, -angle, enemyData.BulletData);

            angle += angleStep;

        }
    }

    //Brilo de daño Funcion
    public void Brillo()
    {
        if (cronometro > 0)
        {
            cronometro -= 1 * Time.deltaTime;
            spr[1].sprite = spr[0].sprite;
            tiempo_brillo += speed_shine * Time.deltaTime;

            switch (cambio)
            {
                case true:

                    spr[1].color = color_[0];
                    break;

                case false:
                    spr[1].color = color_[1];
                    break;
            }

            if (tiempo_brillo > 1)
            {
                cambio = !cambio;
                tiempo_brillo = 0;
            }
        }

        else
        {
            cronometro = 0;
            spr[1].color = color_[0];
        }
    }

    //Corrutinas Enemy
    //Corrutina Enemy Desactivar cuando muere
    IEnumerator EnemyDesactivar()
    {
        yield return new WaitForSeconds(animacionMorir.length);
        gameObject.SetActive(false);
    }

    public void ComohacerForEach()
    {
        foreach (var recorrer in arregleoFE)
        {
            recorrer.gameObject.SetActive(false);
        }
    }

}
