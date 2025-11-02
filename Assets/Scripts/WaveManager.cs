using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int waveNumber;
    public List<GameObject> enemyPrefabs; // Prefab musuh per wave
    public float spawnInterval = 1f;      // Delay antar spawn
}

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves;
    public Transform[] spawnPoints; // Spawn per lane
    public Transform[] baseTargets; // Target base per lane

    private int currentWave = 0;
    private bool waveInProgress = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        if (currentWave >= waves.Count)
        {
            Debug.Log("All waves completed!");
            yield break;
        }

        waveInProgress = true;
        Wave wave = waves[currentWave];

        foreach (GameObject enemyPrefab in wave.enemyPrefabs)
        {
            // Pilih spawn point acak
            int laneIndex = Random.Range(0, spawnPoints.Length);
            Transform spawn = spawnPoints[laneIndex];
            Transform targetBase = baseTargets[laneIndex];

            GameObject enemyObj = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
            Enemy enemyScript = enemyObj.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.SetTarget(targetBase);
            }

            yield return new WaitForSeconds(wave.spawnInterval);
        }

        currentWave++;
        waveInProgress = false;

        // Tunggu sampai semua musuh mati sebelum wave berikutnya
        StartCoroutine(WaitForNextWave());
    }

    IEnumerator WaitForNextWave()
    {
        while (GameObject.FindObjectsOfType<Enemy>().Length > 0)
            yield return null;

        // Semua wave selesai
        if (currentWave >= waves.Count)
        {
            // Cek tower tersisa
            Tower[] towersLeft = GameObject.FindObjectsOfType<Tower>();
            if (towersLeft.Length > 0)
            {
                GameManager.Instance.GameWin();
            }
            else
            {
                GameManager.Instance.GameOver();
            }
        }
        else
        {
            StartCoroutine(StartNextWave());
        }
    }


}
