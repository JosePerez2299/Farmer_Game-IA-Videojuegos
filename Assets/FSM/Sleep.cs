using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    public bool sleeping = true;
    BirdFSM stateMachine;
    private MovementController movement;
    private Transform character;
    private Vector3[] resourceLocation;

    private RefillFarm refillFarmer;


    private GameObject food, bed;


    void Start()
    {
        character = transform;
        movement = GetComponent<MovementController>();
        stateMachine = GetComponent<BirdFSM>();
        bed = GameObject.FindGameObjectWithTag("BirdHouse");
        refillFarmer = GameObject.FindGameObjectWithTag("Farmer").GetComponent<RefillFarm>();



    }

    void Update()
    {
        food = GameObject.FindGameObjectWithTag("Rice");

        if (food != null && refillFarmer.refill)
        {
            sleeping = false;
            stateMachine.ChangeState(stateMachine.EatCorn);
            return;
        } else MoveToSleep(bed.transform);

    }

    private void MoveToSleep(Transform bedTransform)
    {
        Vector3 distance = bedTransform.position - character.position;

        if (distance.magnitude < 1f) {
            sleeping = true;
            return;
        }
        movement.moveTo(bedTransform);
    }
}

