using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Vector3 _forceHit;
    [SerializeField] private Vector3 _direction;
    
    private Rigidbody _rb;
    private bool _isHit;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void addImpulse(Vector3 dir, float speed)
    {
        _rb.velocity = Vector3.zero;
        _rb.AddForce(
            _direction.x * (_forceHit.x + speed),
            _direction.y * (_forceHit.y + speed),
            _direction.z * (_forceHit.z + speed),
            ForceMode.Impulse
            );
        _isHit = true;
    }
}
