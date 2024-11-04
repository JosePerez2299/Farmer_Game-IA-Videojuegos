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

    private NewOrientation orientation;

    // Start is called before the first frame update
    void Start()
    {
        character = transform;
        orientation = gameObject.AddComponent<NewOrientation>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = getSteering().velocity;
        rb.velocity = velocity;
    }


    KinematicSteeringOutput getSteering()
    {
        KinematicSteeringOutput result = GetComponent<KinematicSteeringOutput>();
        result.velocity = target.position - character.position;

        if (result.velocity.magnitude < radius)
        {
            result.velocity = new Vector2(0, 0);
            return result;
        }

        result.velocity /= timeToTarget;

        if (result.velocity.magnitude > maxSpeed)
        {
            result.velocity.Normalize();
            result.velocity *= maxSpeed;
        }

        float newOrientation = orientation.Calculate(character.eulerAngles.z, result.velocity);

        character.eulerAngles = new Vector3(0, 0, newOrientation);


        result.rotation = 0;

        return result;
    }
}
