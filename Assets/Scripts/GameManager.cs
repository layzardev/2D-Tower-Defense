using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //[Header("Game State")]
    //public bool isGameActive = false;
    public bool isGameActive = true;

    void Awake()
    {        
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    //void Start()
    //{
    //    Debug.Log("GameManager initialized!");
    //    isGameActive = true;
    //}

    public void GameOver()
    {
        isGameActive = false;
        //Debug.Log("?? Game Over!");
        Debug.Log("GAME OVER! You lost all bases!");
    }

    public void GameWin()
    {
        isGameActive = false;
        Debug.Log("CONGRATULATIONS! You cleared all waves!");
        // Tambahkan UI menang atau logic lain di sini
    }
}
