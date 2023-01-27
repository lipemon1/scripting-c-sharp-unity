using UnityEngine;
using UnityEngine.AI;

public class PatrolStateBehavior : StateBehavior
{
    [SerializeField] private Transform[] m_WantedPositions;
    private int m_CurWantedPositionIndex = 0;
    private Transform m_CurWantedPosition = null;

    [Space]
    [SerializeField] private NavMeshAgent m_Agent;

    public override void PrepareState()
    {
        base.PrepareState();
        m_WantedPositions = GameLoopControl.Instance.GetPatrolPosition();
    }

    public override void OnStateEnter()
    {
        m_CurWantedPositionIndex = Random.Range(0, m_WantedPositions.Length);
        m_CurWantedPosition = m_WantedPositions[m_CurWantedPositionIndex];

        m_Agent.SetDestination(m_CurWantedPosition.position);
        m_Agent.isStopped = false;
    }

    public override void OnStateUpdate()
    {
        if (m_CurWantedPosition == null)
        {
            return;
        }

        if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
        {
            EnemyStateMachine.RequestState(State.Idle);
        }
    }

    public override void OnStateExit()
    {
        m_CurWantedPosition = null;
        m_Agent.isStopped = true;
    }
}
