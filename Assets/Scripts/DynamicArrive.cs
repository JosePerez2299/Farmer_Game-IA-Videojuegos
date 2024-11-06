using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicArrive : MonoBehaviour
{
    private Transform character;
    public Transform target;
    private Kinematic kinematic;
    public Rigidbody2D rb;
    private SteeringOutput result;
    public float maxAcceleration = 20, maxSpeed = 20;

    public float targetRadius = 3, slowRadius = 10, timeToTarget = 1;

    private NewOrientation orientation;

    // Start is called before the first frame update
    void Start()
    {
        character = transform;
        rb = GetComponent<Rigidbody2D>();
        result = gameObject.AddComponent<SteeringOutput>();
        kinematic = gameObject.AddComponent<Kinematic>();
        gameObject.AddComponent<NewOrientation>();
        orientation = GetComponent<NewOrientation>();
    }

    // Update is called once per frame
    void Update()
    {
        SteeringOutput steering = getSteering();

        if (steering != null)
        {
            kinematic.UpdateKinematic(steering, maxSpeed);
            character.position = kinematic.position;
            rb.rotation = kinematic.rotation;
            rb.angularVelocity = kinematic.orientation;
            rb.linearVelocity = kinematic.velocity;
            character.eulerAngles = new Vector3(0, 0, orientation.Calculate(character.eulerAngles.z, rb.linearVelocity));
        }
        else rb.linearVelocity = Vector2.zero;



    }

    SteeringOutput getSteering()
    {
        Vector2 direction = target.position - character.position;
        Vector2 targetVelocity;

        float distance = direction.magnitude;
        float targetSpeed;

        if (distance < targetRadius)
        {

            return null;
        }

        if (distance > slowRadius)
        {
            targetSpeed = maxSpeed;
        }
        else
        {
            targetSpeed = maxSpeed * distance / slowRadius;
        }

        targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        result.linear = targetVelocity - rb.linearVelocity;
        result.linear /= timeToTarget;

        if (result.linear.magnitude > maxAcceleration)
        {
            result.linear.Normalize();
            result.linear *= maxAcceleration;
        }

        result.angular = 0;
        return result;
    }

}
