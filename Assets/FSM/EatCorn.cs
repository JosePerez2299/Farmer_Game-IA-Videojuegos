using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EatCorn : MonoBehaviour
{
    BirdFSM stateMachine;
    private MovementController movement;
    private Transform character;
    private Vector3[] resourceLocation;
    private GameObject farmer;

    private RefillFarm refillFarmer;


    private GameObject food;


    void Start()
    {
        movement = GetComponent<MovementController>();
        stateMachine = GetComponent<BirdFSM>();
        refillFarmer = GameObject.FindGameObjectWithTag("Farmer").GetComponent<RefillFarm>();




    }

    void Update()
    {
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
