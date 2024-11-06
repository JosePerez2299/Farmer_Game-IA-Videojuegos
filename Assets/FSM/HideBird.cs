
using UnityEngine;

public class HideBird : MonoBehaviour
{
    public BirdFSM stateMachine;
    private MovementController movement;


    private ScareBirdSleep sleepScareBird;


    private GameObject  hideSpot;


    void Start()
    {

        movement = GetComponent<MovementController>();
        hideSpot = GameObject.FindGameObjectWithTag("HideSpot");
        sleepScareBird = GameObject.FindGameObjectWithTag("ScareBird").GetComponent<ScareBirdSleep>();



    }

    void Update()
    {

        if (sleepScareBird.sleeping)
        {
            stateMachine.ChangeState(stateMachine.Sleep);
            return;
        } else MoveToHide(hideSpot.transform);

    }

    private void MoveToHide(Transform bedTransform)
    {
        movement.moveTo(bedTransform);
    }
}

