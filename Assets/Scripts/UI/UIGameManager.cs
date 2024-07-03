using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] private Button m_backButton;

    private void Awake()
    {
        m_backButton.onClick.AddListener(new UnityAction(OnBackButtonClicked));
    }

    private void OnBackButtonClicked()
    {
        GameManager.instance.basicSpawner.DisconnectPlayer();
        SceneManager.LoadScene("Menu_Scene");
    }
}