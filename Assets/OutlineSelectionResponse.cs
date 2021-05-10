using UnityEngine;
using UnityEngine.UI;

public class OutlineSelectionResponse : MonoBehaviour, ISelectionResponse{
	public void OnSelect(Transform selection) {
		var outline = selection.GetComponent<Outline>();
		if (outline != null) {
			// I doesn't have this asset package
			//outline.OutlineWidth = 10;
		}
	}

	public void OnDeselect(Transform selection) {
		var outline = selection.GetComponent<Outline>();
		if (outline != null) {			
			// I doesn't have this asset package
			//outline.OutlineWidth = 0;
		}
	}
}