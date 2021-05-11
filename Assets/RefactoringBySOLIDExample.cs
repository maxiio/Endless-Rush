using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour {
	[SerializeField] private string selectableTag = "Selectable";

	private IRayProvider _rayProvider;
	private ISelector _selector;
	private ISelectionResponse _selectionResponse;
	
	private Transform _currentSelection;
	
	private void Awake() {
		_rayProvider = GetComponent<IRayProvider>();
		_selector = GetComponent<ISelector>();
		_selectionResponse = GetComponent<ISelectionResponse>();
	}

	private void Update() {
		if (_currentSelection != null) {
			_selectionResponse.OnDeselect(_currentSelection);
		}

		_selector.Check(_rayProvider.CreateRay());
		_currentSelection = _selector.GetSelection();

		if (_currentSelection != null) {
			_selectionResponse.OnSelect(_currentSelection);
		}
	}
}