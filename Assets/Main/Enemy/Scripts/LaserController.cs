using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] Material laserMat;
    [SerializeField] LineRenderer laserRenderer;
    [SerializeField] float maxLaserPower;
    float currentLaserPower = 0;
    public float SetMaxLaserPower { set { maxLaserPower= value; } }
    public Material SetLaserMaterial {set { laserMat = value; } }
    private void Start()
    {
        //laserMat = GetComponent<Material>();
        laserRenderer = GetComponent<LineRenderer>();
        laserMat.SetFloat("LaserPower", currentLaserPower);
        laserRenderer.materials[0] = laserMat;
    }

    public void CastLaserFunc( float _maxLaserPower)
    {
        maxLaserPower = _maxLaserPower;
        StartCoroutine(CastLaser());
    }

    IEnumerator CastLaser()
    {
        if (currentLaserPower<= maxLaserPower)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            laserRenderer.materials[0] = laserMat;
            currentLaserPower += Time.deltaTime;
            laserMat.SetFloat("LaserPower", currentLaserPower);
            Debug.Log( laserMat.GetFloat("LaserPower"));
           // Debug.Log(currentLaserPower);
            CastLaserFunc(maxLaserPower);
        }
        else
        {
            //Debug.Log(currentLaserPower);
            StopCoroutine(CastLaser());
        }
    }
}
