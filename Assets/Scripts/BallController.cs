using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Vector3 _forceHit;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _maxVelocityY;
    
    private Rigidbody _rb;
    private bool _isHit;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
//        if (_isHit)
//        {
//            _rb.AddForce(_direction * _forceHit, ForceMode.Force);
//        }
    }

    private void Update()
    {
        Debug.Log(_rb.velocity);
    }

    public void addImpulse()
    {
        _rb.velocity = Vector3.zero;
        Debug.Log(_rb.velocity);
        _rb.AddForce(_direction.x * _forceHit.x, _direction.y * _forceHit.y, _direction.z * _forceHit.z, ForceMode.Impulse);
        _isHit = true;
        Debug.Log(_rb.velocity);
    }
}
