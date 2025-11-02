using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int maxHP = 3;
    private int currentHP;

    private Transform targetBase;

    void Start()
    {
        currentHP = maxHP;
    }

    public void SetTarget(Transform baseTarget)
    {
        targetBase = baseTarget;
    }

    void Update()
    {
        if (targetBase == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetBase.position,
            speed * Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Base"))
        {
            Debug.Log("Enemy reached the base!");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log($"Enemy took {amount} damage. Current HP: {currentHP}");

        speed = Mathf.Max(speed * 0.8f, 0.5f);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}
