using System;
using Core.Components.Health;
using Core.Player.Respawn;
using UI.ScreenTransition.Button;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Managers.Actions {
	public class ActionManager : MonoBehaviour {
		[SerializeField] private GameObject[] gameObjects;

		[Header("Restart option")] [SerializeField]
		private bool isReloadScene;

		private float _defaultTimeScale = 1;

		public enum Request {
			NULL,
			RESTART,
			REVIVE,
			PAUSE,
			UNPAUSE,
			EXIT
		}

		private void Awake() {
			_defaultTimeScale = Time.timeScale;
			ButtonAction.RequestedAction += RequestHandler;
		}

		private void OnDestroy() {
			ButtonAction.RequestedAction -= RequestHandler;
		}

		private void RequestHandler(object sender, Request request) {
			switch (request) {
				case Request.NULL:
					throw new Exception("Call the NULL switch");
				case Request.RESTART:
					Restart();
					break;
				case Request.REVIVE:
					RevivePlayer();
					break;
				case Request.PAUSE:
					Pause();
					break;
				case Request.UNPAUSE:
					UnPause();
					break;
				case Request.EXIT:
					Exit();
					break;
				default:
					throw new Exception("Call the default switch");
			}
		}

		private void Restart() {
			if (isReloadScene) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
			else {
				foreach (var currentObject in gameObjects) {
					currentObject.GetComponent<RespawnComponent>()?.Respawn();
					currentObject.GetComponent<HealthComponent>()?.SetDefaultHealth(currentObject);
				}
			}
		}

		private void RevivePlayer() {
			Restart();
		}

		private void Pause() {
			Time.timeScale = 0;
		}

		private void UnPause() {
			Time.timeScale = _defaultTimeScale;
		}

		private static void Exit() {
			Application.Quit(0);
		}
	}
}