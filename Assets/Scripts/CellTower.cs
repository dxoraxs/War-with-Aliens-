using UnityEngine;

public class CellTower : MonoBehaviour
{
    private TowerCellManager cellManager;

    public void SetCellBusy() => transform.gameObject.SetActive(false);
    public void SetCellUnbusy() => transform.gameObject.SetActive(true);

    private void OnMouseDown()
    {
        cellManager.SetPositionBuyCursor(transform.position);
    }

    private void Awake()
    {
        cellManager = transform.GetComponentInParent<TowerCellManager>();
    }
}
