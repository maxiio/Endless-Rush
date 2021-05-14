using UnityEngine;

namespace Input {
	public class InputHandlerAndroid : MonoBehaviour, IInputHandler {
		private Vector3 _direction;

		public float GetHorizontal() {
			SetDirection(ref _direction);
			_direction.Normalize();
			return _direction.z;
		}

		// Use the next values at vector to control the game throw mobile acceleration
		// Fixate at the z plane
		private void SetDirection(ref Vector3 direction) {
			direction.x = -UnityEngine.Input.acceleration.y;
			direction.y = 0;
			direction.z = UnityEngine.Input.acceleration.x;
		}
	}
}