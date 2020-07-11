using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
	Running,
	Paused,
	Menu,
	BeforeStart,
	AfterEnd,
}

public class Game : MonoBehaviour {
	public static GameState state {
		get => state;
		set {
			state = value;
			if (onStateChange != null)
				onStateChange(value);
		}
	}

	public static event Action<GameState> onStateChange;

	private Controls controls;

	void Start() {
		if (Debug.isDebugBuild)
			onStateChange += state => Debug.Log("GameState change: " + state);

		controls = new Controls();
		controls.Enable();

		controls.Player1.Fire.performed += _ => StartGame();
		controls.Player2.Fire.performed += _ => StartGame();
	}

	void StartGame() {
		controls.Disable();
		state = GameState.Running;
	}

	public static void OnPlayerDeath(GameObject player, int playerId) {
		state = GameState.AfterEnd;
	}
}