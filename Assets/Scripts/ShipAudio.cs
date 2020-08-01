using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ShipController))]
public class ShipAudio : MonoBehaviour {

	public AudioClip healSound;
	public AudioClip damageSound;
	public AudioClip deathSound;

	private AudioSource audioSource;
	private ShipController controller;

	private Action<float, float> onHealthChange;
	// Death sounds are not handled here since this gameObject may be destroyed before the whole SFX can be played

	void Awake() {
		audioSource = GetComponent<AudioSource>();
		controller = GetComponent<ShipController>();

		onHealthChange = (health, change) => {
			audioSource.clip = change < 0f ? damageSound : healSound;
			audioSource.Play();
		};
	}

	void OnEnable() {
		controller.onHealthChange += onHealthChange;
	}
	void OnDisable() {
		controller.onHealthChange -= onHealthChange;
	}
}