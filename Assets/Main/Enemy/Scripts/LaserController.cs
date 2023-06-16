using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] LineRenderer laserRenderer;
    float maxLaserPower;
    [SerializeField] float laserCastValue = 0;
    [SerializeField] Renderer rend;
    [SerializeField] float laserCastSpeed;
    [SerializeField] GameObject startParticles, endParticles;
    bool canDamage = false;
    bool canFollowPlayer;
    Vector2 targetPos;
    public bool GetCanFollowPlayer { get { return canFollowPlayer; } }
    public float SetLaserCastDuration { set { laserCastSpeed = value; } }
    public float SetMaxLaserPower { set { maxLaserPower = value; } }
    public bool GetCanDamage { get { return canDamage; } }
    private void Start()
    {
        rend = GetComponent<Renderer>();
        laserCastValue = 0;
        rend.material.SetFloat("_LaserCastValue", laserCastValue);
        maxLaserPower = 8;
    }

    public void CastLaserFunc( float _laserCastSpeed)
    {
        laserCastSpeed = _laserCastSpeed;
        StartCoroutine(CastLaser());
    }
    public void CastLaserFunc()
    {
        if (rend.material.GetFloat("_LaserCastValue")<1)
        {
            StartCoroutine(CastLaser());
        }
        else
        {
            rend.material.SetFloat("_LaserCastValue", 1);
            canDamage = true;
        }
    }
    public void LaserRaycast(Vector2 _targetPosition, bool _isHit)
    {
        laserRenderer.SetPosition(0, Vector2.zero);
        //startParticles.transform.position = _currentPosition;
        //startParticles.transform.rotation = transform.rotation * Quaternion.AngleAxis(90,Vector2.right);


        laserRenderer.SetPosition(1, _targetPosition);
        if (_isHit && canDamage)
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
    //Follow Player functions
    public Vector2 FPRaycastDirection(Vector2 _playerPos)
    {
        
        if (canFollowPlayer)
        {
            targetPos = _playerPos;
        }
        //Debug.Log(canFollowPlayer);
        return targetPos;
    }
    public void StartFPCorr(float _OffTime, float _onTime, float _castSpeed)
    {
        StartCoroutine(LaserRandomCorr(_onTime));
        StartCoroutine(FPCorr(_OffTime));
        StartCoroutine(LaserRandomTimeOff(_OffTime, _castSpeed));


    }
    IEnumerator FPCorr(float _OffTime)
    {

        canFollowPlayer = true;
        yield return new WaitForSeconds(_OffTime-0.4f);
        canFollowPlayer = false;
    }
    
    IEnumerator CastLaser()
    {
        yield return new WaitForEndOfFrame();
        laserCastValue += Time.deltaTime*laserCastSpeed;
        rend.material.SetFloat("_LaserCastValue", laserCastValue);
        CastLaserFunc();

    }
    public void LaserRandomFunc(float _timeOn, float _timeOff, float _laserCastSpeed)
    {
        StartCoroutine(LaserRandomCorr(_timeOn));
        StartCoroutine(LaserRandomTimeOff(_timeOff, _laserCastSpeed));
    }
    IEnumerator LaserRandomCorr(float _timeOn)
    {
        yield return new WaitForSeconds(_timeOn);
        gameObject.SetActive(false);
    }
    IEnumerator LaserRandomTimeOff(float _timeOff, float _laserCastSpeed)
    {
        yield return new WaitForSeconds(_timeOff);
        CastLaserFunc(_laserCastSpeed);
    }

    public void SwtichLaserFunc(float _offDuration, float _onDuration, float _castSpeed)
    {
        StartCoroutine(SwitchLaserCorr(_offDuration, _onDuration, _castSpeed));
    }


    IEnumerator SwitchLaserCorr(float _offduration, float _onDuration, float _castSpeed)
    {
        laserCastValue = 0f;
        rend.material.SetFloat("_LaserCastValue", laserCastValue);
        canDamage = false;
        yield return new WaitForSeconds(_offduration);
        
        
        while (rend.material.GetFloat("_LaserCastValue")<1)
        {
            laserCastValue += Time.deltaTime * _castSpeed;
            rend.material.SetFloat("_LaserCastValue", laserCastValue);
            yield return new WaitForEndOfFrame();
        }


        laserCastValue = 1f;
        rend.material.SetFloat("_LaserCastValue", laserCastValue);
        canDamage = true;

        yield return new WaitForSeconds(_onDuration);

        while (rend.material.GetFloat("_LaserCastValue") > 0)
        {
            laserCastValue -= Time.deltaTime * _castSpeed;
            rend.material.SetFloat("_LaserCastValue", laserCastValue);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(SwitchLaserCorr(_offduration, _onDuration,_castSpeed));

    }





}
