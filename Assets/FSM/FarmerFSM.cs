using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerFSM : StateMachine
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
        hide = GetComponent<Hide>();
        ChangeState(initialState);

       
    }

    public override void ChangeState(MonoBehaviour newState)
    {
        if (currentState != null)
        {
            currentState.enabled = false;
        }
        lastState = currentState;
        currentState = newState;
        currentState.enabled = true;
    }
}
