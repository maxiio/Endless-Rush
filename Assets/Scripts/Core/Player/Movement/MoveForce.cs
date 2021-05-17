using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Player.Movement {
	public class MoveForce : MonoBehaviour, IMoveForce {
		[Header("Movement")]
		[SerializeField] private float speed;

		[SerializeField] private bool enabledX;
		[SerializeField] private bool enabledY;
		[SerializeField] private bool enabledZ;

		private float _moveHorizontal;
		private Vector3 _movementForce = new Vector3();
		private Vector3 _direction = new Vector3();

		public void SetDirection(Vector3 direction) {
			_direction = direction;
		}

		public Vector3 GetForce() {
			_movementForce = _direction * speed;
			DisableAxisForce(ref _movementForce.x, enabledX);
			DisableAxisForce(ref _movementForce.y, enabledY);
			DisableAxisForce(ref _movementForce.z, enabledZ);
			return _movementForce;
		}

		private void DisableAxisForce(ref float movementForceAxis, bool enabledAxis) {
			if (!enabledAxis) {
				movementForceAxis = 0;
			}
		}
	}
}