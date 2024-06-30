using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Fields
    private PlayerStateMachine m_playerStateMachine;
    private CharacterController m_characterController;

    private float m_playerSpeed;
    private bool m_isGrounded;
    private float m_movementTransitionThreshold;
    private float m_ySpeed;
    private float m_defaultStepOffset;
    private float m_jumpSpeed;
    #endregion

    #region Properties
    public PlayerStateMachine playerStateMachine => m_playerStateMachine;
    public CharacterController characterController => m_characterController;
    public bool isGrounded => m_isGrounded;
    public float movementTransitionThreshold => m_movementTransitionThreshold;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
        m_playerStateMachine = new PlayerStateMachine(this);

        m_playerStateMachine.Initialize(m_playerStateMachine.idleState);

        m_playerSpeed = 5f;
        m_jumpSpeed = 5f;
        m_movementTransitionThreshold = 0.05f;
        m_defaultStepOffset = m_characterController.stepOffset;
    }
    private void Update()
    {
        // TODO 1. try to fix the isGrounded check: done
        // TODO 2. add camera follow: done
        // TODO 3. add player input script: done
        m_playerStateMachine.Update();

        UpdateIfGroundedOrNot();
        Move();
    }
    #endregion

    #region Public Methods
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
    #endregion

    #region Private Methods
    private void Move()
    {
        Vector2 directionInput = PlayerInput.instance.MovementDirection();
        Vector3 movementDirection = new Vector3(directionInput.x, 0, directionInput.y);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * m_playerSpeed;
        movementDirection.Normalize();

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = m_ySpeed;

        m_characterController.Move(velocity * Time.deltaTime);
    }

    private void UpdateIfGroundedOrNot()
    {
        m_ySpeed += Physics.gravity.y * Time.deltaTime;

        if (m_characterController.isGrounded) // grounded
        {
            m_isGrounded = true;
            m_ySpeed = -0.5f; // instead of 0f to fix the isGrounded glitch
            m_characterController.stepOffset = m_defaultStepOffset;

            bool isJumpPressed = PlayerInput.instance.IsJumpPressed();
            if (isJumpPressed)
            {
                Jump();
            }
        }
        else // not grounded
        {
            m_isGrounded = false;
            m_characterController.stepOffset = 0f; // to fix the glitch stepping on walls
        }
    }
    private void Jump()
    {
        m_ySpeed = m_jumpSpeed;
    }
    #endregion
}