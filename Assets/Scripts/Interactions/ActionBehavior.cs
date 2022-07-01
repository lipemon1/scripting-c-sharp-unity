using System;
using UnityEngine;

public class ActionBehavior : MonoBehaviour
{
    [SerializeField]
    private PlayerInput m_PlayerInput;
    private IActionObject m_ActionObjectAvailable;

    private void Update()
    {
        if (m_ActionObjectAvailable != null)
        {
            if (m_PlayerInput.ActionInput)
            {
                m_ActionObjectAvailable.DoAction();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ActionObject"))
        {
            m_ActionObjectAvailable = other.GetComponent<IActionObject>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ActionObject"))
        {
            m_ActionObjectAvailable = null;
        }
    }
}
