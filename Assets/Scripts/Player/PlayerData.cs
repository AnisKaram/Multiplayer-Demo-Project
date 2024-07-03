using UnityEngine;
using Fusion;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects / PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public GameMode playerGameMode;
}
