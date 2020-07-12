using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityBody : MonoBehaviour {

	[HideInInspector]
	public Vector2 position {
		get => new Vector2(transform.position.x, transform.position.y);
	}

	public bool overrideMass = false;
	public float mass = 100f;

	public float gravityWeight = 1f;
	public float gravityOffset = 0f;

	private new Rigidbody2D rigidbody;

	void Awake() {
		rigidbody = GetComponent<Rigidbody2D>();
		if (!overrideMass)
			mass = rigidbody.mass;
	}

	public void AddVelocity(Vector2 acceleration, float time) {
		if (rigidbody.bodyType == RigidbodyType2D.Static)
			return;

		Vector2 deltaV = acceleration * time * gravityWeight;
		rigidbody.velocity += deltaV;

		// Revert gravity as long as deltaV is within power
		if (gravityOffset == 0f)
			return;

		if (deltaV.sqrMagnitude <= gravityOffset * gravityOffset * time * time)
			rigidbody.velocity -= deltaV;
		else {
			rigidbody.velocity -= deltaV;
			rigidbody.velocity += (deltaV / deltaV.magnitude) * (deltaV.magnitude - gravityOffset * time);
		}
	}

	public event Action onDestroy;
	void OnDestroy() {
		if (onDestroy != null)
			onDestroy();
	}

}