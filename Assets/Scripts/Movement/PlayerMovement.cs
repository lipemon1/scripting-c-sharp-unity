using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController m_CharacterController;
    [SerializeField]
    private PlayerInput m_PlayerInput;
    [SerializeField]
    private float m_PlayerSpeed = 10f;
    [SerializeField]
    private float m_Gravity = -9.81f;

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerInput.HorizontalInput == 0f && m_PlayerInput.VerticalInput == 0f)
        {
            return;
        }
    
        Vector3 move = new Vector3(m_PlayerInput.HorizontalInput, 0, m_PlayerInput.VerticalInput);
        
        if(move.y < 0)
        {
            move.y = 0;
        }
        move.y += m_Gravity;
        
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = new Vector3(move.x, 0, move.z);
        }

        m_CharacterController.Move(move * Time.deltaTime * m_PlayerSpeed);
    }
}
