using System;
using UnityEngine;

public class PlayerCloseDetectionBehavior : MonoBehaviour
{
    private Action m_OnGameOverCallback;

    public void PrepareGameOver(System.Action gameOverCallback)
    {
        m_OnGameOverCallback = gameOverCallback;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_OnGameOverCallback?.Invoke();
        }
    }
}