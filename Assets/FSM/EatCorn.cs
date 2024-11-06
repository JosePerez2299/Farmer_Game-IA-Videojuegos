
using UnityEngine;

public class EatCorn : MonoBehaviour
{
    BirdFSM stateMachine;
    private MovementController movement;
    private Transform character;
    private Vector3[] resourceLocation;
    private GameObject farmer;

    private RefillFarm refillFarmer;

    private ScareBirdSleep sleepScareBird;

    private GameObject food;


    void Start()
    {
        movement = GetComponent<MovementController>();
        stateMachine = GetComponent<BirdFSM>();
        refillFarmer = GameObject.FindGameObjectWithTag("Farmer").GetComponent<RefillFarm>();
        sleepScareBird = GameObject.FindGameObjectWithTag("ScareBird").GetComponent<ScareBirdSleep>();

    }

    void Update()
    {
        if (!sleepScareBird.sleeping) {

            stateMachine.ChangeState(stateMachine.hide);
            return;
        }


        food = GameObject.FindGameObjectWithTag("Rice");

        if (food == null || !refillFarmer.refill)
        {
            stateMachine.ChangeState(stateMachine.Sleep);
            return;
        }


        MoveToEat(food.transform);



    }

    private void MoveToEat(Transform food)
    {
        movement.moveTo(food);
    }
}
