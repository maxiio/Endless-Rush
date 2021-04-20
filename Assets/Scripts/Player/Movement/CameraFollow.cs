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

    void FixedUpdate() {
        _lerpedPosition = Vector3.Lerp(transform.position, _target.position + _positionOffset, Time.deltaTime * _lerpSpeed);
    }

    void LateUpdate() { 
        transform.position = _lerpedPosition;
    }
}
