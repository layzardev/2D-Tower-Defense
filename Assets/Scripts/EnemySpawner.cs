using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public LaneManager[] lanes;
    public float spawnInterval = 3f;

    private float timer;

    void Update()
    {
        if (!GameManager.Instance.isGameActive) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    void SpawnEnemy()
    {
        // Pilih lane random
        int laneIndex = Random.Range(0, lanes.Length);
        LaneManager lane = lanes[laneIndex];

        // Spawn di titik spawn
        GameObject enemy = Instantiate(enemyPrefab, lane.GetSpawnPoint().position, Quaternion.identity);
        enemy.GetComponent<Enemy>().SetTarget(lane.GetBasePoint());

        Debug.Log($"Spawned enemy in Lane {laneIndex + 1}");
    }
}
