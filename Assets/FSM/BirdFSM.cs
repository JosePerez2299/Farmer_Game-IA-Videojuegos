using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BirdFSM : StateMachine
{
    [HideInInspector]
    public Sleep Sleep;
    public EatCorn EatCorn ;

    public MonoBehaviour initialState;
    public MonoBehaviour currentState;


    void Start()
    {

        EatCorn = GetComponent<EatCorn>();
        Sleep = GetComponent<Sleep>();
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
