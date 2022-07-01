using UnityEngine;
using UnityEngine.AI;

public class ChaseStateBehavior : StateBehavior
{
    [SerializeField] private Transform m_Player;
    [SerializeField] private NavMeshAgent m_Agent;
    
    public override void OnStateEnter()
    {
        m_Agent.isStopped = false;
    }

    public override void OnStateUpdate()
    {
        m_Agent.SetDestination(m_Player.position);
    }

    public override void OnStateExit()
    {
        m_Agent.isStopped = true;
    }
}
