using UnityEngine;

namespace UI.ScreenTransition.Button {
	public class ReviveButton : ButtonSender {
		[SerializeField] private ButtonSettings settings;

		private bool _isClickable;
		private int _countOfUsage;

		private new void Awake() {
			base.Awake();
			Initialize();
		}

		private void Start() {
			if (!_isClickable) {
				SetViewOff();
			}
		}

		private void Initialize() {
			action = settings.action;
			_countOfUsage = settings.maxCountOfUsage;
			_isClickable = settings.isEnabled;
		}

		private void SetViewOff() {
			gameObject.SetActive(false);
		}

		public new void Clicked() {
			if (_isClickable) {
				ReviveButtonAction();
				_countOfUsage--;
				_isClickable = _countOfUsage > 0;
				base.Clicked();
			}

			if (!_isClickable) {
				SetViewOff();
			}
		}

		private void ReviveButtonAction() {
			// TODO - make advertise
			Debug.Log("ReviveButtonAction");
		}
	}
}