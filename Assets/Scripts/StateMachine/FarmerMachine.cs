using System.Linq;
using UnityEngine;

public class FarmerMachine : MonoBehaviour
{
    public bool refill = false;

    private MovementController movement;

    private ScareBirdMachine scareBird;
    private BirdMachine bird;


    private string currentState,
        lastState;

    private GameObject hideSpot,
        refillSpot;

    void Start()
    {
        movement = GetComponent<MovementController>();
        scareBird = GameObject.FindGameObjectWithTag("ScareBird").GetComponent<ScareBirdMachine>();
        bird = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdMachine>();

        currentState = "Refill";
        hideSpot = GameObject.FindGameObjectWithTag("HideSpot");
        refillSpot = GameObject.FindGameObjectWithTag("Depot");
    }

    void Update()
    {
        if (currentState == "Hide")
        {
            Hide();
        }
        else if (currentState == "Plant")
        {
            Plant();
        }
        else if (currentState == "Refill")
        {
            Refill();
        }
    }

    public void Refill()
    {
        lastState = "Refill";

        if (!scareBird.sleeping)
        {
            currentState = "Hide";
            return;
        }

        GameObject[] food = GameObject.FindGameObjectsWithTag("NoRice");

        if (food != null && food.Length >= 3)
        {
            refill = false;
            currentState = "Plant";
            return;
        }

        MoveToRefill(refillSpot.transform);
    }

    public void Plant()
    {
        lastState = "Plant";

        if (!scareBird.sleeping)
        {
            currentState = "Hide";
            return;
        }

        GameObject[] food = GameObject.FindGameObjectsWithTag("NoRice");

        if (food != null && food.Any() && bird.sleeping == true)
        {
            movement.moveTo(food[0].transform);
            return;
        }

        currentState = "Refill";
    }

    public void Hide()
    {
        if (scareBird.sleeping)
        {
            currentState = lastState;
        }

        movement.moveTo(hideSpot.transform);
    }

    private void MoveToRefill(Transform target)
    {
        Vector3 distance = target.position - transform.position;

        if (distance.magnitude < 1f)
        {
            refill = true;
            return;
        }

        movement.moveTo(target);
    }
}
