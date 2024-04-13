using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public GameObject spark;
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        UpdateVector(new Vector3(GetRandom(), 0f, GetRandom()).normalized);
    }

    private float GetRandom()
    {
        return Random.Range(.3f, .6f) * (Random.Range(0, 10) > 4 ? 1f : -1f);
    }

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude < speed)
            UpdateVector(rb.velocity.normalized);
    }

    private void UpdateVector(Vector3 dir)
    {
        rb.velocity = dir * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed *= 1.02f;
        Instantiate(spark, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal, Vector3.up));
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
