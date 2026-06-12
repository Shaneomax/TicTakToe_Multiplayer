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
    private PlayerType currentPlayablePlayerType;

    public override void OnNetworkSpawn() 
    {
        if (IsServer) 
        {
            localPlayerType = PlayerType.Cross;
            currentPlayablePlayerType = PlayerType.Cross;
        }
        else 
        {
            localPlayerType = PlayerType.Circle;
            currentPlayablePlayerType = PlayerType.Circle;
        }
    }

    [Rpc(SendTo.Server)]
    public void ClickedOnGridPositionRpc(int x, int y, PlayerType playerType) 
    {
        if (playerType != currentPlayablePlayerType) 
        {
            return;
        }

        OnClickedOnGridPosition?.Invoke(x, y, playerType);

        switch (currentPlayablePlayerType) 
        {
            case PlayerType.Cross:
                currentPlayablePlayerType = PlayerType.Circle;
                break;
            case PlayerType.Circle:
                currentPlayablePlayerType = PlayerType.Cross;
                break;
        }
    }

    public PlayerType GetLocalPlayerType() 
    {
        return localPlayerType;
    }
}