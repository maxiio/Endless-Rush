using System;
using Core.Components.Health;
using UI.ScreenTransition;
using UI.ScreenTransition.Button;
using UnityEngine;

namespace Core.Managers.GameOver {
	public class GameOverManager : MonoBehaviour {
		[Header("GameObjects which send the dead notification")] [SerializeField]
		private GameObject[] gameObjects;

		[Header("Screen manager")] [SerializeField]
		private ScreenTransitionManager screenManager;

		private void Awake() {
			foreach (var currentObject in gameObjects) {
				currentObject.GetComponent<HealthComponent>().IsDieEvent += GameOverSender;
			}
		}

		// private void OnDestroy() {
		// 	throw new NotImplementedException("OnDestroy doesn't implement");
		// }

		private void GameOverSender(object sender, EventArgs e) {
			screenManager.CallAction(ButtonAction.Actions.GAMEOVER);
		}
	}
}