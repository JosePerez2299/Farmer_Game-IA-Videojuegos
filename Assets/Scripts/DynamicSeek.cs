using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DynamicSeek : MonoBehaviour
{

    public Transform character;
    public Transform target;
    protected Kinematic kinematic;
    public Rigidbody2D rb;
    protected SteeringOutput result;
    protected Vector3 newTargetPrediction = Vector3.zero;
    public float maxAcceleration = 10; 
    public float maxSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        character = transform;
        rb = GetComponent<Rigidbody2D>();
        result = gameObject.AddComponent<SteeringOutput>();
        kinematic = gameObject.AddComponent<Kinematic>();
        gameObject.AddComponent<NewOrientation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        SteeringOutput steering = getSteering();
        kinematic.UpdateKinematic(steering, maxSpeed);
        rb.linearVelocity = kinematic.velocity;
        NewOrientation orientation = GetComponent<NewOrientation>();
        character.eulerAngles = new Vector3(0,0,orientation.Calculate(character.eulerAngles.z, rb.linearVelocity));
    }

    public SteeringOutput getSteering()
    {
        result.linear = target.position + newTargetPrediction - character.position  ;
        result.linear.Normalize();
        result.linear *= maxAcceleration;
        result.angular = 0;

        return result;
    }

}