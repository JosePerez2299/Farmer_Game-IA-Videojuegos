using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicFlee : MonoBehaviour
{
    private Transform character;
    public Transform target;
    private Rigidbody2D rb;
    public float maxSpeed = 10;
    public float maxDistance = 20;


    // Start is called before the first frame update
    void Start()
    {
        character = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = getSteering().velocity;
        rb.linearVelocity = velocity;
    }


    KinematicSteeringOutput getSteering()
    {
        KinematicSteeringOutput result = GetComponent<KinematicSteeringOutput>();

        result.velocity = character.position - target.position;

        if (result.velocity.magnitude > maxDistance) {
            result.velocity = Vector2.zero;
            return result;
        }

      
        result.velocity.Normalize();
        result.velocity *= maxSpeed;
        result.rotation = 0;

        return result;
    }
}
