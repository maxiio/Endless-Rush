using UnityEngine;

namespace Core.Player.Movement {
	public class XAxisSpeedController : MonoBehaviour, ISpeedController {
		[SerializeField] private Vector3 maxSpeed;

		[SerializeField] [Range(.2f, 10)]
		private float lerpSpeed;

		public Vector3 GetVelocity(Vector3 currentVelocity) {
			return Vector3.Lerp(currentVelocity, maxSpeed, Time.deltaTime * lerpSpeed);
		}
	}
}