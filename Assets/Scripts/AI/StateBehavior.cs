using UnityEngine;

public abstract class StateBehavior : MonoBehaviour
{
    [SerializeField] private State m_State;
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();

    public State GetState()
    {
        return m_State;
    }

    [ContextMenu("Force OnEnterState")]
    public void ForceState()
    {
        EnemyStateMachine.RequestState(m_State);
    }
}
