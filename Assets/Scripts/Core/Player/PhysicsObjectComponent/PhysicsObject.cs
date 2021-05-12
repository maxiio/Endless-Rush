using UnityEngine;

namespace Core.Player.PhysicsObjectComponent {
	public abstract class PhysicsObject : MonoBehaviour {
		protected Rigidbody Rigidbody;

		protected virtual void Awake() {
			Rigidbody = gameObject.GetComponent<Rigidbody>();
		}
	}
}