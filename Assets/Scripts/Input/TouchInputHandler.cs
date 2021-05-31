using UnityEngine;

namespace Input {
	public class TouchInputHandler : MonoBehaviour, IInputHandler {
		[SerializeField] private float sensitivity = 1;

		private Vector2 _touchStartPosition;
		private Touch _touch;
		private Vector2 _direction;

		public Vector3 GetDirection() {
			_direction = Vector2.zero;
			if (UnityEngine.Input.touchCount > 0) {
				_touch = UnityEngine.Input.GetTouch(0);
				if (_touch.phase == TouchPhase.Moved) {
					_direction = _touch.position - _touchStartPosition;
				}

				_touchStartPosition = _touch.position;
			}

			print(_direction);
			return _direction * sensitivity;
		}

		public float GetVertical() {
			return GetDirection().y;
		}

		public float GetHorizontal() {
			return GetDirection().x;
		}
	}
}