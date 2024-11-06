using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public float maxSpeed;
    private NewOrientation orientation;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orientation = gameObject.AddComponent<NewOrientation>();
    }

    // Update is called once per frame
    void Update()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(movX, movY ); 
        Vector2 velocity = direction.normalized*maxSpeed;
        rb.linearVelocity = velocity;
        rb.rotation = orientation.Calculate(rb.rotation, rb.linearVelocity);
    }
}
