using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravitySimulation : MonoBehaviour {
	public static GravitySimulation instance;

	public float gConstant = 0.001f;
	public float timeStep = 1f;

	[HideInInspector]
	public List<GravityBody> bodies { private get; set; }

	void Awake() {
		if (instance == null) {
			instance = this;
			bodies = Resources.FindObjectsOfTypeAll<GravityBody>().ToList();
			return;
		}

		this.enabled = false;
	}

	// 'tis an O(n^2) operation
	// I had thought of an optimization that was still O(n^2) after I thought about it more
	void FixedUpdate() {
		foreach (var body in bodies) {
			if (body.enabled && body.gameObject.activeInHierarchy) {
				body.AddVelocity(CalculateAcceleration(body), timeStep * Time.deltaTime);
			}
		}
	}

	Vector2 CalculateAcceleration(GravityBody body) {
		Vector2 force = Vector2.zero;
		foreach (var b in bodies) {
			if (b != body) {
				var difference = (b.position - body.position);
				var direction = difference.normalized;

				force += direction * (gConstant * (b.mass * body.mass / difference.sqrMagnitude));
			}
		}
		// F = m * a
		return force / body.mass;
	}
}