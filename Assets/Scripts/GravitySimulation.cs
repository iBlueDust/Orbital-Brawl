using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravitySimulation : MonoBehaviour {
	public static GravitySimulation instance;

	public float gConstant = 0.001f;
	public float timeStep = 1f;

	[HideInInspector]
	public List<GravityBody> bodies;

	void Awake() {
		if (instance == null) {
			instance = this;
			bodies = Resources.FindObjectsOfTypeAll<GravityBody>().ToList();
			bodies.ForEach(body => RegisterBody(body));
			return;
		}

		this.enabled = false;
	}

	public void AddBody(GravityBody body) {
		RegisterBody(body);
		bodies.Add(body);
	}
	public void RegisterBody(GravityBody body) {
		body.onDestroy += () => RemoveBody(body);
	}
	public bool RemoveBody(GravityBody body) {
		return bodies.Remove(body);
	}

	public float VelocityToOrbit(Vector3 center, float centerMass, Vector3 position) {
		float radius = (position - center).magnitude;
		int orbitDirection = Random.Range(0, 2) * 2 - 1; // -1 or 1
		return Mathf.Sqrt(centerMass * gConstant / radius); // Sqrt(G*m/r)
	}

	// 'tis an O(n^2) operation
	// I had thought of an optimization that was still O(n^2) after I thought about it more
	void FixedUpdate() {
		if (Game.state != GameState.Running)
			return;


		foreach (var body in bodies) {
			if (body != null && body.enabled && body.gameObject.activeInHierarchy) {
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