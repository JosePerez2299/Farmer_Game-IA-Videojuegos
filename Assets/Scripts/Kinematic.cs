using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Kinematic : MonoBehaviour
{
    public Transform character;
    public Vector2 position, velocity;
    public float orientation, rotation;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        character = transform;
        velocity = rb.linearVelocity;
        position = rb.position;
        rotation = rb.rotation;
        orientation = rb.rotation;

    }

    public void UpdateKinematic(SteeringOutput steering, float maxSpeed)
    {
        float time = Time.deltaTime;

        position += velocity * time;
    
        orientation += rotation * time;

        velocity += steering.linear * time;
  

        rotation += steering.angular * time;

        if (velocity.magnitude > maxSpeed) {
            velocity.Normalize();
            velocity *= maxSpeed;

        }

    }
}
