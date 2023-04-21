using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField]
	private Transform m_CameraTransform;
	
	[SerializeField]
	private Transform m_Target;
	
	[SerializeField]
	private float m_SmoothSpeed = 1f;

	private void LateUpdate()
	{
		Vector3 wantedPos = m_Target.position;
		wantedPos.y = m_CameraTransform.position.y;

		Vector3 smoothedPosition = Vector3.Lerp(m_CameraTransform.position, wantedPos, m_SmoothSpeed * Time.deltaTime);
		m_CameraTransform.position = smoothedPosition;
	}
}
