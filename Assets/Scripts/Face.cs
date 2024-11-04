using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align
{
    void Start()
    {
        // Configurar para que el objeto siempre mire al objetivo
        onFace = true;
        rb = GetComponent<Rigidbody2D>();
        result = gameObject.AddComponent<SteeringOutput>();
        kinematic = gameObject.AddComponent<Kinematic>();
    }

    void Update()
    {
        // Llamar a getSteering para ajustar la orientación hacia el objetivo
        SteeringOutput steering = getSteering();
        
        // Si no se necesita más rotación, mantener la orientación actual
        if (steering.angular == 0f)
        {
            return;
        }

        // Aplicar la rotación calculada al Rigidbody2D
        kinematic.UpdateKinematic(steering, 0);
        rb.rotation = kinematic.rotation;
    }

    public new SteeringOutput getSteering()
    {
        // Calcular la dirección hacia el objetivo
        Vector2 direction = target.position - transform.position;

        // Si ya estamos en la posición objetivo, mantener el resultado anterior
        if (direction.magnitude == 0)
        {
            return result;
        }

        // Calcular la orientación necesaria para mirar al objetivo
        orientation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Utilizar la lógica base de Align para finalizar el cálculo
        return base.getSteering();
    }
}
