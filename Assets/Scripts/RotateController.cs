using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotateController : MonoBehaviour
{
    [SerializeField] private float _step;
    [SerializeField] private float _waitCoroutine;
    [SerializeField] private int _maxAmountRotate;
    [SerializeField] private int _currentAmountRotate;
    
    private Rigidbody _rb;
    private float _axis;
    private float _oldTime;
    
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
        _oldTime = Time.time;
    }

    void Update()
    {
        Touch();
    }

    void FixedUpdate()
    {

        if (_currentAmountRotate != _maxAmountRotate && Time.time - _oldTime > _waitCoroutine)
        {
            _rb.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z + _step);
            _oldTime = Time.time;
            _currentAmountRotate += 1;
        }
    }

    private void Touch()
    {
        if (GameManager.Gm.IsReadyFlip && Input.GetMouseButtonDown(0) && _currentAmountRotate == 36)
        {
            _axis = Input.GetAxis("Mouse Y");
            Vector3 tmp = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            _axis = tmp.x;
            if (_axis < 0.5)
            {
                _axis += 1f;
            }
            else
            {
                _axis *= -1;
            }
            _currentAmountRotate = 0;
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9 && IsRotate())
        {
            other.gameObject.GetComponent<BallController>().addImpulse(new Vector3(0, 0, _axis * 0.5f), 0.1f * _currentAmountRotate);
        }
    }

    public bool IsRotate()
    {
        return _currentAmountRotate != 36;
    }
}