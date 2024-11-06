using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : MonoBehaviour
{

    private Transform character;
    public Transform target;
    public Rigidbody2D rb;
    private KinematicSteeringOutput result;
    public float maxSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        character = transform;
        rb = GetComponent<Rigidbody2D>();
        result = GetComponent<KinematicSteeringOutput>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = funSteeringOutput().velocity;
        rb.linearVelocity = velocity;
    }

    KinematicSteeringOutput funSteeringOutput()
    {
        result.velocity = target.position - character.position;
        result.velocity.Normalize();
        result.velocity *= maxSpeed;
        result.rotation = 0;


        return result;
    }

}
