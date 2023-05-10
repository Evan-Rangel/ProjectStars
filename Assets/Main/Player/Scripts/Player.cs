using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class Player : MonoBehaviour
{
    [Header("Player Controller")]
    [Space]
    //Limites del Jugador en vista Top Down cuando esta en manejando la nave
    [Header("Player Limits Top Down Nave")]
    public Boundary boundary;
    //Player Variables
    [Header("Player Atributes")]
    [SerializeField] int lifePlayer;
    public int LifePlayer => lifePlayer;
    [SerializeField] private float speedPlayer;
    private PolygonCollider2D colliderPlayer;
    private Rigidbody2D rbPlayer;
    float moveX;
    float moveY;
    private Vector2 moveInput;
    bool playerMuere = false;
    bool playerGana = false;
    //Animaciones
    [Header("Player Animation Atributes")]
    [Tooltip("Aqui se coloca la aniamacion del Player de Morir")]
    [SerializeField] private AnimationClip playerDieAnim;
    private Animator animatorPlayer;
    //Bullet Variables
    [Header("Bullets Data")]
    [Tooltip("Aqui se coloca el ScriptableObject de -BulletData- con el nombre -Player Bullet-")]
    [SerializeField] BulletData bulletData;    
    [Tooltip("Aqui se colocaran los hijos del player con el nombre -BulletsSpawners-")]
    [SerializeField] private Transform[] bulletsSpawners;
    [Header("Bullets Atributes")]
    [Tooltip("Nivel de diapro, empieza en 1, si aumenta a 2 dispara dos veces al mimso timepo, si aumenta a 3 dispara 3 veces al mismo tiempo y asi sucesivamente")]
    [SerializeField] int shotLevel;
    public int ShotLevel => shotLevel;
    [Tooltip("Espacio que se sumara para ubicar la aparicion de la bala del Player")]
    [SerializeField] private float bulletOffset;
    [SerializeField] float bulletSpeed;
    //Animacion de Parpadeo
    [Header("Parpadeo Atributes")]
    public float tiempo_brillo;
    public SpriteRenderer[] spr;
    public bool cambio;
    public Color[] color_;
    public float speed_shine;
    public float cronometro;
    //Dash Player (Parte de lo de winona asi que lo puedes borrar luego)
    [Header("Dashing")]
    [SerializeField] private float activeMoveSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed;
    private float dashLength = 0.5f;
    private float dashCoolDown = 1f;
    private float dashCounter;
    private float dashCoolCounter;
    [SerializeField] private TrailRenderer trailRenderer;
    //Sonidos Player
    [Header("Sound Atributes")]
    [SerializeField] AudioMaster audioMaster;
    [SerializeField] GameObject reproductoSonidos;

    private void Start()
    {
        //Player RigidBody
        rbPlayer = GetComponent<Rigidbody2D>();
        //Player Collider
        colliderPlayer = GetComponent<PolygonCollider2D>();
        //Player Animator
        animatorPlayer = GetComponent<Animator>();
        //Player Reproductor de sonidos
        reproductoSonidos = GameObject.Find("SonidosPlayer");
        //Player Dash (Parte de lo de Winona)
        trailRenderer = GetComponent<TrailRenderer>();
        activeMoveSpeed = speedPlayer;

    }

    private void Update()
    {
        //Parpadeo al recibir daño
        Brillo();

        //Player Die
        PlayerDie();

        //Player Movimiento
        PlayerMovimiento();       

        //Player Disparo
        PlayerFire();
    }

    private void FixedUpdate()
    {
        //MovimientoFixed del Personaje
        PlayerMovimientoFixed();
    }

    //Funciones Player
    //Player recibe daño Funcion
    public void RecibirDanio(int danio)
    {
        lifePlayer = lifePlayer - danio;
        Recuperarse();
        cronometro = 1.5f;
    }

    //Player inmunidad despues de recibir daño Funcion
    public void Recuperarse()
    {
        colliderPlayer.enabled = false;
        StartCoroutine(ReactivarColliderPlayer());
    }

    //Player Gana Funcion
    public void Ganaste()
    {
        animatorPlayer.SetBool("Ganar", true);
        StartCoroutine(PlayerDesactivarCorrutina());
    }

    //Player Die Funcion
    private void PlayerDie()
    {
        //Player Die
        if (lifePlayer <= 0)
        {
            speedPlayer = 0;
            animatorPlayer.SetBool("Morir", true);
            colliderPlayer.enabled = false;
            playerMuere = true;
            StartCoroutine(PlayerDesactivarCorrutina());
        }
    }

    //Player Movimiento Funcion
    private void PlayerMovimiento()
    {
        //Movimiento del Personaje
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        animatorPlayer.SetFloat("MovX", moveX); //Animacion del Personaje
        animatorPlayer.SetFloat("MovY", moveY); //Animacion del Personaje
        moveInput = new Vector2(moveX, moveY).normalized;
        animatorPlayer.SetFloat("Speed", moveInput.sqrMagnitude);

        //Dash del Personaje (Winona)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                speedPlayer = dashSpeed;
                dashCounter = dashLength;
                trailRenderer.emitting = true;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                speedPlayer = activeMoveSpeed;
                dashCoolCounter = dashCoolDown;
                trailRenderer.emitting = false;
            }

        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    //Player MovimientoFixed Funcion
    private void PlayerMovimientoFixed()
    {
        //Movimiento del Personaje para el FixedUpdate
        rbPlayer.position = new Vector2(Mathf.Clamp(rbPlayer.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(rbPlayer.position.y, boundary.yMin, boundary.yMax));
        rbPlayer.MovePosition(rbPlayer.position + moveInput * speedPlayer * Time.fixedDeltaTime);
    }

    //Player Fire Funcion
    private void PlayerFire()
    {
        //Player Disparo
        if (Input.GetButtonDown("Fire1"))
        {
            
            reproductoSonidos.GetComponent<AudioSource>().PlayOneShot(audioMaster.playerAudios[0]);
            GameObject bullet;
            if (shotLevel == 1 || shotLevel == 3)
            {
                bullet = BulletsPool.Instance.RequestPlayerBullet(); //Aqui se llama para pedir la bala al pool
                bullet.GetComponent<Bullets>().SetPropsPlayer(new Vector2(bulletsSpawners[0].position.x, bulletsSpawners[0].position.y) + Vector2.up * bulletOffset, bulletData);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
            }
            if (shotLevel == 2 || shotLevel == 3)
            {
                bullet = BulletsPool.Instance.RequestPlayerBullet();//Aqui se llama para pedir la bala al pool
                bullet.GetComponent<Bullets>().SetPropsPlayer(new Vector2(bulletsSpawners[1].position.x, bulletsSpawners[1].position.y) + Vector2.up * bulletOffset, bulletData);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
                bullet = BulletsPool.Instance.RequestPlayerBullet();//Aqui se llama para pedir la bala al pool
                bullet.GetComponent<Bullets>().SetPropsPlayer(new Vector2(bulletsSpawners[2].position.x, bulletsSpawners[2].position.y) + Vector2.up * bulletOffset, bulletData);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
            }
        }
    }

    //Parpadeo del Player
    public void Brillo()
    {
        //Parpadeo del Player cuando recibe daño
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

    //Corrutinas Player
    //Corrutina para cuando el Player muere corra la animacion y se desactive o gane corra la animacion y se desactive
    IEnumerator PlayerDesactivarCorrutina()
    {
        //Para cuando el Player pierde
        if (playerMuere == true)
        {
            yield return new WaitForSeconds(playerDieAnim.length);
            gameObject.SetActive(false);
        }
        //Para cuando el Player gana
        if (playerGana == true)
        {
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }
        
    }

    //Corrutina para reactivar el Collider del Player
    IEnumerator ReactivarColliderPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        colliderPlayer.enabled = true;
    }
}
