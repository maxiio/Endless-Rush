using System;
using Core.Components.Health;
using UI.ScreenTransition;
using UI.ScreenTransition.Button;
using UnityEngine;

namespace Core.Managers.GameOver {
	public class GameOverManager : MonoBehaviour {
		public static event EventHandler<ButtonAction.Actions> IsGameOver;

		[Header("GameObjects which send the dead notification")] 
		[SerializeField] private GameObject player;

		[Header("Screen manager")] 
		[SerializeField] private ScreenTransitionManager screenManager;

		private void Awake() {
			var player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<HealthComponent>().IsDieEvent += GameOverSender;
		}

		private void GameOverSender(object sender, EventArgs e) {
			screenManager.CallAction(ButtonAction.Actions.GAMEOVER);
			IsGameOver?.Invoke(this, ButtonAction.Actions.GAMEOVER);
		}
	}
}