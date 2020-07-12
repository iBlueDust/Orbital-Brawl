using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

	public Transform orbitCenter;
	public float orbitSpeed = 10f;
	public float rotationSpeed = 10f;

	private float orbitAngle;
	private float orbitRadius;

	void Start() {
		var orbitRelativePosition = transform.position - orbitCenter.position;

		orbitRadius = orbitRelativePosition.magnitude;
		orbitAngle = Mathf.Atan2(orbitRelativePosition.y, orbitRelativePosition.x);
	}

	void FixedUpdate() {
		if (Game.state != GameState.Running)
			return;

		orbitAngle += orbitSpeed * Mathf.Deg2Rad * Time.deltaTime;

		var orbitRelativePosition = new Vector3(
			Mathf.Cos(orbitAngle) * orbitRadius,
			Mathf.Sin(orbitAngle) * orbitRadius,
			0f
		);
		transform.position = orbitRelativePosition + orbitCenter.position;

		transform.Rotate(0f, 0f, Time.deltaTime * rotationSpeed);
	}
}