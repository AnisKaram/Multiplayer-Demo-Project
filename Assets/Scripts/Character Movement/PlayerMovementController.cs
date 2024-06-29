using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private PlayerStateMachine m_playerStateMachine;
    private CharacterController m_characterController;

    private float m_playerSpeed;
    public bool m_isGrounded;
    private float m_movementTransitionThreshold;
    private float m_ySpeed;
    private float m_defaultStepOffset;
    private float m_jumpSpeed;

    private int m_groundMasks;

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
        m_jumpSpeed = 5f;
        m_movementTransitionThreshold = 0.05f;
        m_defaultStepOffset = m_characterController.stepOffset;

        m_groundMasks = 1 << 6;
    }
    private void Update()
    {
        // TODO try to fix the isGrounded check
        // TODO add camera follow
        // TODO add player input script
        m_playerStateMachine.Update();
        m_ySpeed += Physics.gravity.y * Time.deltaTime;

        if (m_characterController.isGrounded) // grounded
        {
            m_isGrounded = true;
            m_ySpeed = -0.5f; // instead of 0f to fix the isGrounded glitch
            m_characterController.stepOffset = m_defaultStepOffset;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_isGrounded = false;
                Jump();
            }
        }
        else // not grounded
        {
            m_isGrounded = false;
            m_characterController.stepOffset = 0f; // to fix the glitch stepping on walls
        }

        Move();
    }

    public bool IsWalking()
    {
        float velocityX = Mathf.Abs(m_characterController.velocity.x);
        float velocityZ = Mathf.Abs(m_characterController.velocity.z);

        if (velocityX > m_movementTransitionThreshold || velocityZ > m_movementTransitionThreshold)
        {
            return true;
        }
        return false;
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * m_playerSpeed;
        movementDirection.Normalize();

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = m_ySpeed;

        m_characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        m_ySpeed = m_jumpSpeed;
    }
    private bool IsGrounded()
    {
        RaycastHit hit;
        float rayLength = 2f; // Adjust based on your character's size
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength, m_groundMasks, QueryTriggerInteraction.Ignore))
        {
            Debug.Log($"hit: {hit.collider.name}");
            return true;
        }
        return false;
    }
}