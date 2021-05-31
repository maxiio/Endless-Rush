using System;
using UnityEngine;

namespace Input {
	public class TouchAndTargetInputHandler : MonoBehaviour, IInputHandler {
		[Header("Target to calculate direction")]
		[SerializeField] private Transform target;

		[Tooltip("Direction is normilized and independent from distance between target and touch position")]
		[SerializeField] private bool useNormalizedDirection;

		private UnityEngine.Camera _camera;
		private Vector3 _lastDirection;
		private Vector3 _positionToMove;

		private void Awake() {
			if (!target) {
				throw new Exception("Target isn't set");
			}

			_camera = UnityEngine.Camera.main;
		}

		public Vector3 GetDirection() {
			var directionToMove = Vector3.zero;

			if (UnityEngine.Input.touchCount > 0) {
				var touch = UnityEngine.Input.GetTouch(0);
				if (touch.phase == TouchPhase.Stationary) {
					directionToMove = _lastDirection;
				}
				else if (touch.phase == TouchPhase.Moved) {
					Ray ray = _camera.ScreenPointToRay(touch.position);
					if (Physics.Raycast(ray, out var hitInfo, 100)) {
						_positionToMove = hitInfo.point;

						directionToMove = _positionToMove - target.position;
						if (useNormalizedDirection) {
							directionToMove = directionToMove.normalized;
						}

						_lastDirection = directionToMove;
					}
				}
			}

			return directionToMove;
		}
	}
}