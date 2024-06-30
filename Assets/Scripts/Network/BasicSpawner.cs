using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner m_runner;

    [SerializeField] private NetworkPrefabRef m_playerPrefab;
    private Dictionary<PlayerRef, NetworkObject> m_spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    public void OnConnectedToServer(NetworkRunner runner)
    {}

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {}

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {}

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {}

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {}

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {}

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData data = new NetworkInputData();

        if (Input.GetKey(KeyCode.W))
            data.direction += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            data.direction += Vector3.back;

        if (Input.GetKey(KeyCode.A))
            data.direction += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            data.direction += Vector3.right;

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {}

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            // create a unique position for the player
            Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);

            // spawn the prefab
            NetworkObject networkPlayerObject = runner.Spawn(m_playerPrefab, spawnPosition, Quaternion.identity, player);

            // Keep track of the player avatars so we can remove it when they disconnect
            m_spawnedCharacters.Add(player, networkPlayerObject);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // find and remove the player
        if (m_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            m_spawnedCharacters.Remove(player);
        }
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {}

    public void OnSceneLoadDone(NetworkRunner runner)
    {}

    public void OnSceneLoadStart(NetworkRunner runner)
    {}

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {}

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {}

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {}


    async void StartGame(GameMode gameMode)
    {
        // create the fusion runner and let it know that we will be providing user input
        m_runner = gameObject.AddComponent<NetworkRunner>();
        m_runner.ProvideInput = true;

        // start or join (depends on gamemode) a session with a specific name
        await m_runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            SessionName = "Test room",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    private void OnGUI()
    {
        if (m_runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }
}