using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Fields
    private InputControls m_inputControls;

    private static PlayerInput m_instance;
    #endregion

    #region Properties
    public static PlayerInput instance => m_instance;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        m_inputControls = new InputControls();
        m_instance = this;
    }
    private void OnEnable()
    {
        m_inputControls.Movement.Enable();
    }
    private void OnDisable()
    {
        m_inputControls.Movement.Disable();
    }
    #endregion

    #region Public Methods
    public Vector2 MovementDirection()
    {
        return m_inputControls.Movement.WASD.ReadValue<Vector2>();
    }
    public bool IsJumpPressed()
    {
        return m_inputControls.Movement.Jump.IsPressed();
    }
    #endregion
}