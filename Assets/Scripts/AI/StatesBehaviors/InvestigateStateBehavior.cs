using System.Collections;
using UnityEngine;

public class InvestigateStateBehavior : StateBehavior
{
    [SerializeField] private float m_RotateSpeed;
    [SerializeField] private float m_TimeBeforeChangeDirection;
    private WaitForSeconds m_SecondsBeforeChangeDirection;
    private WaitForSeconds m_TimeBeforeRequestPatrol;
    private int m_Direction = 1;

    private void Awake()
    {
        m_SecondsBeforeChangeDirection = new WaitForSeconds(m_TimeBeforeChangeDirection);
        m_TimeBeforeRequestPatrol = new WaitForSeconds(m_TimeBeforeChangeDirection * 2);
    }

    public override void OnStateEnter()
    {
        m_Direction = 1;
        StartCoroutine(ChangeDirectionAndRequestState());
    }

    private IEnumerator ChangeDirectionAndRequestState()
    {
        yield return m_SecondsBeforeChangeDirection;
        m_Direction = -1;
        yield return m_TimeBeforeRequestPatrol;
        EnemyStateMachine.RequestState(State.Patrol);
    }

    public override void OnStateUpdate()
    {
        transform.Rotate(Vector3.up, m_RotateSpeed * Time.deltaTime * m_Direction);
    }

    public override void OnStateExit()
    {
        StopCoroutine(ChangeDirectionAndRequestState());
    }
}
