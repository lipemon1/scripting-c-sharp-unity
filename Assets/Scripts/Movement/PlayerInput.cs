using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float m_HorinzontalInput;
    private float m_VerticalInput;
    private bool m_ActionInput;

    public float HorizontalInput => m_HorinzontalInput;
    public float VerticalInput => m_VerticalInput;
    public bool ActionInput => m_ActionInput;
    
    // Update is called once per frame
    void Update()
    {
        m_HorinzontalInput = Input.GetAxis("Horizontal");
        m_VerticalInput = Input.GetAxis("Vertical");
        m_ActionInput = Input.GetButtonDown("Jump");
        
        if(m_ActionInput)
            Debug.Log("Pressed Space");
    }
}
