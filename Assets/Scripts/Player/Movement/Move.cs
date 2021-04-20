using System;
using UnityEngine;
using BaseClasses;

[AddComponentMenu("SnakeRush/Movement/Move")]
[RequireComponent(typeof(Rigidbody))]
public class Move : PhysicsObject {
    [Header("Movement")]
    [SerializeField] private float _speedHorizontal;
    [SerializeField] private float _speedVertical;

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
        rigidbody.velocity = _movementSpeed;
    }

    private void InputHandler() {
        _moveHorizontal = Input.GetAxis("Horizontal");
    }
}
