
using UnityEngine;

public class ScareBirdSleep : MonoBehaviour
{
    public bool sleeping = true;

    public float targetTime;
    ScareBirdFSM stateMachine;

    void Start()
    {
        stateMachine = GetComponent<ScareBirdFSM>();
        targetTime = 10f;
    }

    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0)
        {
            targetTime = 10f;
            sleeping = false;
            stateMachine.ChangeState(stateMachine.walking);
        }
    }
}
