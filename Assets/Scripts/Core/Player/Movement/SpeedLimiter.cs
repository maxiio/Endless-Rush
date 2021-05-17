using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Player.Movement {
	public class SpeedLimiter : MonoBehaviour, ISpeedController {
		[Header("Speed limits")]
		[SerializeField] private float maxXAxisSpeed;
		[SerializeField] private bool isLockedXAxis;
		[SerializeField] private bool isLockedYAxis;
		[SerializeField] private bool isLockedZAxis;

		private void LockAxis(ref float axisSpeed, bool isLockedAxis) {
			if (isLockedAxis) {
				axisSpeed = axisSpeed > maxXAxisSpeed ? maxXAxisSpeed : axisSpeed;
			}
		}
		
		public Vector3 GetVelocity(Vector3 currentVelocity) {
			LockAxis(ref currentVelocity.x, isLockedXAxis);
			LockAxis(ref currentVelocity.y, isLockedYAxis);
			LockAxis(ref currentVelocity.z, isLockedZAxis);
			
			return currentVelocity;
		}
	}
}