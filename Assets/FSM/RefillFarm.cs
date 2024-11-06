using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RefillFarm : MonoBehaviour
{
    public bool refill = false;
    FarmerFSM stateMachine;
    private MovementController movement;
    private Transform character;
    private Vector3[] resourceLocation;



    private GameObject[] food;

    private GameObject bed;
    public float targetTime;


    void Start()
    {
        character = transform;
        movement = GetComponent<MovementController>();
        stateMachine = GetComponent<FarmerFSM>();
        bed = GameObject.FindGameObjectWithTag("Depot");


    }

    void Update()
    {
        food = GameObject.FindGameObjectsWithTag("NoRice");
        if (food != null && food.Length >= 1)
        {
            {
                refill = false;
                stateMachine.ChangeState(stateMachine.PlantCorn);

            }
            return;
        }
        else MoveToSleep(bed.transform);

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

