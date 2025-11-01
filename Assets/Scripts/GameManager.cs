using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public bool isGameActive = false;

    void Awake()
    {        
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Debug.Log("GameManager initialized!");
        isGameActive = true;
    }

    public void GameOver()
    {
        isGameActive = false;
        Debug.Log("?? Game Over!");
    }
}
