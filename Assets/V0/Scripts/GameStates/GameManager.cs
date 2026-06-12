using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events; 

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    public UnityEvent<int, int, PlayerType> OnClickedOnGridPosition;

    private void Awake() 
    {
        if (Instance != null) 
        {
            return;
        }
        Instance = this;
    }

    public enum PlayerType
    {
        None,
        Cross,
        Circle
    }

    private PlayerType localPlayerType;

    public override void OnNetworkSpawn() 
    {
        if (IsServer) 
        {
            localPlayerType = PlayerType.Cross;
        }
        else 
        {
            localPlayerType = PlayerType.Circle;
        }
    }

    public void ClickedOnGridPosition(int x, int y) 
    {
        OnClickedOnGridPosition?.Invoke(x, y, localPlayerType);
    }

    public PlayerType GetLocalPlayerType() 
    {
        return localPlayerType;
    }
}