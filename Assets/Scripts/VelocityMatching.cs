using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatching : MonoBehaviour
{
    private Transform character;
    public Transform target;
    private Kinematic kinematic;
    public Rigidbody2D rbCharacter, rbTarget;
    private SteeringOutput result;
    public float maxAcceleration = 50 ,maxSpeed = 20;
    public float timeToTarget = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        character = transform;
        rbCharacter = GetComponent<Rigidbody2D>();
        rbTarget = target.GetComponent<Rigidbody2D>();
        result = gameObject.AddComponent<SteeringOutput>();
        kinematic = gameObject.AddComponent<Kinematic>();
    }

    // Update is called once per frame
    void Update()
    {
        SteeringOutput steering = getSteering();
        kinematic.UpdateKinematic(steering, 40);
        rbCharacter.velocity = kinematic.velocity;

    }

    SteeringOutput getSteering()
    {
        result.linear = rbTarget.velocity - rbCharacter.velocity;
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
