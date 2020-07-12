using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Drops "pickables" all over the scene, such as heal canisters
public class PickableDropper : MonoBehaviour {
	public GameObject orbitCenter;

	[Header("Heal Canisters")]
	public GameObject healCanister;
	public float timeBetweenDrops = 10f;
	public float timeBetweenDropsDeviation = 5f;

	private Action<GameState> onGameStateChange;

	void Awake() {
		onGameStateChange = _ => {
			if (Game.state == GameState.Running)
				dropHealCanisters = StartCoroutine(DropHealCanisters());
			else {
				StopCoroutine(dropHealCanisters);
				dropHealCanisters = null;
			}
		};
	}

	void Start() {
		Game.instance.onStateChange += onGameStateChange;
	}

	void OnEnable() {
		onGameStateChange(Game.state);
	}

	void OnDisable() {
		StopCoroutine(dropHealCanisters);
		dropHealCanisters = null;
	}

	void OnDestroy() {
		Game.instance.onStateChange -= onGameStateChange;
	}

	private Coroutine dropHealCanisters = null;
	private IEnumerator DropHealCanisters() {

		// Max of camera view width and height
		var halfCamHeight = Camera.main.orthographicSize;
		var halfCamWidth = Camera.main.orthographicSize / Camera.main.aspect;

		while (Game.state == GameState.Running) {
			yield return new WaitForSeconds(Random.Range(timeBetweenDrops - timeBetweenDropsDeviation, timeBetweenDrops + timeBetweenDropsDeviation));

			// Instantiate canister just outside of camera bounds
			var relativePos = new Vector2(
				Random.Range(-4f, 4f),
				Random.Range(-4f, 4f)
			);
			if (relativePos.x < 0)
				relativePos.x -= halfCamWidth;
			else
				relativePos.x += halfCamWidth;

			if (relativePos.y < 0)
				relativePos.y -= halfCamHeight;
			else
				relativePos.y += halfCamHeight;

			Vector2 position = Camera.main.transform.TransformPoint(relativePos);
			var rotation = Quaternion.Euler(0f, 0f, Random.Range(-360f, 360f));

			var canister = Instantiate(healCanister, position, rotation);

			// Give canister velocity to orbit the sun
			Vector2 radiusVector = orbitCenter.transform.InverseTransformPoint(position);
			int orbitDirection = Random.Range(0, 2) * 2 - 1; // -1 or 1
			float velocity = GravitySimulation.instance.VelocityToOrbit(
				orbitCenter.transform.position,
				orbitCenter.GetComponent<Rigidbody2D>().mass,
				position
			);
			canister.GetComponent<HealCanister>().SetVelocity(Vector2.Perpendicular(radiusVector * orbitDirection) * velocity);
		}

		dropHealCanisters = null;
	}
}