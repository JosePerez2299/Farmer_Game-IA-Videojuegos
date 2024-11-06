using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : DynamicFlee
{
    public Rigidbody2D rbTarget;
    public float maxPredition = 50;
    // Start is called before the first frame update
    void Start()
    {
        character = transform;
        rbTarget = target.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        result = gameObject.AddComponent<SteeringOutput>();
        kinematic = gameObject.AddComponent<Kinematic>();
    }

    // Update is called once per frame
    void Update()
    {
      SteeringOutput steering = getSteering();
         Debug.Log(steering.linear);
 
        kinematic.UpdateKinematic(steering, maxSpeed);

        if (steering.linear == Vector2.zero) kinematic.velocity = Vector2.zero;
        rb.linearVelocity = kinematic.velocity;


    }

    public new SteeringOutput getSteering()
    {

        Vector2 direction = transform.position - target.position ;
        float distance = direction.magnitude;
        float speed = rb.linearVelocity.magnitude;

        float prediction;

        if (speed <= distance / maxPredition)
        {
            prediction = maxPredition;
        }
        else
        {
            prediction = distance / speed;
        }

        newTargetPrediction = rbTarget.linearVelocity * prediction;


        return base.getSteering();
    }
}