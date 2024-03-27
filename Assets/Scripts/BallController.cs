using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float initialSpeed = 10f;
    private bool launched = false;
    private Transform paddle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        paddle = GameObject.Find("Paddle").transform;
        transform.SetParent(paddle);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = rb.velocity.normalized * initialSpeed;
        LaunchBall();
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

        else if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void LaunchBall()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !launched)
        {
            rb.velocity = Vector3.up * initialSpeed;
            transform.SetParent(null);
            launched = true;
        }

    }
}
