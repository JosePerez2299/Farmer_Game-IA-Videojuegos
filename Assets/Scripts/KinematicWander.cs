using Unity.VisualScripting;
using UnityEngine;

public class KinematicWander : MonoBehaviour
{
    private Transform character; // Suponiendo que tienes una clase StaticCharacter con la propiedad 'orientation'
    private Rigidbody2D rb;
    private KinematicSteeringOutput result;
    public float maxSpeed;
    public float maxRotation;

    void Start()
    {
        character = transform;
        rb = GetComponent<Rigidbody2D>();
        result = GetComponent<KinematicSteeringOutput>();
    }

    // Update is called once per frame
    void Update()
    {
        result = GetSteering();
        rb.velocity = result.velocity;
        rb.rotation = result.rotation;


    }


    // Esta función retorna un objeto KinematicSteeringOutput, el cual debe ser definido.
    public KinematicSteeringOutput GetSteering()
    {
        float orientation = character.eulerAngles.z;
        // Obtener la velocidad a partir de la orientación del personaje como un vector.
        result.velocity = maxSpeed * OrientationAsVector(orientation);

        // Cambiar la orientación de manera aleatoria.
        result.rotation = orientation + RandomBinomial() * maxRotation;

        return result;
    }

    // Función para convertir la orientación a un vector 2D.
    private Vector2 OrientationAsVector(float orientation)
 {
        // Convertir los grados a radianes
        float radians = orientation * Mathf.Deg2Rad;

        // Usar trigonometría para obtener la dirección
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
    }

    // Función para obtener un valor aleatorio entre -1 y 1.
    private float RandomBinomial()
    {
        return Random.value - Random.value;
    }
}
