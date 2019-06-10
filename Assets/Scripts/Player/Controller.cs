using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Player
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private float _step;
        [SerializeField] private float _waitCoroutine;
        [SerializeField] private int _maxAmountRotate;
        [SerializeField] private int _currentAmountRotate;
        [SerializeField] private Trough _trough;
        

        private Rigidbody _rb;
        private Camera _main;
        private float _axis;
        private float _oldTime;
        [SerializeField] private bool _isBall;

        public static Controller playerController;

        void Awake()
        {
            if (playerController == null)
            {
                playerController = this;
            }
        }

        void Start()
        {
            _main = Camera.main;
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
                _rb.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                    transform.rotation.eulerAngles.y,
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
                Vector3 tmp = _main.ScreenToViewportPoint(Input.mousePosition);
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
                        StartCoroutine(GameManager.Gm.BallDone());
                    }
                }
                else
                {
                    other.gameObject.GetComponent<BallController>().StopingFly();
                }
            }
        }

        private bool IsRotate()
        {
            return _currentAmountRotate != 36;
        }

        public bool IsBall
        {
            get => _isBall;
            set => _isBall = value;
        }
    }
}