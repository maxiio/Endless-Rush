using System;
using UnityEngine;

namespace Camera.Follow {
	public class CameraFollow : MonoBehaviour {
		[Serializable]
		private struct Axis {
			public bool x;
			public bool y;
			public bool z;
		}

		[Header("Object to follow")] [SerializeField]
		private Transform target;

		[SerializeField] private Axis freezeAxis;
		[SerializeField] private float lerpSpeed;

		[SerializeField] private bool isUsePositionOffset;
		[SerializeField] private Vector3 positionOffset;

		private Vector3 _lerpPosition;
		private Vector3 _defaultPosition;

		private void Awake() {
			if (!target) {
				throw new Exception("No target at camera");
			}

			// Set to offset the start default camera position
			if (!isUsePositionOffset) {
				positionOffset = gameObject.transform.position;
			}

			CalculateNewPosition();
			_defaultPosition = _lerpPosition;
		}

		private void CalculateNewPosition() {
			_lerpPosition = Vector3.Lerp(transform.position, target.position + positionOffset,
				Time.deltaTime * lerpSpeed);
			FreezeCameraMovementAxis(ref _lerpPosition);
		}

		private void FreezeCameraMovementAxis(ref Vector3 position) {
			if (freezeAxis.x) {
				position.x = _defaultPosition.x;
			}

			if (freezeAxis.y) {
				position.y = _defaultPosition.y;
			}

			if (freezeAxis.z) {
				position.z = _defaultPosition.z;
			}
		}

		private void FixedUpdate() {
			CalculateNewPosition();
		}

		// Set camera position after all other object updates
		private void LateUpdate() {
			transform.position = _lerpPosition;
		}
	}
}