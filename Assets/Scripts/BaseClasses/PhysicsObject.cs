using UnityEngine;

namespace BaseClasses {
    public abstract class PhysicsObject : MonoBehaviour {
        protected new Rigidbody rigidbody;

        protected virtual void Awake() {
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }
    }
}
