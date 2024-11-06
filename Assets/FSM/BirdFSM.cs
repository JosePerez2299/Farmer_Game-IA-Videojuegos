using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BirdFSM : MonoBehaviour
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
