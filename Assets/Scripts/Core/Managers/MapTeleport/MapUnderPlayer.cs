using UnityEngine;

namespace Core.Managers.MapTeleport {
	public class MapUnderPlayer : MonoBehaviour {
		[Header("Object to follow")] [SerializeField]
		private GameObject player;

		[Header("Configuration")] [Tooltip("The max size of the current gameobject")] [SerializeField]
		private int size = 100;

		[Tooltip("We need one more platform forward for not visible teleportation of this object")] [SerializeField]
		private bool isNeedOneMorePlatform;

		private void Awake() {
			if (isNeedOneMorePlatform) {
				isNeedOneMorePlatform = false;
				var cloneOfObject = Instantiate(gameObject);
				var defaultXOffset = size;
				Move(cloneOfObject.transform, defaultXOffset);
			}
		}

		private void FixedUpdate() {
			if (player.transform.position.x > transform.position.x + size) {
				var xAxisOffset = size * 2;
				Move(transform, xAxisOffset);
			}
		}

		private void Move(Transform transformToMove, int xAxisOffset) {
			transformToMove.Translate(xAxisOffset, 0, 0);
		}
	}
}