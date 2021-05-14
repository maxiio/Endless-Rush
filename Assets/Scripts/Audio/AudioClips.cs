using System;
using UnityEngine;

namespace Audio {
	[CreateAssetMenu(fileName = "AudioClips", menuName = "ScriptableObjects/Audio")]
	public class AudioClips : ScriptableObject {
		[Serializable]
		public struct ClipDictionary {
			public ClipName clipName;
			public AudioClip audioClip;
		}

		public enum ClipName {
			NONE,
			START_MENU,
			PAUSE_MENU,
			GAME,
			GAMEOVER,
			HIT,
			HEAL
		}
		
		public ClipDictionary[] clips;
	}
}