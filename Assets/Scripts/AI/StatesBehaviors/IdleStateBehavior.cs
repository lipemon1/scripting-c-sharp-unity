using System.Collections;
using UnityEngine;

public class IdleStateBehavior : StateBehavior
{
    [SerializeField] private float m_WaitTime;
    private WaitForSeconds m_WaitTimeYield;

    private void Awake()
    {
        m_WaitTimeYield = new WaitForSeconds(m_WaitTime);
    }

    public override void OnStateEnter()
    {
        StartCoroutine(StartPatrolRequest());
    }

    private IEnumerator StartPatrolRequest()
    {
        yield return m_WaitTimeYield;
        EnemyStateMachine.RequestState(State.Patrol);
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateExit()
    {
        StopCoroutine(StartPatrolRequest());
    }
}
