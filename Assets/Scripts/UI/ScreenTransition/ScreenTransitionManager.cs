using System;
using UI.ScreenTransition.Button;
using UnityEngine;

namespace UI.ScreenTransition {
	[Serializable]
	public struct ActionButtons {
		public ButtonAction.Actions action;
		public GameObject screen;
	}

	public class ScreenTransitionManager : MonoBehaviour {
		// Default screen
		[SerializeField] private ButtonAction.Actions startAction;

		// This is contain the screen which will be load when button pressed
		[SerializeField] private ActionButtons[] actionButtons;

		private GameObject _currentScreen;

		private void Awake() {
			ChangeScreen(startAction);

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

			var isShow = TryShowGameObject(_currentScreen);
			if (!isShow) {
				TryShowGameObject(GetScreenByAction(startAction));
			}
		}

		private GameObject GetScreenByAction(ButtonAction.Actions currentAction) {
			foreach (var action in actionButtons) {
				if (currentAction == action.action) {
					return action.screen;
				}
			}

			return null;
		}

		private bool TryHideGameObject(GameObject currentObject) {
			if (currentObject) {
				HideGameObject(currentObject);
			}

			return currentObject != null;
		}

		private bool TryShowGameObject(GameObject currentObject) {
			if (currentObject) {
				ShowGameObject(currentObject);
			}

			return currentObject != null;
		}

		private void HideGameObject(GameObject currentObject) {
			currentObject.SetActive(false);
		}

		private void ShowGameObject(GameObject currentObject) {
			currentObject.SetActive(true);
		}
	}
}