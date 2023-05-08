using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_PlayerDash : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Player _player;
    //private float _baseGravity;

    [Header("Dash")]
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashForce = 20f;
    [SerializeField] private float _timeCanDash = 1f;
    private bool _isDashing;
    public bool IsDashing => _isDashing;
    private bool _canDash = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        //_baseGravity = _rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(Dash());
        }       
    }

    private IEnumerator Dash()
    {
        _isDashing = true;
        _canDash = false;
        _rb.gravityScale = 0f;
        //_rb.velocity = new Vector2(_player.DirectionX * _dashForce, _player.DirectionY * _dashForce);
        yield return new WaitForSeconds(_dashingTime);
        _isDashing = false;
        //_rb.gravityScale = _baseGravity;
        yield return new WaitForSeconds(_timeCanDash);
        _canDash = true;
    }
}
