using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Serializable]
    struct Axis {
        public bool x;
        public bool y;
        public bool z;
    }

    [Header("Object to follow")]
    [SerializeField] private Transform _target;
    [SerializeField] private Axis _freezeAxis;
    [SerializeField] private float _lerpSpeed;

    [SerializeField] private bool _IsUsePositionOffset;
    [SerializeField] private Vector3 _positionOffset;

    private Vector3 _lerpedPosition;
    private Vector3 _defaultPosition;

    private void Awake() {
        if (!_target) {            
            throw new Exception("No target at camera");
        }

        // Set to offset the start default camera position
        if (!_IsUsePositionOffset) {
            _positionOffset = gameObject.transform.position;
        }

        CalculateNewPosition();
        _defaultPosition = _lerpedPosition;
    }

    private void CalculateNewPosition() {
        _lerpedPosition = Vector3.Lerp(transform.position, _target.position + _positionOffset, Time.deltaTime * _lerpSpeed);
        FreezeCameraMovementAxis(ref _lerpedPosition);
    }

    private void FreezeCameraMovementAxis(ref Vector3 position) {
        if (_freezeAxis.x) {
            position.x = _defaultPosition.x;
        }
        if (_freezeAxis.y) {
            position.y = _defaultPosition.y;
        }
        if (_freezeAxis.z) {
            position.z = _defaultPosition.z;
        }
    }

    void FixedUpdate() {
        CalculateNewPosition();
    }

    // Set camera position after all other object updates
    void LateUpdate() { 
       transform.position = _lerpedPosition;
    }
}
