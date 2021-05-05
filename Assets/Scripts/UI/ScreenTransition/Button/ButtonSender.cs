using System;
using UnityEngine;

public class ButtonSender : MonoBehaviour
{
    // The button click is notify the subscribers
    public static event EventHandler<ButtonAction.Actions> ButtonClickedAction;
    // This action is send to the subscribers
	[SerializeField] private ButtonAction.Actions _action;

    private void Awake() {
        if (_action == ButtonAction.Actions.NULL) {
            throw new Exception("Set the action at ButtonSender");
        }
    }

    // This method is called from buttonOnClick
    public void Clicked() {
        ButtonClickedAction?.Invoke(this, _action);
	}

    private void Update() {
        if (Input.GetButtonDown("Submit") && _action == ButtonAction.Actions.RESTART) {
            Clicked();
        }
        if (Input.GetButtonDown("Cancel") && _action == ButtonAction.Actions.MENU) {
            Clicked();
        }
        if (Input.GetButtonDown("Cancel") && _action == ButtonAction.Actions.MENU) {
            Clicked();
        }
    }
}
