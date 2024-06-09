using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private string PLAYER_PREFS_GAMEINPUT_BINDINGS_JSON = "GameInputBindings";
    public static GameInput Instance {get; private set;}

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;

    public enum Bindings
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause,
        Gamepad_Interact,
        Gamepad_InteractAlternate,
        Gamepad_Pause,
    }

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_GAMEINPUT_BINDINGS_JSON))
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_GAMEINPUT_BINDINGS_JSON));

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Bindings binding)
    {
        switch (binding)
        {
            case Bindings.MoveUp:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Bindings.MoveDown:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Bindings.MoveLeft:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Bindings.MoveRight:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Bindings.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Bindings.InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Bindings.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            case Bindings.Gamepad_Interact:
                return playerInputActions.Player.Interact.bindings[1].ToDisplayString();
            case Bindings.Gamepad_InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
            case Bindings.Gamepad_Pause:
                return playerInputActions.Player.Pause.bindings[1].ToDisplayString();

        }

        Debug.Log("No binding found for binding: " + binding.ToString());
        return "No Binding";
    }

    public void RebindBinding(Bindings binding, Action onBindingChanged)
    {
        InputAction inputAction = null;
        int bindingIndex = -1;

        switch (binding){
            case Bindings.MoveUp:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Bindings.MoveDown:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Bindings.MoveLeft:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Bindings.MoveRight:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Bindings.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Bindings.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Bindings.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
            case Bindings.Gamepad_Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;
            case Bindings.Gamepad_InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 1;
                break;
            case Bindings.Gamepad_Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 1;
                break;
        };

        playerInputActions.Player.Disable();
        
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            callback.Dispose();
            playerInputActions.Player.Enable();
            PlayerPrefs.SetString(PLAYER_PREFS_GAMEINPUT_BINDINGS_JSON, playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

            onBindingChanged();
        }).Start();
    }
}
