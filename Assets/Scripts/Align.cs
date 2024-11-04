using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour
{
    protected Transform character;
    public Transform target;
    protected Kinematic kinematic;
    protected Rigidbody2D rb;
    protected SteeringOutput result;
    protected bool onFace = false;
    public float maxAngularAcceleration = 100, maxRotation = 100, orientation;

    public float targetRadius = 1, slowRadius = 10, timeToTarget = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        // Configurar el personaje y los componentes de física
        character = transform;
        rb = GetComponent<Rigidbody2D>();
        result = gameObject.AddComponent<SteeringOutput>();
        kinematic = gameObject.AddComponent<Kinematic>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener los datos de dirección y aplicarlos
        SteeringOutput steering = getSteering();
        kinematic.UpdateKinematic(steering, 0);
        rb.rotation = kinematic.rotation;
    }

    public virtual SteeringOutput getSteering()
    {
        // Obtener el componente SteeringOutput
        result = GetComponent<SteeringOutput>();

        float targetRotation, angularAcceleration;
        float rotation;

        // Calcular la rotación deseada según si se está usando Face o Align
        if (!onFace)
        {
            rotation = Mathf.DeltaAngle(transform.eulerAngles.z, target.eulerAngles.z);
        }
        else
        {
            rotation = -Mathf.DeltaAngle(orientation, transform.eulerAngles.z);
        }

        // Determinar el tamaño de la rotación
        float rotationSize = Mathf.Abs(rotation);

        // Si la rotación es menor que el radio objetivo, detener la rotación
        if (rotationSize < targetRadius)
        {
            result.linear = Vector2.zero;
            result.angular = 0f;
            return result;
        }

        // Ajustar la velocidad de rotación según la distancia al objetivo
        if (rotationSize > slowRadius)
        {
            targetRotation = maxRotation;
        }
        else
        {
            targetRotation = maxRotation * rotationSize / slowRadius;
        }

        // Aplicar la dirección correcta a la rotación
        targetRotation *= Mathf.Sign(rotation);

        // Calcular la aceleración angular
        result.angular = targetRotation - rb.angularVelocity;
        result.angular /= timeToTarget;
        angularAcceleration = Mathf.Abs(result.angular);

        // Limitar la aceleración angular si excede el máximo permitido
        if (angularAcceleration > maxAngularAcceleration)
        {
            result.angular = Mathf.Sign(result.angular) * maxAngularAcceleration;
        }

        result.linear = Vector2.zero;
        return result;
    }
}
