using UnityEditor;
using UnityEngine;

namespace Core.Player.Movement {
	public interface ISpeedController {
		Vector3 GetVelocity(Vector3 currentVelocity);
	}
}