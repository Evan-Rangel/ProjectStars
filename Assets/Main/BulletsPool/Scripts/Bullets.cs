using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [Header("Bullets Prefaps")]
    [Space]
    [Header("Bullets Data")]
    [Tooltip("Aqui se coloca todos los ScriptableObjects de los -BulletData-")]
    public BulletData[] bulletData;
    [Header("Bullet RB")]
    [Tooltip("Aqui arrastra el RigidBody del prefap")]
    [SerializeField] private Rigidbody2D bulletRB;
    [Header("Bullet Atributes")]
    [SerializeField] private int bulletDamagePlayer;
    [Header("Sound Atributes")]
    [SerializeField] AudioMaster audioMaster;
    [SerializeField] GameObject reproductoSonidos;

    float rotSum = 0;
    float rot;
    float timeRot;
    float vel;
    private void Start()
    {
        audioMaster = GameObject.FindGameObjectWithTag("AudioMaster").GetComponent<AudioMaster>();
        reproductoSonidos = GameObject.Find("SonidosBalas");
    }

    public void GenerateRotation(float _rot, float _time, float _vel, float _rotsum)
    {
        rotSum = _rotsum;
        timeRot = _time;
        rot = _rot;
        vel = _vel;
        /*float DirXPosition = transform.position.x + Mathf.Sin((rot * Mathf.PI) / 180);
        float DirYPosition = transform.position.y + Mathf.Cos((rot * Mathf.PI) / 180);
        Vector2 Vector = new Vector2(DirXPosition, DirYPosition);
        Vector2 MoveDirection = (Vector - (Vector2)transform.position).normalized * bulletRB.velocity;
        bulletRB.velocity = MoveDirection;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        */
        StartCoroutine(TimeRotation());

    }
    public void GenerateRotation()
    {
        rot += rotSum*Time.deltaTime;//* Time.deltaTime ;
        
        Debug.Log(rot);
        float DirXPosition = transform.position.x + Mathf.Sin((rot * Mathf.PI) / 180);
        float DirYPosition = transform.position.y + Mathf.Cos((rot * Mathf.PI) / 180);
        Vector2 Vector = new Vector2(DirXPosition, DirYPosition);
        Vector2 MoveDirection = (Vector - (Vector2)transform.position).normalized * vel;
        transform.rotation = Quaternion.Euler(0, 0, -rot);
        bulletRB.velocity = MoveDirection;
        //Debug.Log(bulletRB.velocity);

        StartCoroutine(TimeRotation());

    }
    IEnumerator TimeRotation()
    {
        yield return new  WaitForSeconds(timeRot*Time.deltaTime);
        //yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(Time.deltaTime);

        GenerateRotation();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemies") && transform.CompareTag("PlayerBullets"))
        {

            other.GetComponent<EnemyController>().DealDamage(bulletDamagePlayer);         
            ResetProps();
        }

        if (other.CompareTag("Player") && transform.CompareTag("EnemiesBullets") || other.CompareTag("Player") && transform.CompareTag("BossesBullets"))
        {
            other.GetComponent<Player>().RecibirDanio(bulletData[1].BulletDamagePlayer);
            ResetProps();
        }

        if (other.CompareTag("Limits"))
        {
            reproductoSonidos.GetComponent<AudioSource>().PlayOneShot(audioMaster.playerAudios[1]);
            ResetProps();
        }
    }
    public void ResetProps()
    {
        gameObject.tag = "Untagged";
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
    public void SetProps(Vector2 _vel, Vector2 _pos, float _ang)
    {
       // bulletData[1] = _bulletData;
        gameObject.GetComponent<SpriteRenderer>().sprite = bulletData[1].Sprite;
        //gameObject.GetComponent<Animator>().runtimeAnimatorController = bulletData[1].Animator;
        gameObject.tag = bulletData[1].TagName;
        transform.rotation = Quaternion.Euler(0, 0, _ang);
        transform.position = _pos;
        bulletRB.velocity = _vel;
    }
    public void SetPropsPlayer(Vector2 _pos, BulletData _bulletData, int _damage)
    {
        bulletData[0] = _bulletData;
        gameObject.GetComponent<SpriteRenderer>().sprite = bulletData[0].Sprite;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = bulletData[0].Animator;
        gameObject.tag = bulletData[0].TagName;
        transform.position = _pos;
        bulletDamagePlayer = _damage;
    }
}
