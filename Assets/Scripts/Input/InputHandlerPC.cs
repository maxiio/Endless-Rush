using UnityEngine;

namespace Input {
	public class InputHandlerPC : MonoBehaviour, IInputHandler {
		public float GetHorizontal() {
			return UnityEngine.Input.GetAxis("Horizontal");
		}
	}
}