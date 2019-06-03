using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Vector3 _forceHit;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _impulseHit;
    
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
            (_direction.x + dir.x) *(_forceHit.x + speed),
            (_direction.y + dir.y) *(_forceHit.y + speed),
            (_direction.z + dir.z) *(_forceHit.z + speed),
            ForceMode.Impulse
        );
        _isHit = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<BlockCheck>().Impulse(_impulseHit);
        }
    }
}
