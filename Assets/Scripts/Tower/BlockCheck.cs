using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck : MonoBehaviour
{
    [SerializeField, Range(0, 3)]
    float velDelta;
    Vector3 startPos;
    bool grade;
    bool push;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        grade = false;
        push = false;
        GameManager.Gm.AddBlock();
    }

    void Update()
    {
        if (!grade)
            AddScore();
        if (!push)
            GravityOn();
    }
    
    void AddScore()
    {
        if (Mathf.Abs(startPos.y - transform.position.y) >= 1)
        {
            grade = true;
            GameManager.Gm.AddScore();
            rb.useGravity = true;

        }
    }
    
    void GravityOn()
    {
        if (rb.velocity.x > velDelta || rb.velocity.z > velDelta || rb.velocity.z > velDelta)
        {
            rb.useGravity = true;
            GetComponent<BoxCollider>().enabled = false;
            push = true;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        rb.useGravity = true;
    }
    
    public void Impulse(float speed)
    {
        Vector3 dir = rb.velocity.normalized;
        rb.AddForce(dir * speed, ForceMode.Impulse);
    }
}
