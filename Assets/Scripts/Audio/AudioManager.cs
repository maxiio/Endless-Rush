using System;
using Core.Managers.GameOver;
using UI.ScreenTransition.Button;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Audio {
	public class AudioManager : MonoBehaviour {
		[Header("Audio Settings")]
		[SerializeField] private AudioSource audioSource;

		[SerializeField] private ButtonAction.Actions actionAtStart = ButtonAction.Actions.PLAY;

		[Header("Music Settings")]
		[SerializeField] private float musicVolume = 0.5f;
		[SerializeField] private bool isMusicEnabled = true;
		[SerializeField] private AudioClips musicClips;

		public bool IsMusicEnabled {
			get => isMusicEnabled;
			set {
				if (isMusicEnabled != value) {
					isMusicEnabled = value;
					audioSource.volume = value ? musicVolume : 0f;
				}
			}
		}

		private void Awake() {
			ButtonSender.ButtonClickedAction += HandleAction;
			GameOverManager.IsGameOver += HandleAction;
		}

		private void Start() {
			HandleAction(this, actionAtStart);
		}

		private void OnDestroy() {
			ButtonSender.ButtonClickedAction -= HandleAction;
			GameOverManager.IsGameOver -= HandleAction;
		}

		private void HandleAction(object sender, ButtonAction.Actions action) {
			PlayByAction(action);
		}

		private void PlayByAction(ButtonAction.Actions action) {
			var currentClipData = musicClips.GetClipByAction(action);
			Play(currentClipData.audioClip, currentClipData.isLoop);
		}

		private void Play(AudioClip clip, bool isLoop = false) {
			if (!IsMusicEnabled) {
				return;
			}

			if (!clip) {
				Debug.LogWarning("Can't play null clip");
				return;
			}

			if (!audioSource) {
				Debug.LogError("audioSource is null");
				return;
			}

			audioSource.clip = clip;
			audioSource.loop = isLoop;
			audioSource.Play();
		}
	}
}