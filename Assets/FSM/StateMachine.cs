using UnityEngine;

public abstract class StateMachine: MonoBehaviour{
    public  MonoBehaviour lastState;
    public MonoBehaviour hide;

    public abstract void ChangeState(MonoBehaviour newState);
}