using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseObject : MonoBehaviour
{
    private Rigidbody _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Impulse(float speed)
    {
        Vector3 dir = _rb.velocity.normalized;
        _rb.AddForce(dir * speed, ForceMode.Impulse);
    }
}
