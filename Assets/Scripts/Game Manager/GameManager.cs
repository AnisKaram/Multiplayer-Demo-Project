using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;

    [SerializeField] private BasicSpawner m_basicSpawner;
    [SerializeField] private PlayerData m_playerData;

    public static GameManager instance => m_instance;
    public BasicSpawner basicSpawner => m_basicSpawner;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
    }

    private void Start()
    {
        m_basicSpawner.StartGame(m_playerData.playerGameMode);
    }
}