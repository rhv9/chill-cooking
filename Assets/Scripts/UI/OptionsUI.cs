using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    private Action OnCloseButtonAction;

    [SerializeField] private Transform rebindUI;

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button mainMusicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI mainMusicText;

    // Key bindings
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;

    // Gamepad bindings
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private Button gamepadPauseButton;

    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        mainMusicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });

        moveUpButton.onClick.AddListener(() => RebindBinding(GameInput.Bindings.MoveUp));
        moveDownButton.onClick.AddListener(() => RebindBinding(GameInput.Bindings.MoveDown));
        moveLeftButton.onClick.AddListener(() => RebindBinding(GameInput.Bindings.MoveLeft));
        moveRightButton.onClick.AddListener(() => RebindBinding(GameInput.Bindings.MoveRight));
        interactButton.onClick.AddListener(() => RebindBinding(GameInput.Bindings.Interact));
        interactAlternateButton.onClick.AddListener(() => RebindBinding(GameInput.Bindings.InteractAlternate));
        pauseButton.onClick.AddListener(() => RebindBinding(GameInput.Bindings.Pause));
        gamepadInteractButton.onClick.AddListener(() => RebindBinding(GameInput.Bindings.Gamepad_Interact));
        gamepadInteractAlternateButton.onClick.AddListener(() => RebindBinding(GameInput.Bindings.Gamepad_InteractAlternate));
        gamepadPauseButton.onClick.AddListener(() => RebindBinding(GameInput.Bindings.Gamepad_Pause));
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;

        UpdateVisual();
        Hide();
    }

    private void KitchenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        mainMusicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();

        moveUpButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveUp);
        moveDownButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveDown);
        moveLeftButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveLeft);
        moveRightButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveRight);
        interactButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        interactAlternateButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlternate);
        pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);
        gamepadInteractButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_Interact);
        gamepadInteractAlternateButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_InteractAlternate);
        gamepadPauseButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Bindings.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.OnCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);
        soundEffectsButton.Select();
    }
    public void Hide()
    {
        OnCloseButtonAction?.Invoke();
        gameObject.SetActive(false);
    }

    private void ShowRebindUI()
    {
        rebindUI.gameObject.SetActive(true);
    }
    private void HideRebindUI()
    {
        rebindUI.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Bindings binding)
    {
        ShowRebindUI();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HideRebindUI();
            UpdateVisual();
        });
    }
}

