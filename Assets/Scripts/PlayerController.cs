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

		if (playerId == 0) {
			controls.Player1.Fire.performed += _ => ship.Fire();
			GetMovementInput = () => controls.Player1.Movement.ReadValue<Vector2>();
		} else {
			controls.Player2.Fire.performed += _ => ship.Fire();
			GetMovementInput = () => controls.Player2.Movement.ReadValue<Vector2>();
		}

		ship.onDeath += () => Game.OnPlayerDeath(gameObject, playerId);
	}

	void Update() {
		if (Game.state != GameState.Running)
			return;

		ship.Move(GetMovementInput());
	}

	void OnEnable() {
		controls.Enable();
	}
	void OnDisable() {
		controls.Disable();
	}
}