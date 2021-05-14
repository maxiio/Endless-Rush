using System;
using UnityEngine;

namespace Audio {
	public class AudioManager : MonoBehaviour {
		

		[Header("Audio Settings")] [SerializeField]
		private AudioSource audioSource;

		[Header("Music Settings")] [SerializeField]
		private float musicVolume = 0.5f;

		[SerializeField] private bool isMusicEnabled = true;
		[SerializeField] private AudioClips musicClips;
		//[SerializeField] private ClipDictionary[] musicClips;

		public bool IsMusicEnabled {
			get => isMusicEnabled;
			set {
				if (isMusicEnabled != value) {
					isMusicEnabled = value;
					// TODO : Check this statement, why we should to change volume
					audioSource.volume = value ? musicVolume : 0f;
					// Here we can SAVE to prefs the settings
				}
			}
		}

		private void Start() {
			Play(AudioClips.ClipName.START_MENU, true);
		}


		public void Play(AudioClips.ClipName clipName, bool isLoop = false, float volume = 1f) {
			audioSource = GetAudioSource(clipName, isLoop, volume);
			audioSource.Play();
		}
		
		public void PlayOneShot(AudioClips.ClipName clipName) {
			audioSource.PlayOneShot(GetAudioClipByName(clipName));
		}

		private AudioSource GetAudioSource(AudioClips.ClipName clipName, bool isLoop, float volume) {
			AudioSource source = audioSource;
			source.clip = GetAudioClipByName(clipName);
			source.volume = volume;
			source.loop = isLoop;
			return source;
		}

		private AudioClip GetAudioClipByName(AudioClips.ClipName clipName) {
			foreach (var clip in musicClips.clips) {
				if (clip.clipName == clipName) {
					return clip.audioClip;
				}
			}

			throw new InvalidOperationException($"Not found clip {clipName} at clips dictionary");
		}
	}
}