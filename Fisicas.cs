using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisicas : MonoBehaviour
{
    [SerializeField]
    private Vector3 acceleration;
    private Vector3 velocity;

    [SerializeField]  [Range(0,1)]
    private float dampFactor;

    [SerializeField]
    public float mass, gravity;

    [SerializeField] [Range(0, 1)]
    public float coeficientefric;

    [SerializeField]
    private float normalForce;

    [SerializeField]
    private Vector3 friction;

    private void Start()
    {
        normalForce = mass * gravity;
    }

    private void Update()
    {
        acceleration = Vector3.zero;
        ApplyForce(new Vector3(0, mass * gravity, 0));
        CalculateFriction();
        ApplyForce(friction);
        Move();
        CheckLimits();
    }

    public void Move()
    {
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

    }

    private void CheckLimits()
    {
        Vector3 position = transform.position;
        if ((position.x > 5 || position.x < -5))
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

    private void CalculateFriction()
    {
        friction = coeficientefric * normalForce * velocity.normalized;
    }
}
