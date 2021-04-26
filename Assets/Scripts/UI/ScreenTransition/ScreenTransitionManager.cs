using System;
using UnityEngine;

[Serializable]
public struct ActionButtons {
    public ButtonAction.Actions action;
    public GameObject screen;
}

public class ScreenTransitionManager : MonoBehaviour
{
    // Default screen
    [SerializeField] private ButtonAction.Actions _startAction;
    // This is contain the screen which will be load when button pressed
    [SerializeField] private ActionButtons[] _actionButtons;

    private GameObject _currentScreen;

    private void Awake() {
        ChangeScreen(_startAction);

        ButtonSender.ButtonClickedAction += SetScreen;
    }

    private void OnDestroy() {
        ButtonSender.ButtonClickedAction -= SetScreen;
    }

    private void SetScreen(object sender, ButtonAction.Actions action) {
        ChangeScreen(action);
    }

    public void CallAction(ButtonAction.Actions action) {
        ChangeScreen(action);
    }

    private void ChangeScreen(ButtonAction.Actions action) {
        ButtonAction.HandleAction(action);

        TryHideGameObject(_currentScreen);

        _currentScreen = GetScreenByAction(action);

        bool isShow = TryShowGameObject(_currentScreen);
        if (!isShow) {
            TryShowGameObject(GetScreenByAction(_startAction));
        }
    }

    private GameObject GetScreenByAction(ButtonAction.Actions currentAction) {
        foreach (var action in _actionButtons) {
            if (currentAction == action.action) {
                return action.screen;
            }
        }
        return null;
    }

    private bool TryHideGameObject(GameObject gameObject) {
        if (gameObject) {
            HideGameObject(gameObject);
        }
        return gameObject != null;
    }

    private bool TryShowGameObject(GameObject gameObject) {
        if (gameObject) {
            ShowGameObject(gameObject);
        }
        return gameObject != null;
    }

    private void HideGameObject(GameObject gameObject) {
        gameObject.SetActive(false);
    }

    private void ShowGameObject(GameObject gameObject) {
        gameObject.SetActive(true);
    }
}

