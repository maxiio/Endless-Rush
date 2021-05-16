using System;
using Core.Player.PhysicsObjectComponent;
using Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Player.Movement {
	public class MoveSystem : PhysicsObject {
		[Header("Force settings")]
		[SerializeField] private bool useForceController;

		[SerializeField] private bool hasConstantXForce;
		[SerializeField] private float directionXForce;

		[Header("Velocity settings")]
		[SerializeField] private bool useVelocityController;

		private IInputHandler _input;
		private IMoveForce _moveForce;
		private ISpeedController _speedController;
		private Vector3 _direction;

		protected override void Awake() {
			base.Awake();

			_input = GetComponent<IInputHandler>();
			if (useForceController) {
				_moveForce = GetComponent<IMoveForce>();
			}

			if (useVelocityController) {
				_speedController = GetComponent<ISpeedController>();
			}
		}

		private void Update() {
			SetDirection();
		}

		private void FixedUpdate() {
			UpdateMovement();
		}

		private void UpdateMovement() {
			if (_moveForce != null && useForceController) {
				Rigidbody.AddForce(_moveForce.GetForce());
			}

			if (_speedController != null && useVelocityController) {
				Rigidbody.velocity = _speedController.GetVelocity(Rigidbody.velocity);
			}
		}

		// Take the user commands
		private void SetDirection() {
			_direction = _input.GetDirection();
			if (hasConstantXForce) {
				_direction.x = directionXForce;
			}

			if (_moveForce != null && useForceController) {
				_moveForce.SetDirection(_direction);
			}
		}
	}
}