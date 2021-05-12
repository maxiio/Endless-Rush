using UnityEngine.UI;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class DisplayOnText : MonoBehaviour {
    [Tooltip("Set to the child text this tag if you need to display the current health")]
    [SerializeField] protected string _tagName = "HealthText";

    [Header("Text look at camera")]
    [Tooltip("Set rotation of object to camera movement direction")]
    [SerializeField] protected bool _isCameraRotationDirection;
    [SerializeField] protected Transform _cameraTransform;
    [SerializeField] protected string _cameraTag = "MainCamera";

    protected Text[] _texts;

    // Find all text objects at childrens
    protected virtual void Awake() {
        _texts = gameObject.GetComponentsInChildren<Text>();

        SetCameraTransform();
        if (_isCameraRotationDirection) {
            RotateTextsToCamera();        
        }
    }

    // Each text object look at camera
    private void RotateTextsToCamera() {
        foreach (var text in _texts) {
            text.transform.rotation = _cameraTransform.rotation;
        }
    }

    // Find camera on the scene by tag
    protected void SetCameraTransform() {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag(_cameraTag);
        if (cameras.Length > 1 && !_cameraTransform) {
            throw new Exception("On scene more then 2 cameras. Set the camera to DisplayOnText.cs");
        }
        _cameraTransform = cameras[0].transform;
    }    

    // Set to each text the current sender health
    protected void UpdateText(object sender, float value) {
        foreach (var textComponent in _texts) {
            if (textComponent.tag == _tagName) {
                textComponent.text = ((int)value).ToString();
            }
        }
    }
}