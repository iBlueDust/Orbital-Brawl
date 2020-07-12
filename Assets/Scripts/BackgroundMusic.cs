using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour {

	public AudioClip startClip;
	public AudioClip loopClip;

	private AudioSource audioSource;

	private Action<GameState> onGameStateChange;

	void Awake() {
		audioSource = GetComponent<AudioSource>();

		onGameStateChange = state => {
			if (Game.state == GameState.Running)
				playGameplayMusic = StartCoroutine(PlayGameplayMusic());
			else {
				audioSource.Stop();

				if (playGameplayMusic != null) {
					StopCoroutine(playGameplayMusic);
					playGameplayMusic = null;
				}
			}
		};
		Game.onStateChange += onGameStateChange;
	}

	void OnEnable() {
		onGameStateChange(Game.state);
	}

	void OnDisable() {
		StopCoroutine(playGameplayMusic);
		playGameplayMusic = null;
	}

	void OnDestroy() {
		Game.onStateChange -= onGameStateChange;
	}

	private Coroutine playGameplayMusic = null;
	private IEnumerator PlayGameplayMusic() {
		audioSource.clip = startClip;
		audioSource.Play();
		yield return new WaitForSeconds(audioSource.clip.length);

		audioSource.loop = true;
		audioSource.clip = loopClip;
		audioSource.Play();

		playGameplayMusic = null;
	}
}