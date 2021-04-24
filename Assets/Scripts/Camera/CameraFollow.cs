using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Object to follow")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpSpeed;

    [SerializeField] private bool _IsUsePositionOffset;
    [SerializeField] private Vector3 _positionOffset;


    private Vector3 _lerpedPosition;

    private void Awake() {
        if (!_target) {            
            throw new Exception("No target at camera");
        }

        // Set to offset the start default camera position
        if (!_IsUsePositionOffset) {
            _positionOffset = gameObject.transform.position;
        }

        CalculateNewPosition();
    }

    void CalculateNewPosition() {
        _lerpedPosition = Vector3.Lerp(transform.position, _target.position + _positionOffset, Time.deltaTime * _lerpSpeed);
    }

    void FixedUpdate() {
        CalculateNewPosition();
    }

    // Set camera position after all other object updates
    void LateUpdate() { 
       transform.position = _lerpedPosition;
    }
}
