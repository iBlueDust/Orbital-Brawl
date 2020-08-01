using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public SceneReference gameScene;

	public void Play() {
		SceneManager.LoadScene(gameScene.ScenePath);
		Game.state = GameState.BeforeStart;
	}

	public void Quit() {
		Application.Quit(0);
	}
}