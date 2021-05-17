using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Health {
	[ExecuteInEditMode]
	public class DisplayOnText : MonoBehaviour {
		[Tooltip("Set to the child text this tag if you need to display the current health")] [SerializeField]
		protected string tagName = "HealthText";

		[Header("Text look at camera")]
		[Tooltip("Set rotation of object to camera movement direction")]
		[SerializeField]
		protected bool isCameraRotationDirection;

		[SerializeField] protected Transform cameraTransform;
		[SerializeField] protected string cameraTag = "MainCamera";

		private Text[] _texts;

		// Find all text objects at children's
		protected virtual void Awake() {
			_texts = gameObject.GetComponentsInChildren<Text>();

			SetCameraTransform();
			if (isCameraRotationDirection) {
				RotateTextsToCamera();
			}
		}

		// Each text object look at camera
		private void RotateTextsToCamera() {
			foreach (var text in _texts) {
				text.transform.rotation = cameraTransform.rotation;
			}
		}

		// Find camera on the scene by tag
		private void SetCameraTransform() {
			var cameras = GameObject.FindGameObjectsWithTag(cameraTag);
			if (cameras.Length > 1 && !cameraTransform) {
				throw new Exception("On scene more then 2 cameras. Set the camera to DisplayOnText.cs");
			}

			cameraTransform = cameras[0].transform;
		}

		// Set to each text the current sender health
		protected void UpdateText(object sender, float value) {
			foreach (var textComponent in _texts) {
				if (textComponent.CompareTag(tagName)) {
					textComponent.text = ((int) value).ToString();
				}
			}
		}
	}
}