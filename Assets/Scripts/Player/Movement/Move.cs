using System;
using UnityEngine;
using UnityEditor;
using BaseClasses;

[AddComponentMenu("SnakeRush/Movement/Move")]
[RequireComponent(typeof(Rigidbody))]
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

    private void UpdateMovementForce() {
        _movementForce.z = _speedHorizontal * _moveHorizontal;
        rigidbody.AddForce(_movementForce);
    }

    private void UpdateMovementSpeed() {
        _movementSpeed.x = _speedVertical;
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, _movementSpeed, Time.deltaTime * _lerpSpeed);
    }

    private void InputHandler() {
        _moveHorizontal = Input.GetAxis("Horizontal");
    }
}
