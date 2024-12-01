using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KinematicArrive : MonoBehaviour
{
    private Transform character;
    public Transform target;

    public KinematicSteeringOutput steeringOutput;
    private Rigidbody2D rb;
    public float maxSpeed = 1;

    public float targetRadius = 0.5f, timeToTarget = 0.1f;

    public bool arrive = false;


    // Start is called before the first frame update
    void Start()
    {
        character = transform;
        rb = GetComponent<Rigidbody2D>();
        steeringOutput = gameObject.AddComponent<KinematicSteeringOutput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        rb.linearVelocity = getSteering();

    }


    Vector2 getSteering()
    {
        Vector2 velocityResult = target.position - character.position;
        if (velocityResult.magnitude < targetRadius)
        {
            velocityResult = new Vector2(0, 0);
            arrive = true;
            return  velocityResult;
        }

        velocityResult /= timeToTarget;

        if (velocityResult.magnitude > maxSpeed)
        {
            velocityResult.Normalize();
            velocityResult *= maxSpeed;
        }

        return  velocityResult;
    }
}
