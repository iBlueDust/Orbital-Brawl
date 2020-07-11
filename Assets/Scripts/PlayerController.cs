﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerID = System.UInt16;

[RequireComponent(typeof(ShipController))]
public class PlayerController : MonoBehaviour {
	public PlayerID playerId = 0;

	private Controls controls;
	private ShipController ship;

	void Awake() {
		controls = new Controls();
		ship = GetComponent<ShipController>();

		if (playerId == 0) {
			controls.Player1.Fire.performed += _ => ship.Fire();
		} else {
			controls.Player2.Fire.performed += _ => ship.Fire();

		}
	}

	void Update() {
		float thrust = controls.Player1.Thrust.ReadValue<float>();
		float steer = controls.Player1.Steer.ReadValue<float>();

		Debug.Log($"Thrust: {thrust}, Steer: {steer}");

		ship.Move(new Vector2(steer, thrust));
	}

	void OnEnable() {
		controls.Enable();
	}
	void OnDisable() {
		controls.Disable();
	}
}