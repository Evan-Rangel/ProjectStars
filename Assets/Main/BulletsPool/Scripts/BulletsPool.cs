using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    [Header("BulletsPool")]
    [Space]
    [Header("Bullets Data")]
    [Tooltip("Aqui se coloca todos los ScriptableObjects de los -BulletData-")]
    [SerializeField] BulletData[] bulletData;
    [Header("Bullet Prefaps")]
    [Tooltip("Aqui se coloca el Prefap de Bullet el cual contiene el script de -Bullets-")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<GameObject> bulletsPlayerList;
    [SerializeField] private List<GameObject> bulletsEnemiesList;

    private static BulletsPool instance;
    public static BulletsPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //AddLaserToPool(poolSize);
    }

    private void AddPlayerBulletToPool(int amount)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bulletsPlayerList.Add(bullet);
        bullet.transform.parent = transform;
        bullet.SetActive(false);
    }

    public GameObject RequestPlayerBullet()
    {
        for (int i = 0; i < bulletsPlayerList.Count; i++)
        {
            if (!bulletsPlayerList[i].activeSelf)
            {
                bulletsPlayerList[i].SetActive(true);
                return bulletsPlayerList[i];
            }
        }
        AddPlayerBulletToPool(1);
        bulletsPlayerList[bulletsPlayerList.Count - 1].SetActive(true);
        return bulletsPlayerList[bulletsPlayerList.Count - 1];
    }

    private void AddEnemyBulleToPool(int amount)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bulletsEnemiesList.Add(bullet);
        bullet.transform.parent = transform;
        bullet.SetActive(false);
    }



    public GameObject RequestEnemyBullet()
    {
        for (int i = 0; i < bulletsEnemiesList.Count; i++)
        {
            if (!bulletsEnemiesList[i].activeSelf)
            {
                bulletsEnemiesList[i].SetActive(true);
                return bulletsEnemiesList[i];
            }
        }
        AddEnemyBulleToPool(1);
        bulletsEnemiesList[bulletsEnemiesList.Count - 1].SetActive(true);
        return bulletsEnemiesList[bulletsEnemiesList.Count - 1];
    }
}
