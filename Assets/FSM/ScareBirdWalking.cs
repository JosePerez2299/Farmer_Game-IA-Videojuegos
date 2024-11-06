
using UnityEngine;

public class ScareBirdWalking : MonoBehaviour
{
    ScareBirdFSM stateMachine;
    private ScareBirdSleep sleepScareBird;
    public float targetTime = 10f;

    public float rotationSpeed = 360f; // Velocidad de rotación (grados por segundo)

    void Start()
    {
        stateMachine = GetComponent<ScareBirdFSM>();
        targetTime = 10f;
        sleepScareBird = GameObject.FindGameObjectWithTag("ScareBird").GetComponent<ScareBirdSleep>();

        // Inicia la corrutina que controla la rotación y el temporizador
    }

    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0)
        {
            targetTime = 10f;
            sleepScareBird.sleeping = true;
            stateMachine.ChangeState(stateMachine.sleep);
        }
        else
            Rotate();
         
    }

    void Rotate()
    {
        // Rotar el personaje sobre su propio eje Z a una velocidad constante
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
