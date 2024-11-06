using System.Collections;
using UnityEngine;

public class ScareBirdMachine : MonoBehaviour
{
    public bool sleeping = true;

    private MovementController movement;

    private ScareBirdMachine scareBird;

    private string currentState;

    private GameObject hideSpot,
        refillSpot;

    public float sleepingTime = 8f;  // Tiempo de reposo
    public float crazyTime = 10f;    // Tiempo de rotación
    public float rotationSpeed = 360f; // Velocidad de rotación (grados por segundo)

    void Start()
    {
        movement = GetComponent<MovementController>();
        scareBird = GameObject.FindGameObjectWithTag("ScareBird").GetComponent<ScareBirdMachine>();
        currentState = "Sleep";
        hideSpot = GameObject.FindGameObjectWithTag("HideSpot");
        refillSpot = GameObject.FindGameObjectWithTag("Depot");

        // Iniciar el ciclo de rotación y reposo
        StartCoroutine(CycleRotationAndSleep());
    }

    void Update()
    {
        if (currentState == "Sleep")
        {
            sleeping = true;
            // Aquí no hace falta hacer nada, ya que el reposo se maneja con la corutina
        }
        else if (currentState == "RotateFast")
        {
            sleeping = false;
            RotateFast();
        }
    }

    // Corutina que maneja el ciclo de rotación y reposo
    IEnumerator CycleRotationAndSleep()
    {
        while (true)
        {
            // Primero el reposo
            currentState = "Sleep";
            yield return new WaitForSeconds(sleepingTime); // Reposo durante 'sleepingTime'

            // Luego la rotación
            currentState = "RotateFast";
            yield return new WaitForSeconds(crazyTime); // Rotación durante 'crazyTime'
        }
    }

    public void RotateFast()
    {
        Rotate();  // Ejecutar la rotación en el estado de "RotateFast"
    }

    void Rotate()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);  // Rota de manera constante
    }
}
