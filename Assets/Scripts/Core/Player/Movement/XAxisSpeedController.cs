using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Player.Movement {
	public class XAxisSpeedController : MonoBehaviour, ISpeedController {
		[SerializeField] private float maxXAxisSpeed;

		[SerializeField] [Range(.2f, 10)]
		private float lerpSpeed;

		private Vector3 _movementSpeed;

		private void Awake() {
			SetMovementSpeed();
		}

		private void SetMovementSpeed() {
			_movementSpeed.x = maxXAxisSpeed;
		}

		public Vector3 GetVelocity(Vector3 currentVelocity) {
			return Vector3.Lerp(currentVelocity, _movementSpeed, Time.deltaTime * lerpSpeed);
		}
	}
}