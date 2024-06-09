using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Transform KeyMoveUp;
    [SerializeField] private Transform KeyMoveDown;
    [SerializeField] private Transform KeyMoveLeft;
    [SerializeField] private Transform KeyMoveRight;
    [SerializeField] private Transform KeyInteract;
    [SerializeField] private Transform KeyInteractAlternate;
    [SerializeField] private Transform KeyPause;

    [SerializeField] private Transform KeyGamepadInteract;
    [SerializeField] private Transform KeyGamepadInteractAlternate;
    [SerializeField] private Transform KeyGamepadPause;

    private void Awake()
    {

    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsWaitingToStart())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        UpdateVisual();

        Show();
    }

    private void UpdateVisual()
    {
        KeyMoveUp.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveUp);
        KeyMoveDown.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveDown);
        KeyMoveLeft.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveLeft);
        KeyMoveRight.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveRight);
        KeyInteract.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        KeyInteractAlternate.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlternate);
        KeyPause.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);

        KeyGamepadInteract.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_Interact);
        KeyGamepadInteractAlternate.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_InteractAlternate);
        KeyGamepadPause.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
