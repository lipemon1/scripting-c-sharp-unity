using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActionObject : MonoBehaviour, IActionObject
{
    [SerializeField]
    private Transform m_DoorTransform;
    [SerializeField]
    private Collider m_DoorCollider;
    private bool m_IsOpen = false;

    public void DoAction()
    {
        if(m_IsOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }
    
    private void OpenDoor()
    {
        m_DoorCollider.enabled = false;
        m_DoorTransform.Rotate(Vector3.up, 90);
        m_IsOpen = true;
    }
    
    private void CloseDoor()
    {
        m_DoorTransform.Rotate(Vector3.up, -90);
        m_DoorCollider.enabled = true;
        m_IsOpen = false;
    }
}
