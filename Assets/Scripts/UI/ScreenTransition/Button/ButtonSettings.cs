using UnityEngine;

namespace UI.ScreenTransition.Button {
	[CreateAssetMenu(fileName = "ButtonSettings", menuName = "ScriptableObjects/UI/Button")]
	public class ButtonSettings : ScriptableObject {
		public bool isEnabled;
		public int maxCountOfUsage;
		public ButtonAction.Actions action;
	}
}