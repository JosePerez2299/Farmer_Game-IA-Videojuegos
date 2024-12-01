using UnityEngine;

public class BirdMachine : MonoBehaviour
{
    public bool sleeping = false;
    private MovementController movement;
    private GameObject bed,
        hideSpot;
    private FarmerMachine farmer;
    private ScareBirdMachine scareBird;

    public string currentState,
        lastState;

    void Start()
    {
        movement = GetComponent<MovementController>();
        farmer = GameObject.FindGameObjectWithTag("Farmer").GetComponent<FarmerMachine>();
        scareBird = GameObject.FindGameObjectWithTag("ScareBird").GetComponent<ScareBirdMachine>();
        hideSpot = GameObject.FindGameObjectWithTag("HideSpot");
        currentState = "Sleep";
        bed = GameObject.FindGameObjectWithTag("BirdHouse");
    }

    void Update()
    {
        if (currentState == "Hide")
        {
            Hide();
        }
        else if (currentState == "Eat")
        {
            Eat();
        }
        else if (currentState == "Sleep")
        {
            Sleep();
        }
    }

    public void Sleep()
    {
        lastState = "Sleep";

        if (!scareBird.sleeping)
        {
            currentState = "Hide";
            return;
        }

        GameObject food = GameObject.FindGameObjectWithTag("Rice");

        if (food != null && farmer.refill)
        {
            sleeping = false;

            currentState = "Eat";
            return;
        }

        MoveToHouse(bed.transform);
    }

    public void Eat()
    {
        lastState = "Eat";

        if (!scareBird.sleeping)
        {
            currentState = "Hide";
            return;
        }

        GameObject food = GameObject.FindGameObjectWithTag("Rice");

        if (food == null || !farmer.refill)
        {
            currentState = "Sleep";
            return;
        }

        movement.moveTo(food.transform);
    }

    public void Hide()
    {
        if (scareBird.sleeping)
        {
            currentState = lastState;
        }

        movement.moveTo(hideSpot.transform);
    }


    private void MoveToHouse(Transform target)
    {
        Vector3 distance = target.position - transform.position;

        if (distance.magnitude < 2f)
        {
            sleeping = true;
            return;
        }



        movement.moveTo(target);
    }
}
