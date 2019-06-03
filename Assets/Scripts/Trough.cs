using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Trough : MonoBehaviour
{
    [SerializeField] private List<GameObject> _balls;
    [SerializeField] private Vector3 _upPosition;
    [SerializeField] private Vector3 _downPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _waitMoveDown;
    [SerializeField] private float _waitCloseFlip;
    
    [SerializeField] private bool _isDown;
    [SerializeField] private bool _isUp;
    [SerializeField] private bool _isMoveUp;
    [SerializeField] private bool _isMoveDown;
    
    
    void Start()
    {
        _downPosition = transform.localPosition;
        _isDown = true;
    }

    void Update()
    {
        if (_balls.Count != 0)
        {
            MoveUp();
        }
        MoveDown();
    }

    private void MoveUp()
    {
        if (Input.GetMouseButtonDown(0) && _isDown)
        {
            _isMoveUp = true;
            _isDown = false;
        }
        if (_isMoveUp)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _upPosition, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, _upPosition) < 0.001f)
            {
                _isMoveUp = false;
                _isUp = true;
                _balls.First().GetComponent<SphereCollider>().enabled = true;
                _balls.First().GetComponent<Rigidbody>().useGravity = true;
                _balls.Remove(_balls.First());
                GameManager.Gm.IsReadyFlip = true;
                StartCoroutine(StartMoveDown());
            }
        }
    }

    private void MoveDown()
    {
        if (_isMoveDown)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _downPosition, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, _downPosition) < 0.001f)
            {
                StartCoroutine(CloseFlip());
            }
        }
    }

    IEnumerator StartMoveDown()
    {
        yield return new WaitForSeconds(_waitMoveDown);
        _isMoveDown = true;
    }

    IEnumerator CloseFlip()
    {
        yield return new WaitForSeconds(_waitCloseFlip);
        GameManager.Gm.IsReadyFlip = false;
        _isMoveDown = false;
        _isDown = true;
    }
}
