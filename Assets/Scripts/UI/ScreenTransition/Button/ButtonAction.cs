using System;
using Core.Managers.Actions;

namespace UI.ScreenTransition.Button {
	public abstract class ButtonAction {
		public static event EventHandler<ActionManager.Request> RequestedAction;

		// The all actions which button's can send
		public enum Actions {
			NULL,
			PLAY,
			PAUSE,
			MENU,
			RESTART,
			CONTINUE,
			EXIT,
			GAMEOVER,
			REVIVE
		}

		public static void HandleAction(Actions action) {
			switch (action) {
				case Actions.NULL:
					throw new Exception("Handle the Null action");
				case Actions.RESTART:
					RestartPlay();
					UnPause();
					break;
				case Actions.PLAY:
					UnPause();
					break;
				case Actions.CONTINUE:
					UnPause();
					break;
				case Actions.REVIVE:
					RevivePlayer();
					UnPause();
					break;
				case Actions.MENU:
				case Actions.GAMEOVER:
				case Actions.PAUSE:
					Pause();
					break;
				case Actions.EXIT:
					Pause();
					Exit();
					break;
				default:
					throw new Exception("Handle the default action");
			}
		}

		private static void RestartPlay() {
			RequestedAction?.Invoke(null, ActionManager.Request.RESTART);
		}

		private static void RevivePlayer() {
			RequestedAction?.Invoke(null, ActionManager.Request.REVIVE);
		}

		private static void Pause() {
			RequestedAction?.Invoke(null, ActionManager.Request.PAUSE);
		}

		private static void UnPause() {
			RequestedAction?.Invoke(null, ActionManager.Request.UNPAUSE);
		}

		private static void Exit() {
			RequestedAction?.Invoke(null, ActionManager.Request.EXIT);
		}
	}
}