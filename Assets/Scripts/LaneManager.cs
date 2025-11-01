using UnityEngine;

public class LaneManager : MonoBehaviour
{
    [Header("Lane Objects")]
    public Transform basePoint;
    public Transform[] slots;
    public Transform spawnPoint;

    public int laneIndex;

    void Start()
    {
        Debug.Log($"Lane {laneIndex} ready! Base: {basePoint.name}, Spawn: {spawnPoint.name}");
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    public Transform GetBasePoint()
    {
        return basePoint;
    }
}
