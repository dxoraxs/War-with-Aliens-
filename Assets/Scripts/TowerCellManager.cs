using UnityEngine;

public class TowerCellManager : MonoBehaviour
{
    [SerializeField] private ShopButtonsController buttonsShop;
    [SerializeField] private ShopButtonsController buttonsDestroy;
    [SerializeField] private DataInstaller dataInstaller;
    [SerializeField] private TowerController towerPrefab;
    [SerializeField] private GameObject radiusTower;
    [SerializeField] private Transform castleTransform;
    [SerializeField] private GameController gameController;

    public void SetPositionBuyCursor(Vector3 position) => buttonsShop.SetButtonsPosition(position);
    public void SetPositionDestroyCursor(Vector3 position) => buttonsDestroy.SetButtonsPosition(position);

    public void BuildingTower(ShopButtonsController buttons, Vector3 position, int typeTower)
    {
            radiusTower.SetActive(false);
        if (typeTower > 0)
        {

            if (gameController.TowerIsBuying(dataInstaller.GetTowerIndex(typeTower - 1).cost))
            {
                var tower = Instantiate(towerPrefab, transform);
                tower.transform.position = position;
                tower.InitializedTower(dataInstaller.GetTowerIndex(typeTower - 1), castleTransform.position);

                foreach (CellTower cell in transform.GetComponentsInChildren<CellTower>())
                {
                    if (cell.transform.position == tower.transform.position)
                    {
                        cell.SetCellBusy();
                        break;
                    }
                }
            }
        }
        else
        {
            foreach (TowerController cell in transform.GetComponentsInChildren<TowerController>())
            {
                if (cell.transform.position == position)
                {
                    DestroyTower(cell.gameObject);
                    break;
                }
            }
        }
    }

    private void DestroyTower(GameObject tower)
    {
        Debug.Log("count = " + transform.GetComponentsInChildren<CellTower>().Length);
        foreach (Transform cell in transform)
        {
            Debug.Log(cell.transform.position +" != " + tower.transform.position);
            if (cell.transform.position == tower.transform.position && cell.GetComponent<CellTower>())
            {
                cell.GetComponent<CellTower>().SetCellUnbusy();
                break;
            }
        }
        Destroy(tower);
    }
	
	public void EnableRadiusTower(int typeTower, Vector3 position)
	{
        if (typeTower <= 0)
        {
            radiusTower.SetActive(false);
            return;
        }
        radiusTower.SetActive(true);
        radiusTower.transform.position = position;
        radiusTower.transform.localScale = Vector3.one * 12 * dataInstaller.GetTowerIndex(typeTower-1).radius;
	}
}
