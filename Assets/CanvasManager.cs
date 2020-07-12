using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
	public Canvas pauseCanvas;
	public Canvas beforeStartCanvas;
	public Canvas afterEndCanvas;

	[Header("Misc")]
	public Text winnerText;
	public Color player1;
	public Color player2;

	// Cleaning up is not necessary since this component lives alongside Game
	void Start() {
		Game.instance.onStateChange += state => {
			pauseCanvas.gameObject.SetActive(state == GameState.Paused);
			beforeStartCanvas.gameObject.SetActive(state == GameState.BeforeStart);
			afterEndCanvas.gameObject.SetActive(state == GameState.AfterEnd);
		};

		Game.instance.onPlayerWin += playerId => {
			if (playerId == 0) {
				winnerText.text = "Player 1";
				winnerText.color = player1;
			} else {
				winnerText.text = "Player 2";
				winnerText.color = player2;
			}
		};
	}
}