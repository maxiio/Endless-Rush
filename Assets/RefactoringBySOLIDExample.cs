using UnityEngine;

public class SelectionManager : MonoBehaviour {
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material higlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;

    private void Update() {
        if (_selection != null) {
            var selection = _selection;
            var selectionRenderer = _selection.GetComponent<Renderer>();
            if (selectionRenderer != null) {
                selectionRenderer.material = defaultMaterial;
            }
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        _selection = null;
        if (Physics.Raycast(ray, out var hit)) {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag)) {
                _selection = selection;
            }
        }

        if (_selection != null) {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            if (selectionRenderer != null) {
                selectionRenderer.material = higlightMaterial;
            }
        }
    }
}
