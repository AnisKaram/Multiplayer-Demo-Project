using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private PlayerStateMachine m_playerStateMachine;
    private CharacterController m_characterController;

    private float m_playerSpeed;
    private bool m_isGrounded;
    private float m_movementTransitionThreshold;

    public PlayerStateMachine playerStateMachine => m_playerStateMachine;
    public CharacterController characterController => m_characterController;
    public bool isGrounded => m_isGrounded;
    public float movementTransitionThreshold => m_movementTransitionThreshold;

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
        m_playerStateMachine = new PlayerStateMachine(this);

        m_playerStateMachine.Initialize(m_playerStateMachine.idleState);

        m_playerSpeed = 5f;
        m_movementTransitionThreshold = 0.05f;
    }
    private void Update()
    {
        m_playerStateMachine.Update();
        Move();
    }

    // TODO add a player input script
    // TODO add jumping mechanic
    // TODO continue JumpState
    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        m_characterController.Move(move * Time.deltaTime * m_playerSpeed);
    }
}