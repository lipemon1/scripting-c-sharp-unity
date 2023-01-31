using UnityEngine;

public class MaterialColorChangeComponent : MonoBehaviour
{
	[SerializeField] private MeshRenderer m_MeshRenderer;
	private float _initialAlphaValue = 0f;
	
	private void Awake()
	{
		_initialAlphaValue = m_MeshRenderer.material.color.a;
		EnemyStateMachine.onNewState += OnNewState;
	}
	
	private void OnDestroy()
	{
		EnemyStateMachine.onNewState -= OnNewState;
	}

	private void OnNewState(State newState)
	{
		Color newColor = Color.white;
		switch (newState)
		{
			case State.Patrol:
				break;
			case State.Idle:
				break;
			case State.Chase:
				newColor = Color.red;
				break;
			case State.Investigate:
				newColor = Color.yellow;
				break;
			default:
				Debug.LogError("No state color configuration found for " + newState);
				break;
		}
		
		newColor.a = _initialAlphaValue;
		m_MeshRenderer.material.color = newColor;
	}
}
