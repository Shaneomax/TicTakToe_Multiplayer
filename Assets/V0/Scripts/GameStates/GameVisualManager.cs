using Unity.Netcode;
using UnityEngine;

public class GameVisualManager : NetworkBehaviour
{
    private const float GRID_SIZE = 3.1f;
    [SerializeField] private Transform CrossPrefab;
    [SerializeField] private Transform CirclePrefab;

    public void GameManager_OnClickedOnGridPosition(int x, int y, GameManager.PlayerType playerType) 
    {
        SpawnObjectRpc(x, y, playerType);
    }

    [Rpc(SendTo.Server)]
    private void SpawnObjectRpc(int x, int y, GameManager.PlayerType playerType) 
    {   
        Transform prefab;
        if (playerType == GameManager.PlayerType.Cross)
        {
            prefab = CrossPrefab;
        }
        else
        {
            prefab = CirclePrefab;
        }
        Vector2 worldPosition = GetWorldPosition(x, y);
        Transform spawnedTransform = Instantiate(prefab, worldPosition, Quaternion.identity);
        spawnedTransform.GetComponent<NetworkObject>().Spawn();
    }

    private Vector2 GetWorldPosition(int x, int y) 
    {
        return new Vector2(-GRID_SIZE + x * GRID_SIZE, -GRID_SIZE + y * GRID_SIZE);
    }
}