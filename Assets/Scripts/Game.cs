using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState {
	Running,
	Paused,
	Menu,
	BeforeStart,
	AfterEnd,
}

public class Game : MonoBehaviour {
	public static GameState state {
		get => instance._state;
		set {
			instance._state = value;
			instance.onStateChange(instance._state);
		}
	}

	public static Game instance;
	public GameState _state = GameState.BeforeStart;

	public event Action<GameState> onStateChange = delegate { };
	public event Action<int> onPlayerWin = delegate { };

	private Controls controls;

	void Awake() {
		if (instance == null)
			instance = this;
		else {
			Debug.LogWarning("Another running Game instance has been found. Disabling self...");
			this.enabled = false;
		}

		controls = new Controls();
		controls.Enable();
		controls.Menu.Pause.performed += _ => {
			// Double check in case GameState = BeforeStart
			switch (state) {
				case GameState.Running:
					state = GameState.Paused;
					break;
				case GameState.BeforeStart:
				case GameState.Paused:
				case GameState.Menu:
					state = GameState.Running;
					break;
			}
		};
	}

	void Start() {
		if (Debug.isDebugBuild)
			onStateChange += state => Debug.Log("GameState change: " + state);

		var defaultScale = Time.timeScale;
		onStateChange += state => Time.timeScale = state == GameState.Running ? defaultScale : 0f;

		StartCoroutine(AwaitFirstInput());
	}

	IEnumerator AwaitFirstInput() {
		while (!Keyboard.current.anyKey.isPressed) {
			yield return null;
		}

		state = GameState.Running;
	}

	public void OnPlayerDeath(GameObject player, int playerId) {
		state = GameState.AfterEnd;
		onPlayerWin(playerId == 0 ? 1 : 0);
	}

	public void Pause() {
		state = GameState.Paused;
	}
	public void Resume() {
		state = GameState.Running;
	}
	public void Restart() {
		Debug.Log("Restart");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void Quit() {
		Application.Quit(0);
	}
}