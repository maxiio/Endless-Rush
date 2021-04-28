using System;
using UnityEngine;
using UnityEditor;
using BaseClasses;

public class Move : PhysicsObject {
    [Header("Movement")]
    [SerializeField] private float _speedHorizontal;
    [SerializeField] private float _speedVertical;
    [SerializeField] [Range(.2f, 10)] private float _lerpSpeed;

    private float _moveHorizontal;
    private Vector3 _movementForce;
    private Vector3 _movementSpeed;

    private void Update() {
        InputHandler();        
    }   

    private void FixedUpdate() {
        UpdateMovementForce();
        UpdateMovementSpeed();
    }

    // Update the acceleration on Z axis (left-right moving)
    private void UpdateMovementForce() {
        _movementForce.z = _speedHorizontal * _moveHorizontal;
        rigidbody.AddForce(_movementForce);
    }

    // Update the speed of body while he is stopped
    private void UpdateMovementSpeed() {
        _movementSpeed.x = _speedVertical;
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, _movementSpeed, Time.deltaTime * _lerpSpeed);
    }

    // Take the user commands
    private void InputHandler() {
        _moveHorizontal = Input.GetAxis("Horizontal");

        if (_moveHorizontal == 0f) {
#if UNITY_ANDROID
            Vector3 dir = new Vector3(-Input.acceleration.y, 0, Input.acceleration.x);
            dir.Normalize();
            _moveHorizontal = dir.z;
#endif
        }
    }
}
