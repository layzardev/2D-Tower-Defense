using UnityEngine;
using TMPro;

public class BaseController : MonoBehaviour
{
    public int health = 3;
    public TMP_Text hpText;

    private void Start()
    {
        UpdateHPText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Base hit by enemy!");
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0)
            health = 0;

        Debug.Log("Base HP: " + health);
        UpdateHPText();

        if (health <= 0)
        {
            Debug.Log("Base destroyed!");
            // GameManager.Instance.GameOver();
            WaveManager.Instance.BaseDestroyed(this);
        }
    }

    void UpdateHPText()
    {
        if (hpText != null)
            hpText.text = health.ToString();
    }
}
