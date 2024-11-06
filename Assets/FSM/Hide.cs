
using UnityEngine;

public class Hide : MonoBehaviour
{
    public StateMachine stateMachine;
    private MovementController movement;
    private Transform character;

    private ScareBirdSleep sleepScareBird;


    private GameObject  hideSpot;


    void Start()
    {
        character = transform;
        movement = GetComponent<MovementController>();
        hideSpot = GameObject.FindGameObjectWithTag("HideSpot");
        sleepScareBird = GameObject.FindGameObjectWithTag("ScareBird").GetComponent<ScareBirdSleep>();



    }

    void Update()
    {

        if (sleepScareBird.sleeping)
        {
            stateMachine.ChangeState(stateMachine.lastState);
            return;
        } else MoveToHide(hideSpot.transform);

    }

    private void MoveToHide(Transform bedTransform)
    {
        Vector3 distance = bedTransform.position - character.position;

        if (distance.magnitude < 1f) {
            return;
        }
        movement.moveTo(bedTransform);
    }
}

