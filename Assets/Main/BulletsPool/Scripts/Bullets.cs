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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemies") && transform.CompareTag("BulletPlayer"))
        {
            //other.GetComponent<ControladorDeEnemigos>().RecibirDanio(bulletData[0].BulletDamage);         
            ResetProps();
        }

        if (other.CompareTag("Player") && transform.CompareTag("EnemiesBullets") || other.CompareTag("Player") && transform.CompareTag("BossesBullets"))
        {
            other.GetComponent<Player>().RecibirDanio(bulletData[1].BulletDamagePlayer);
            ResetProps();
        }

        if (other.CompareTag("Limits"))
        {
            ResetProps();
        }
    }
    public void ResetProps()
    {
        gameObject.tag = "Untagged";
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
    public void SetProps(Vector2 _vel, Vector2 _pos, float _ang, BulletData _bulletData)
    {
        bulletData[1] = _bulletData;
        gameObject.GetComponent<SpriteRenderer>().sprite = bulletData[1].Sprite;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = bulletData[1].Animator;
        gameObject.tag = bulletData[1].TagName;
        transform.rotation = Quaternion.Euler(0, 0, _ang);
        transform.position = _pos;
        bulletRB.velocity = _vel;
    }
    public void SetPropsPlayer(Vector2 _pos, BulletData _bulletData)
    {
        bulletData[0] = _bulletData;
        gameObject.GetComponent<SpriteRenderer>().sprite = bulletData[0].Sprite;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = bulletData[0].Animator;
        gameObject.tag = bulletData[0].TagName;
        transform.position = _pos;
    }
}
