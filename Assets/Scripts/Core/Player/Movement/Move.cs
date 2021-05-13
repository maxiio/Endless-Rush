using Core.Player.PhysicsObjectComponent;
using Input;
using UnityEngine;

namespace Core.Player.Movement {
	public class Move : PhysicsObject {
		[Header("Movement")] [SerializeField] private float speedHorizontal;
		[SerializeField] private float speedVertical;
		[SerializeField] [Range(.2f, 10)] private float lerpSpeed;

		private float _moveHorizontal;
		private Vector3 _movementForce;
		private Vector3 _movementSpeed;
		private IInputHandler _input;

		protected override void Awake() {
			base.Awake();
			
			_input = GetComponent<IInputHandler>();
		}

		private void Update() {
			InputHandler();
		}

		private void FixedUpdate() {
			UpdateMovementForce();
			UpdateMovementSpeed();
		}

		// Update the acceleration on Z axis (left-right moving)
		private void UpdateMovementForce() {
			_movementForce.z = speedHorizontal * _moveHorizontal;
			Rigidbody.AddForce(_movementForce);
		}

		// Update the speed of body while he is stopped
		private void UpdateMovementSpeed() {
			_movementSpeed.x = speedVertical;
			Rigidbody.velocity = Vector3.Lerp(Rigidbody.velocity, _movementSpeed, Time.deltaTime * lerpSpeed);
		}

		// Take the user commands
		private void InputHandler() {
			// Get the horizontal direction
			_moveHorizontal = _input.GetHorizontal();
		}
	}
}