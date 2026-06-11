using UnityEngine;
using UnityEngine.Events; 

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance { get; private set; }

    public UnityEvent<int, int> OnClickedOnGridPosition;

    private void Awake() 
    {
        if (Instance != null) 
        {
            return;
        }
        Instance = this;
    }

    public void ClickedOnGridPosition(int x, int y) 
    {
        OnClickedOnGridPosition?.Invoke(x, y);
    }
}