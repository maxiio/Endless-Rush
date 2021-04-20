using System;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    [Header("Object to follow")]
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private float _lerpSpeed;

    private Vector3 _lerpedPosition;

    private void Awake() {
        if (!_target) {            
            throw new Exception("No target at camera");
        }
    }

    // Calculate new position
    void FixedUpdate() {
        _lerpedPosition = Vector3.Lerp(transform.position, _target.position + _positionOffset, Time.deltaTime * _lerpSpeed);
    }

    // Set camera position after all other object updates
    void LateUpdate() { 
        transform.position = _lerpedPosition;
    }
}
