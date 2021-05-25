using System;
using Core.Components.Health;
using Core.Player.Respawn;
using UI.ScreenTransition.Button;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Managers.Actions {
	public class ActionManager : MonoBehaviour {
		[Header("Object to revive")]
		[SerializeField] private GameObject[] gameObjects;

		[Header("Revive option")]
		[SerializeField] private Vector3 offsetPosition = new Vector3(5, 0, 0);

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
					Revive();
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
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		private void Revive() {
			foreach (var currentObject in gameObjects) {
				currentObject.SetActive(true);
				currentObject.GetComponent<Transform>().position += offsetPosition;
				currentObject.GetComponent<HealthComponent>()?.Revive(currentObject);
			}
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