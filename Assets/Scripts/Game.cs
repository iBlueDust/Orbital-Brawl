using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState {
	Running,
	Paused,
	Menu,
	BeforeStart,
	AfterEnd,
}

public class Game : MonoBehaviour {
	private static GameState _state = GameState.BeforeStart;
	public static GameState state {
		get => _state;
		set {
			_state = value;
			if (onStateChange != null)
				onStateChange(_state);
		}
	}

	public static event Action<GameState> onStateChange;

	void Start() {
		if (Debug.isDebugBuild)
			onStateChange += state => Debug.Log("GameState change: " + state);

		StartCoroutine(AwaitFirstInput());
	}

	IEnumerator AwaitFirstInput() {
		while (!Keyboard.current.anyKey.isPressed) {
			yield return null;
		}

		state = GameState.Running;
	}

	public static void OnPlayerDeath(GameObject player, int playerId) {
		state = GameState.AfterEnd;
	}


}