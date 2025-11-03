using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameActive = true;

    [Header("UI Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        if (!isGameActive) return;

        isGameActive = false;
        Debug.Log("GAME OVER! You lost all towers!");
        if (losePanel != null)
            losePanel.SetActive(true);
    }

    public void GameWin()
    {
        if (!isGameActive) return;

        isGameActive = false;
        Debug.Log("YOU WIN! All waves cleared!");
        if (winPanel != null)
            winPanel.SetActive(true);
    }
    
    public void RestartGame()
    {        
        if (winPanel != null)
            winPanel.SetActive(false);
        if (losePanel != null)
            losePanel.SetActive(false);

        Debug.Log("Restarting game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
