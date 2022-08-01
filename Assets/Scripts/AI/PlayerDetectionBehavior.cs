using UnityEngine;

public class PlayerDetectionBehavior : MonoBehaviour
{
    [SerializeField] private Transform m_Player;
    [SerializeField] private Transform m_Enemy;
    private bool m_PlayerOnSight;
    private bool m_RequestedState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_PlayerOnSight = true;
            m_RequestedState = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int layerMask = 1 << 6;

            RaycastHit hit;
            if (Physics.Raycast(m_Player.position, m_Enemy.position - m_Player.position, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(m_Player.position, m_Enemy.position - m_Player.position, Color.yellow);
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.DrawRay(m_Player.position, m_Enemy.position - m_Player.position, Color.red);
                    TryRequestChaseState();
                }
            }
        }
    }

    private void TryRequestChaseState()
    {
        if (!m_RequestedState)
        {
            m_RequestedState = true;
            EnemyStateMachine.RequestState(State.Chase);   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (EnemyStateMachine.CurrentState() == State.Chase)
            {
                EnemyStateMachine.RequestState(State.Investigate);
            }
            
            m_PlayerOnSight = false;
            m_RequestedState = false;
        }
    }
}
