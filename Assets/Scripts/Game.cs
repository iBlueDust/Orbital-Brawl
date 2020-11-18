using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;

public enum GameState {
	Running,
	Paused,
	Menu,
	BeforeStart,
	AfterEnd,
}

public class Game : MonoBehaviour {
	public static GameState state {
		get => instance?._state ?? GameState.Menu;
		set {
			if (instance == null)
				return;

			instance._state = value;
			instance.onStateChange(instance._state);
		}
	}

	public static Game instance;
	public GameState _state = GameState.Menu;

	public SceneReference mainMenuScene;
	public SceneReference gameScene;

	public event Action<GameState> onStateChange = delegate { };
	public event Action<int> onPlayerWin = delegate { };

	private Controls controls;

	void Awake() {
		if (instance == null)
			instance = this;
		else {
			Debug.LogWarning("Another running Game instance has been found. Disabling self...");
			this.enabled = false;
			return;
		}

		controls = new Controls();
	}

	void Start() {
		if (Debug.isDebugBuild)
			onStateChange += state => Debug.Log("GameState change: " + state);

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

		FindPlayerSpawns();

		// This might be a nightmare in the future but
		// If you wanna change the default timeScale, go here :)))
		Time.timeScale = 0f;
		onStateChange += state => Time.timeScale = state == GameState.Running ? 1f : 0f;

		onStateChange += state => {
			if (state == GameState.BeforeStart) {
				StartCoroutine(AwaitFirstInput());
				ChoosePlayerSpawn();
			}
		};
		if (state == GameState.BeforeStart) {
			StartCoroutine(AwaitFirstInput());
			ChoosePlayerSpawn();
		}
	}

	IEnumerator AwaitFirstInput() {
#if UNITY_WEBGL
		while (Keyboard.current == null || !Keyboard.current.anyKey.isPressed) {
			yield return null;
		}
#else
		while (!Keyboard.current.anyKey.isPressed) {
			yield return null;
		}
#endif

		state = GameState.Running;
	}

	private struct PlayerSpawnArea {
		public Vector3 camera;
		public Vector3[] players;
	}

	private PlayerSpawnArea[] playerSpawns;
	public void FindPlayerSpawns() {
		playerSpawns = GameObject.FindGameObjectsWithTag("Spawn Area")
			.Select(g => {
				var spawnArea = new PlayerSpawnArea();
				spawnArea.players = new Vector3[2];
				spawnArea.camera = g.transform.position;

				foreach (Transform child in g.transform) {
					switch (child.tag) {
						case "P1 Spawn":
							spawnArea.players[0] = child.position;
							break;
						case "P2 Spawn":
							spawnArea.players[1] = child.position;
							break;
					}
				}

				return spawnArea;
			})
			.ToArray();
	}

	public void ChoosePlayerSpawn() {
		var players = GameObject.FindGameObjectsWithTag("Player");

		var p1 = players.FirstOrDefault((p) => p.name == "Player 1") ?? players[0];
		var p2 = players.FirstOrDefault((p) => p.name == "Player 2") ?? players[1];

		var spawnArea = playerSpawns[Random.Range(0, playerSpawns.GetLength(0))];
		p1.transform.SetPositionAndRotation(spawnArea.players[0], Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
		p2.transform.SetPositionAndRotation(spawnArea.players[1], Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
		Camera.main.transform.position = new Vector3(spawnArea.camera.x, spawnArea.camera.y, Camera.main.transform.position.z);
	}

	public void OnPlayerDeath(GameObject player, int playerId) {
		state = GameState.AfterEnd;
		onPlayerWin(playerId == 0 ? 1 : 0);
	}

	public void OnEnable() {
		controls.Enable();
	}

	public void OnDisable() {
		controls.Disable();
	}

	#region UI Handlers

	public void Pause() {
		state = GameState.Paused;
	}

	public void Resume() {
		state = GameState.Running;
	}

	public void Restart() {
		Debug.Log("Restart");
		SceneManager.LoadScene(gameScene.ScenePath);
		state = GameState.BeforeStart;
	}

	public void MainMenu() {
		SceneManager.LoadScene(mainMenuScene.ScenePath);
		state = GameState.Menu;
	}

	#endregion
}