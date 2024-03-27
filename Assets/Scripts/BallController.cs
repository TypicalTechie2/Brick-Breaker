using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float initialSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LaunchBall();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * initialSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            float hitPoint = collision.contacts[0].point.x - collision.transform.position.x;
            Vector3 direction = new Vector3(hitPoint, 1f, 0f).normalized;
            rb.velocity = direction * initialSpeed;
        }

        else if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 reflection = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
            rb.velocity = reflection;
        }
    }

    private void LaunchBall()
    {
        rb.velocity = Vector3.up * initialSpeed;
    }
}
