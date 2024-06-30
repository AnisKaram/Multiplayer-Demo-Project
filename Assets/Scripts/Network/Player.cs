using Fusion;

public class Player : NetworkBehaviour
{
    private NetworkCharacterControllerPrototype m_controller;

    private void Awake()
    {
        m_controller = GetComponent<NetworkCharacterControllerPrototype>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInput))
        {
            networkInput.direction.Normalize();
            m_controller.Move(5 * networkInput.direction * Runner.DeltaTime);
        }
    }
}
