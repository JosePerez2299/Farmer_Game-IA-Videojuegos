using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWhereYoureGoing : Align
{
    // Start is called before the first frame update
    void Start()
    {
        onFace = true;
        rb = GetComponent<Rigidbody2D>();
        result = gameObject.AddComponent<SteeringOutput>();
        kinematic = gameObject.AddComponent<Kinematic>();


    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            return;
        }
        // Llamar a getSteering para ajustar la orientación hacia el objetivo
        SteeringOutput steering = getSteering();

        // Si no se necesita más rotación, mantener la orientación actual
        if (steering == null)
        {

            return;
        }

        // Aplicar la rotación calculada al Rigidbody2D
        kinematic.UpdateKinematic(steering, 0);
        rb.rotation = kinematic.rotation;

    }

    public new SteeringOutput getSteering()
    {
        Vector3 velocity = rb.velocity;
        // Calcular la dirección hacia el objetivo
        if (velocity.magnitude == 0)
        {
            return null;
        }


        // Calcular la orientación necesaria para mirar al objetivo
        orientation = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        // Utilizar la lógica base de Align para finalizar el cálculo
        return base.getSteering();
    }
}
