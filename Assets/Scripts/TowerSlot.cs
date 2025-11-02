using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    public bool isOccupied = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = isOccupied ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.9f);
    }
}
