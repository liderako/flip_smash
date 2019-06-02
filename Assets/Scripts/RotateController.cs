using System;
using System.Collections;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    [SerializeField] private float _step;
    [SerializeField] private float _waitCoroutine;
    [SerializeField] private int _maxAmountRotate;
    [SerializeField] private int _currentAmountRotate;
    
    private Rigidbody _rb;
    private float oldTime;
    
    public static RotateController rt;
    
    void Awake()
    {
        if (rt == null)
        {
            rt = new RotateController();
        }
    }

    void Start()
    {;
        _rb = GetComponent<Rigidbody>();
        oldTime = Time.time;
    }

    void Update()
    {
        Touch();
    }

    void FixedUpdate()
    {

        if (_currentAmountRotate != _maxAmountRotate && Time.time - oldTime > _waitCoroutine)
        {
            _rb.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z + _step);
            oldTime = Time.time;
            _currentAmountRotate += 1;
        }
    }

    private void Touch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentAmountRotate = 0;
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9 && IsRotate())
        {
            other.gameObject.GetComponent<BallController>().addImpulse(Vector3.zero, 0.1f * _currentAmountRotate);
        }
    }

    public bool IsRotate()
    {
        return _currentAmountRotate != 36;
    }
}