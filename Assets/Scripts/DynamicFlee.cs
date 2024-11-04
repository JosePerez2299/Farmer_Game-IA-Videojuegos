using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFlee : MonoBehaviour
{
    protected Transform character;
    public Transform target;
    protected Rigidbody2D rb;
    public float maxSpeed = 15;
    public float maxDistance = 20;
    protected SteeringOutput result;

    protected Vector3 newTargetPrediction = Vector3.zero;
    protected Kinematic kinematic;

    // Start is called before the first frame update
    void Start()
    {
        character = transform;
        result = gameObject.AddComponent<SteeringOutput>();
        kinematic = gameObject.AddComponent<Kinematic>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SteeringOutput steering = getSteering();
         Debug.Log(steering.linear);
 
        kinematic.UpdateKinematic(steering, maxSpeed);

        if (steering.linear == Vector2.zero) kinematic.velocity = Vector2.zero;
        rb.velocity = kinematic.velocity;

    }


    public SteeringOutput getSteering()
    {
        result.linear = character.position - target.position;
        float compareDistance = result.linear.magnitude;
        result.linear += (Vector2) newTargetPrediction;
        if (compareDistance > maxDistance )
        {   
            result.angular = 0;
            result.linear = Vector2.zero;
            return result;
        }

        result.linear.Normalize();
        result.linear *= maxSpeed;
        result.angular = 0;

        return result;
    }
}

