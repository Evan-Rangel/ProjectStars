using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] LineRenderer laserRenderer;
    float maxLaserPower;
    [SerializeField]float currentLaserPower = 1.1f;
    [SerializeField] Renderer rend;
    [SerializeField] float laserCastDuration;
    public float SetLaserCastDuration { set { laserCastDuration = value; } }
    public float SetMaxLaserPower { set { maxLaserPower= value; } }
    private void Start()
    {
        rend = GetComponent<Renderer>();
        //laserMat = GetComponent<Material>();
        laserRenderer = GetComponent<LineRenderer>();
        rend.material.SetFloat("_LaserPower", currentLaserPower);
        maxLaserPower = 8;
    }

    public void CastLaserFunc( float _laserCastDuration)
    {
        laserCastDuration = _laserCastDuration;
        //Debug.Log(laserCastDuration);
        StartCoroutine(CastLaser());
    }
    public void CastLaserFunc()
    {
        StartCoroutine(CastLaser());
    }

    IEnumerator CastLaser()
    {
        if (currentLaserPower<= 8)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            currentLaserPower += Time.deltaTime*laserCastDuration;
            Debug.Log(currentLaserPower);
            rend.material.SetFloat("_LaserPower", currentLaserPower);
            CastLaserFunc();
        }
        else
        {
            //Debug.Log(currentLaserPower);
            StopCoroutine(CastLaser());
        }
    }
}
