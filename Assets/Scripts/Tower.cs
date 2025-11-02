using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float fireRate = 1.5f;
    private float fireTimer; 

    void Update()
    {
        fireTimer -= Time.deltaTime;

        // Cari musuh di depan (kanan)
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 4f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                if (fireTimer <= 0f)
                {
                    Shoot();
                    fireTimer = fireRate;
                }
                break;
            }
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null)
        {
            Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Debug.Log("Tower fired!");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 4f);
    }

    public void DestroyTower()
    {
        Destroy(gameObject);
    }
}
