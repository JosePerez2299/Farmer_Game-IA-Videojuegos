
using UnityEngine;

public class RefillFarm : MonoBehaviour
{
    public bool refill = false;
    FarmerFSM stateMachine;
    private MovementController movement;
    private Transform character;

    private ScareBirdSleep sleepScareBird;

    private GameObject[] food;

    private GameObject bed;
    public float targetTime;

    void Start()
    {
        character = transform;
        movement = GetComponent<MovementController>();
        stateMachine = GetComponent<FarmerFSM>();
        bed = GameObject.FindGameObjectWithTag("Depot");
        sleepScareBird = GameObject
            .FindGameObjectWithTag("ScareBird")
            .GetComponent<ScareBirdSleep>();
    }

    void Update()
    {
        if (!sleepScareBird.sleeping)
        {
            stateMachine.ChangeState(stateMachine.hide);
            return;
        }

        food = GameObject.FindGameObjectsWithTag("NoRice");
        if (food != null && food.Length >= 4)
        {
            {
                refill = false;
                stateMachine.ChangeState(stateMachine.PlantCorn);
            }
            return;
        }
        else
            MoveToSleep(bed.transform);
    }

    private void MoveToSleep(Transform bedTransform)
    {
        Vector3 distance = bedTransform.position - character.position;

        if (distance.magnitude < 1f)
        {
            refill = true;
            return;
        }
        movement.moveTo(bedTransform);
    }
}
