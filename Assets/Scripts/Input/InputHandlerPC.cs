using UnityEngine;

namespace Input {
	public class InputHandlerPC : MonoBehaviour, IInputHandler {
		private Vector3 _direction;
		
		public float GetHorizontal() {
			return UnityEngine.Input.GetAxis("Horizontal");
		}

		private float GetVertical() {
			return UnityEngine.Input.GetAxis("Vertical");
		}

		public Vector3 GetDirection() {
			_direction.x = GetVertical();
			_direction.z = GetHorizontal();
			return _direction;
		}
	}
}