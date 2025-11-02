using UnityEngine;

public class BaseController : MonoBehaviour
{
    public int health = 3;

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
        Debug.Log("Base HP: " + health);

        if (health <= 0)
        {
            Debug.Log("Base destroyed!");
            GameManager.Instance.GameOver();
        }
    }
}
