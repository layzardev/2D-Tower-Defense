using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public string waveName = "Wave";
    public List<GameObject> enemyPrefabs;
    public float spawnInterval = 1f;
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public List<WaveData> waves;
    public Transform[] spawnPoints;
    public Transform[] baseTargets;

    private int currentWave = 0;
    private List<BaseController> aliveBases = new List<BaseController>();

    void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        aliveBases.AddRange(Object.FindObjectsByType<BaseController>(FindObjectsSortMode.None));
    }

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    public void BaseDestroyed(BaseController baseCtrl)
    {
        aliveBases.Remove(baseCtrl);

        if (aliveBases.Count == 0)
        {
            StopAllCoroutines();
            Debug.Log("All bases destroyed mid-wave! Game Over!");
            GameManager.Instance.GameOver();
        }
    }

    IEnumerator StartNextWave()
    {
        if (currentWave >= waves.Count)
        {
            Debug.Log("All waves completed!");
            yield break;
        }

        WaveData wave = waves[currentWave];
        Debug.Log($"Starting {wave.waveName}");

        foreach (GameObject enemyPrefab in wave.enemyPrefabs)
        {
            int laneIndex = Random.Range(0, spawnPoints.Length);
            Transform spawn = spawnPoints[laneIndex];
            Transform targetBase = baseTargets[laneIndex];

            GameObject enemyObj = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
            Enemy enemyScript = enemyObj.GetComponent<Enemy>();
            enemyScript?.SetTarget(targetBase);

            yield return new WaitForSeconds(wave.spawnInterval);
        }

        currentWave++;
        StartCoroutine(WaitForNextWave());
    }

    IEnumerator WaitForNextWave()
    {
        while (Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length > 0)
            yield return null;

        if (currentWave >= waves.Count)
        {
            if (aliveBases.Count > 0)
            {
                Debug.Log("Wave completed! Bases still standing. You Win!");
                GameManager.Instance.GameWin();
            }
            else
            {
                Debug.Log("All bases destroyed at last wave! Game Over!");
                GameManager.Instance.GameOver();
            }
        }
        else
        {
            StartCoroutine(StartNextWave());
        }
    }
}
