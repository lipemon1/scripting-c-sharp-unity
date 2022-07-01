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
            if (Physics.Raycast(m_Enemy.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(m_Enemy.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if(Vector3.Distance(hit.collider.transform.position, m_Enemy.position) > Vector3.Distance(m_Player.position, m_Enemy.position))
                {
                    TryRequestChaseState();
                }
            }
            else
            {
                if (m_PlayerOnSight)
                {
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
        }
    }
}
