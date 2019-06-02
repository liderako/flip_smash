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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        grade = false;
        push = false;
    }

    // Update is called once per frame
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
            GameManager.score += 1;
            rb.useGravity = true;

        }
    }
    void GravityOn()
    {
        if (rb.velocity.x > velDelta || rb.velocity.z > velDelta || rb.velocity.z > velDelta)
        {
            rb.useGravity = true;
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
