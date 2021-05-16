using UnityEngine;

namespace Core.Player.Movement {
	public interface IMoveForce {
		void SetDirection(Vector3 direction);
		Vector3 GetForce();
	}
}