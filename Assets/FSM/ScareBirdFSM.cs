using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScareBirdFSM : MonoBehaviour
{
    [HideInInspector]
    public ScareBirdSleep sleep;
    public ScareBirdWalking walking;

    public MonoBehaviour initialState;
    public MonoBehaviour currentState;


    void Start()
    {

        sleep = GetComponent<ScareBirdSleep>();
        walking = GetComponent<ScareBirdWalking>();
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
