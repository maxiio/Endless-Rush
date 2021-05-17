using System;
using System.Collections;
using UI.ScreenTransition.Button;
using UnityEngine;

namespace Audio {
	[CreateAssetMenu(fileName = "AudioClips", menuName = "ScriptableObjects/Audio")]
	public class AudioClips : ScriptableObject {
		[Serializable]
		public struct ClipData {
			public ButtonAction.Actions action;
			public AudioClip audioClip;
			public bool isLoop;
		}

		public ClipData[] clips;

		public ClipData GetClipByAction(ButtonAction.Actions action) {
			foreach (var clip in clips) {
				if (action == clip.action) {
					return clip;
				}
			}

			Debug.LogError($"No clip with action {action}");
			return default;
		}
	}
}