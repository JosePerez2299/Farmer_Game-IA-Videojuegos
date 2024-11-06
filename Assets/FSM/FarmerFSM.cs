using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FarmerFSM : MonoBehaviour
{
    [HideInInspector]
    public RefillFarm RefillFarm;
    public PlantCorn PlantCorn;

    public MonoBehaviour initialState;
    public MonoBehaviour currentState;


    void Start()
    {

        RefillFarm = GetComponent<RefillFarm>();
        PlantCorn = GetComponent<PlantCorn>();
        ChangeState(initialState);

    }

    public void ChangeState(MonoBehaviour newState)
    {
        if (currentState != null)
        {
            currentState.enabled = false;
        }

        currentState = newState;
        currentState.enabled = true;


    }


}
