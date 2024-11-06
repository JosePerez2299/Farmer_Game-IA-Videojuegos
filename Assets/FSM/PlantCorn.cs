
using UnityEngine;

public class PlantCorn : MonoBehaviour
{
    FarmerFSM stateMachine;
    private MovementController movement;
    private Transform character;
    private Vector3[] resourceLocation;
    private GameObject farmer;
    private Sleep sleep;

    private ScareBirdSleep sleepScareBird;

    private GameObject food;

    void Start()
    {
        movement = GetComponent<MovementController>();
        stateMachine = GetComponent<FarmerFSM>();
        sleep = GetComponent<Sleep>();
        sleepScareBird = GameObject
            .FindGameObjectWithTag("ScareBird")
            .GetComponent<ScareBirdSleep>();
    }

    void Update()
    {
        // if (!sleepScareBird.sleeping)
        // {
        //     stateMachine.ChangeState(stateMachine.hide);
        //     return;
        // }
        food = GameObject.FindGameObjectWithTag("NoRice");

        if (food == null)
        {
            stateMachine.ChangeState(stateMachine.RefillFarm);
            return;
        }

        MoveToEat(food.transform);
    }

    private void MoveToEat(Transform food)
    {
        Vector3 distance = food.position - transform.position;

        if (distance.magnitude < 1f)
        {
            return;
        }

        movement.moveTo(food);
    }
}
