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
    [SerializeField] private Trough _trough;
    
    private Rigidbody _rb;
    private float _axis;
    private float _oldTime;
    [SerializeField]private bool _isBall;
    
    public static RotateController Rt;
    
    void Awake()
    {
        if (Rt == null)
        {
            Rt = this;
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
        if (_isBall && Input.GetMouseButtonDown(0) && _currentAmountRotate == 36)
        {
            _axis = Input.GetAxis("Mouse Y");
            Vector3 tmp = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            _axis = tmp.x;
            if (_axis <= 0.3)
            {
                _axis = -0.2f;
            }
            else if (_axis >= 0.7)
            {
                _axis = 0.2f;
            }
            else
            {
                _axis = 0;
            }
            _currentAmountRotate = 0;
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9 && other.gameObject.GetComponent<BallController>() != null)
        {
            if (IsRotate())
            {
                other.gameObject.GetComponent<BallController>()
                    .addImpulse(new Vector3(_axis, 0, 0), 0.1f * _currentAmountRotate);
                if (_isBall)
                {
                    _isBall = false;
                }
                if (_trough.AmountBall() == 0)
                {
                    GameManager.Gm._isDoneBall = true;
                }
            }
            else
            {
                other.gameObject.GetComponent<BallController>().StopingFly();
            }
        }
    }

    public bool IsRotate()
    {
        return _currentAmountRotate != 36;
    }

    public bool IsBall
    {
        get => _isBall;
        set => _isBall = value;
    }
}