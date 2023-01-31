using System;
using UnityEngine;

public enum State
{
    Patrol,
    Idle,
    Chase,
    Investigate
}

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State m_InitialState = State.Idle;
    private static StateBehavior[] _StatesAvailable = new StateBehavior[]{};
    private static StateBehavior _CurState = null;

    public delegate void StateDelegate(State newState);
    public static StateDelegate onNewState;
    private void Awake()
    {
        _StatesAvailable = transform.GetComponents<StateBehavior>();
    }

    private void Start()
    {
        foreach (StateBehavior stateBehavior in _StatesAvailable)
        {
            stateBehavior.PrepareState();
        }
        RequestState(m_InitialState);
    }

    private void Update()
    {
        if (IsStateRunning())
        {
            _CurState.OnStateUpdate();
        }
    }

    public static void RequestState(State wantedState)
    {
        StateBehavior wantedStateBehavior = GetStateBehaviorByType(wantedState, _StatesAvailable);
        if (wantedStateBehavior == null)
        {
            Debug.LogError($"No state found for [{wantedState}]");
            return;
        }

        if (IsStateRunning())
        {
            Debug.Log($"OnStateExit [{_CurState.GetType()}]");
            _CurState.OnStateExit();
        }

        _CurState = wantedStateBehavior;
        if(onNewState != null)
        {
            onNewState.Invoke(_CurState.GetState());
        }
        Debug.Log($"OnStateEnter [{_CurState.GetType()}]");
        _CurState.OnStateEnter();
    }

    private static StateBehavior GetStateBehaviorByType(State wantedState, StateBehavior[] statesAvailable)
    {
        foreach (StateBehavior stateBehavior in statesAvailable)
        {
            if (stateBehavior.GetState() == wantedState)
            {
                return stateBehavior;
            }
        }

        return null;
    }

    private static bool IsStateRunning()
    {
        return _CurState != null;
    }

    public static State CurrentState()
    {
        return _CurState.GetState();
    }
}
