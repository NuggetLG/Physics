using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluido : MonoBehaviour
{
    [SerializeField]
    private Vector3 acceleration;
    private Vector3 velocity;
    [SerializeField]
    public float mass, gravity;
    [SerializeField]
    [Range(0, 1)]
    private float frictionC;
    private float frontalArea;
    [SerializeField]
    [Range(0, 1)]
    private float dampFactor;


    private void Start()
    {
        frontalArea = transform.localScale.x;
    }

    private void Update()
    {

        acceleration = Vector3.zero;
        ApplyForce(new Vector3(0, mass * gravity, 0));

        if (transform.position.y < 0)
        {
            Vector3 fluidFriction = ((velocity.magnitude * velocity.magnitude) * frontalArea * -frictionC * velocity.normalized) / 2;
            ApplyForce(fluidFriction);
        }

        Move();
        CheckLimits();
    }

    private void Move()
    {
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    private void CheckLimits()
    {
        Vector3 position = transform.position;
        if (position.x > 5 || position.x < -5)
        {
            if (position.x > 5) transform.position = new Vector3(5, transform.position.y);
            if (position.x < -5) transform.position = new Vector3(-5, transform.position.y);

            velocity.x = velocity.x * -1;
            velocity *= dampFactor;
        }
        else if (position.y > 5 || position.y < -5)
        {
            if (position.y > 5) transform.position = new Vector3(transform.position.x, 5);
            if (position.y < -5) transform.position = new Vector3(transform.position.x, -5);

            velocity.y = velocity.y * -1;
            velocity *= dampFactor;
        }
    }

    private void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }
}
