using Unity.Netcode;
using UnityEngine;

public class GameVisualManager : NetworkBehaviour
{
    private const float GRID_SIZE = 3.1f;
    [SerializeField] private Transform CrossPrefab;
    [SerializeField] private Transform CirclePrefab;

    public void GameManager_OnClickedOnGridPosition(int x, int y) 
    {
        SpawnObjectRpc(x, y);
    }

    [Rpc(SendTo.Server)]
    private void SpawnObjectRpc(int x, int y) 
    {
        Vector2 worldPosition = GetWrorldPosition(x, y);
        Transform SpawnedCrossTransform = Instantiate(CrossPrefab, worldPosition, Quaternion.identity);
        SpawnedCrossTransform.GetComponent<NetworkObject>().Spawn();
    }

    private Vector2 GetWrorldPosition(int x, int y) 
    {
        return new Vector2(-GRID_SIZE + x * GRID_SIZE, -GRID_SIZE + y * GRID_SIZE);
    }
}