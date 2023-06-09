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
    public void LaserRaycast(Vector2 _targetPosition, Vector2 _currentPosition, bool _isHit)
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
  
    IEnumerator CastLaser()
    {
        yield return new WaitForEndOfFrame();
        laserCastValue += Time.deltaTime*laserCastSpeed;
        rend.material.SetFloat("_LaserCastValue", laserCastValue);
        CastLaserFunc();

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
