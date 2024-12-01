using UnityEngine;

public class DynamicArrive : MonoBehaviour
{
    public Transform target;          // El objetivo al que moverse
    public float maxSpeed = 5f;       // Velocidad máxima del agente
    public float maxAcceleration = 10f; // Máxima aceleración del agente
    public float targetRadius = 0.5f; // Radio de llegada al objetivo
    public float slowRadius = 3f;     // Radio donde comienza a desacelerar
    public float timeToTarget = 0.1f; // Tiempo deseado para llegar al objetivo

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Calcula la dirección hacia el objetivo
        Vector2 direction = target.position - transform.position;
        float distance = direction.magnitude;

        // Si está dentro del radio de llegada, detén el movimiento
        if (distance < targetRadius)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Calcula la velocidad objetivo dependiendo de la distancia
        float targetSpeed = (distance > slowRadius) ? maxSpeed : maxSpeed * (distance / slowRadius);

        // Calcula la velocidad deseada
        Vector2 desiredVelocity = direction.normalized * targetSpeed;

        // Calcula el cambio de velocidad necesario
        Vector2 steering = desiredVelocity - rb.linearVelocity;

        // Limita la aceleración al máximo permitido
        if (steering.magnitude > maxAcceleration)
        {
            steering = steering.normalized * maxAcceleration;
        }

        // Aplica la aceleración al cuerpo rígido
        rb.linearVelocity += steering * Time.fixedDeltaTime;

        // Limita la velocidad al máximo permitido
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}
