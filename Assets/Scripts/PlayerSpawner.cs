using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            NetworkObject spawnedPlayerObject = Runner.Spawn(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity, player);
            GameObject spawnedPlayer = spawnedPlayerObject.gameObject;
            

                
        }
    }
}
