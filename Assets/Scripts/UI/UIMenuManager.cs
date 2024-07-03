using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private Button m_joinButton;
    [SerializeField] private Button m_hostButton;

    [SerializeField] private PlayerData m_playerData;

    private const string m_gameScene = "Game_Scene";

    private void Awake()
    {
        m_joinButton.onClick.AddListener(new UnityAction(OnJoinButtonClicked));
        m_hostButton.onClick.AddListener(new UnityAction(OnHostButtonClicked));
    }

    private void OnJoinButtonClicked()
    {
        m_playerData.playerGameMode = Fusion.GameMode.Client;
        LoadScene(m_gameScene);
    }

    private void OnHostButtonClicked()
    {
        m_playerData.playerGameMode = Fusion.GameMode.Host;
        LoadScene(m_gameScene);
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}