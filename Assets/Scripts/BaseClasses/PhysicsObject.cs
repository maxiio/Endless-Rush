using UnityEngine;

namespace BaseClasses {
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PhysicsObject : MonoBehaviour {
        [HideInInspector]
        public new Rigidbody rigidbody;

        protected virtual void Awake() {
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }
    }
}
