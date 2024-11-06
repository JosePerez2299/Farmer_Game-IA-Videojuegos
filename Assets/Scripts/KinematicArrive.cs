using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KinematicArrive : MonoBehaviour
{
    private Transform character;
    public Transform target;
    private Rigidbody2D rb;
    public float maxSpeed = 1;

    public float radius, timeToTarget;

    public bool arrive = false;


    // Start is called before the first frame update
    void Start()
    {
        character = transform;
        rb = GetComponent<Rigidbody2D>();
        gameObject.AddComponent<KinematicSteeringOutput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector2 velocity = getSteering().velocity;
        rb.linearVelocity = velocity;
    }


    KinematicSteeringOutput getSteering()
    {
        KinematicSteeringOutput result = GetComponent<KinematicSteeringOutput>();
        result.velocity = target.position - character.position;

        if (result.velocity.magnitude < radius)
        {
            result.velocity = new Vector2(0, 0);
            arrive = true;
            return result;
        }

        result.velocity /= timeToTarget;

        if (result.velocity.magnitude > maxSpeed)
        {
            result.velocity.Normalize();
            result.velocity *= maxSpeed;
        }
        result.rotation = 0;

        return result;
    }
}
