using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimator : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int typeButton;
    [SerializeField] private Image imageButton;
    [SerializeField] private TMPro.TMP_Text textButton;
    private Animator animator;
    private const string nameVariableAnimator = "StateButton";
    private enum StateAnim { MouseEnter, MouseExit };
    private StateAnim State
    {
        get => (StateAnim)animator.GetInteger(nameVariableAnimator);
        set => animator.SetInteger(nameVariableAnimator, (int)value);
    }
    private ShopButtonsController buttonsController;

    public int GetTypeButton => typeButton;

    public void InitializedButton(DataInstaller.Tower towerInfo)
    {
        imageButton.sprite = towerInfo.backgroundImage;
        textButton.text = "Стоимость: " + towerInfo.cost;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonsController.SetSelectButton(typeButton);
        State = StateAnim.MouseEnter;
    }

    public void OnPointerExit(PointerEventData eventData) { }
    public void ResetAnimator() => State = StateAnim.MouseExit;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        buttonsController = GetComponentInParent<ShopButtonsController>();
    }
}