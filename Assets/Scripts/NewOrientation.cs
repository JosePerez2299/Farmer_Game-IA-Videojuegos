using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewOrientation : MonoBehaviour
{
  public float Calculate(float current, Vector2 velocity)
    {
        // Asegúrate de que haya una velocidad.
        if (velocity.magnitude > 0)
        {
            // Calcula la orientación a partir de la velocidad.
            return Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        }
        // De lo contrario, usa la orientación actual.
        else
        {
            return current;
        } 
    }
}
