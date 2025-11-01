using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Transform targetBase;

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
}
