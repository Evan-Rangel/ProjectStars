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
    [SerializeField] private GameObject laserPrefab;
    //[SerializeField, Range(0, 50)] private int poolSize;
    [SerializeField] private List<GameObject> laserList;
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

    private void AddLaserToPool(int amount)
    {
        GameObject laser = Instantiate(laserPrefab);
        laserList.Add(laser);
        laser.transform.parent = transform;
        laser.SetActive(false);
    }

    public GameObject RequestLaser()
    {
        for (int i = 0; i < laserList.Count; i++)
        {
            if (!laserList[i].activeSelf)
            {
                laserList[i].SetActive(true);
                return laserList[i];
            }
        }
        AddLaserToPool(1);
        laserList[laserList.Count - 1].SetActive(true);
        return laserList[laserList.Count - 1];
    }
}
