using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using PlayerID = System.UInt16;

[RequireComponent(typeof(ShipController))]
public class PlayerController : MonoBehaviour {
	public PlayerID playerId = 0;

	private Controls controls;
	private ShipController ship;
	// Just ignore the possible security vulnerability here (it's not C/C++ anyway)
	private Func<bool> GetFireInput;
	private Func<Vector2> GetMovementInput;

	void Awake() {
		controls = new Controls();
		ship = GetComponent<ShipController>();

		Action<InputAction.CallbackContext> fireHandler = _ => {
			if (Game.state == GameState.Running)
				ship.Fire();
		};

		if (playerId == 0) {
			controls.Player1.Fire.performed += fireHandler;
			GetMovementInput = () => controls.Player1.Movement.ReadValue<Vector2>();
		} else {
			controls.Player2.Fire.performed += fireHandler;
			GetMovementInput = () => controls.Player2.Movement.ReadValue<Vector2>();
		}

		// No need to unregister this since controller will be trashed alongside this class
		ship.onDeath += () => OnDeath();
	}

	void Update() {
		if (Game.state != GameState.Running)
			return;

		ship.Move(GetMovementInput());
	}

	private void OnDeath() {
		Game.instance.OnPlayerDeath(gameObject, playerId);
	}

	void OnEnable() {
		controls.Enable();
	}
	void OnDisable() {
		controls.Disable();
	}
}