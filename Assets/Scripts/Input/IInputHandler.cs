using UnityEngine;

namespace Input {
	public interface IInputHandler {
		float GetHorizontal();
		Vector3 GetDirection();
	}
}