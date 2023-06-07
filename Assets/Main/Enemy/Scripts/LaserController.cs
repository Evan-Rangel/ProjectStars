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
    [SerializeField] GameObject startParticles, endParticles;

    public float SetLaserCastDuration { set { laserCastDuration = value; } }
    public float SetMaxLaserPower { set { maxLaserPower= value; } }
    private void Start()
    {
        rend = GetComponent<Renderer>();
        //laserMat = GetComponent<Material>();
        //laserRenderer = GetComponent<LineRenderer>();
        rend.material.SetFloat("_LaserPower", currentLaserPower);
        maxLaserPower = 8;
    }

    public void CastLaserFunc( float _laserCastDuration)
    {
        laserCastDuration = _laserCastDuration;
        StartCoroutine(CastLaser());
    }
    public void CastLaserFunc()
    {
        StartCoroutine(CastLaser());
    }
    public void LaserRaycast(Vector2 _targetPosition, Vector2 _currentPosition, bool _isHit)
    {
        laserRenderer.SetPosition(0, Vector2.zero);
        //startParticles.transform.position = _currentPosition;
        //startParticles.transform.rotation = transform.rotation * Quaternion.AngleAxis(90,Vector2.right);
        //Debug.Log(_targetPosition);
        laserRenderer.SetPosition(1, _targetPosition);
        if (_isHit)
        {
            endParticles.SetActive(true);
            endParticles.transform.localPosition = _targetPosition - (_targetPosition.normalized/4);
            endParticles.transform.rotation = transform.rotation * Quaternion.AngleAxis(-90, Vector2.right);

        }
        else
        {
            endParticles.SetActive(false);
        }
    }
  
    IEnumerator CastLaser()
    {
        if (currentLaserPower<= 20)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            currentLaserPower += Time.deltaTime*laserCastDuration;
            rend.material.SetFloat("_LaserPower", currentLaserPower);
            CastLaserFunc();
        }
        else
        {
            StopCoroutine(CastLaser());
        }
    }

    public void SwtichLaserFunc(float _offDuration, float _onDuration, float _castDuration)
    {
        StartCoroutine(LaserOffTime(_offDuration, _onDuration, _castDuration));
    }



    IEnumerator LaserOffTime(float _duration, float _onDuration, float _castDuration)
    {
        currentLaserPower = 1.1f;
        rend.material.SetFloat("_LaserPower", currentLaserPower);
        yield return new WaitForSeconds(_duration);

        //StartCoroutine(DecastLaser(_offDuration));

    }
    /*
    IEnumerator LaserOnTime(float _duration)
    {
        StartCoroutine(LaserOffTime(_offDuration));

    }
    IEnumerator DecastLaser(float _duration)
    {

    }
    */

}
