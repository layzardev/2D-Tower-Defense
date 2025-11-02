using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject draggedTower;
    public GameObject towerPrefab;
    private TowerSlot currentSlot;
    private bool snappedToSlot = false;

    [Header("Placement Settings")]
    public float detectRadius = 0.7f; // area deteksi di sekitar tower

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedTower = Instantiate(towerPrefab);
        draggedTower.tag = "Tower";
        draggedTower.GetComponent<Tower>().enabled = false;
        draggedTower.GetComponent<Collider2D>().isTrigger = true;
        snappedToSlot = false;
        Debug.Log("Dragging tower started...");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedTower == null) return;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0;

        // Gerakkan tower mengikuti mouse
        draggedTower.transform.position = worldPos;

        // Cek semua collider di sekitar tower
        Collider2D[] hits = Physics2D.OverlapCircleAll(worldPos, detectRadius);
        TowerSlot foundSlot = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Slot"))
            {
                TowerSlot slot = hit.GetComponent<TowerSlot>();
                if (!slot.isOccupied)
                {
                    foundSlot = slot;
                    break;
                }
            }
        }

        if (foundSlot != null)
        {
            currentSlot = foundSlot;
            draggedTower.transform.position = foundSlot.transform.position; // snap langsung
            snappedToSlot = true;
        }
        else
        {
            currentSlot = null;
            snappedToSlot = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedTower == null) return;

        if (currentSlot != null && snappedToSlot && !currentSlot.isOccupied)
        {
            currentSlot.isOccupied = true;
            draggedTower.GetComponent<Tower>().enabled = true;
            Debug.Log("Tower placed successfully at " + currentSlot.name);
        }
        else
        {
            Debug.Log("Tower not placed (no valid slot).");
            Destroy(draggedTower);
        }

        draggedTower = null;
        currentSlot = null;
        snappedToSlot = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
