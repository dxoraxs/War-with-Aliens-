using UnityEngine;
using UnityEngine.EventSystems;

public class ShopButtonsController : MonoBehaviour
#if UNITY_EDITOR
    , IPointerUpHandler, IPointerDownHandler
#endif
{
    [SerializeField] private TowerCellManager towerCellManager;
    [SerializeField] private DataInstaller dataInstaller;
    private Vector3 lastCellPosition;
    private int selectButton;
    private CanvasGroup canvasGroup;

    public void HideButtonsShop() => transform.gameObject.SetActive(false);

    public void SetSelectButton(int index)
    {
        if (index != 0)
        {
            canvasGroup.alpha = 0.45f;
        }
        else
        {
            canvasGroup.alpha = 1f;
        }

        ResetAnimation();
        selectButton = index;
        towerCellManager.EnableRadiusTower(index, lastCellPosition);
    }

    public void ButtonMouseUp()
    {
        if (selectButton != 0)
        {
            towerCellManager.BuildingTower(this, lastCellPosition, selectButton);
        }
        ResetAnimation();
        HideButtonsShop();
    }

    public void SetButtonsPosition(Vector3 position)
    {
        lastCellPosition = position;
        transform.gameObject.SetActive(true);
        transform.position = Camera.main.WorldToScreenPoint(lastCellPosition);
    }

#if UNITY_EDITOR
    public void OnPointerUp(PointerEventData eventData) => ButtonMouseUp();
    public void OnPointerDown(PointerEventData eventData) { }
#endif

#if !UNITY_EDITOR
    private void Update()
    {
        if (Input.touchCount == 0)
        {
            ButtonMouseUp();
        }
    }
#endif

    private void ResetAnimation()
    {
        foreach (Transform button in transform)
        {
            var buttonAnimator = button.GetComponent<ButtonAnimator>();
            buttonAnimator.ResetAnimator();
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        foreach (Transform button in transform)
        {
            var buttonAnimator = button.GetComponent<ButtonAnimator>();
            if (buttonAnimator.GetTypeButton > 0)
            {
                buttonAnimator.InitializedButton(dataInstaller.GetTowerIndex(buttonAnimator.GetTypeButton - 1));
            }
        }
    }
}