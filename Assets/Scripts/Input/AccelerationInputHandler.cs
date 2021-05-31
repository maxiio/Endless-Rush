using UnityEngine;

namespace Input {
	public class AccelerationInputHandler : MonoBehaviour, IInputHandler {
		private Vector3 _direction;

		public Vector3 GetDirection() {
			SetDirection(ref _direction);
			_direction.Normalize();
			return _direction;
		}
		
		public float GetHorizontal() {
			return GetDirection().z;
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