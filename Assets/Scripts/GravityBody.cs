using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityBody : MonoBehaviour {

	[HideInInspector]
	public Vector2 position {
		get => new Vector2(transform.position.x, transform.position.y);
	}
	public float mass;

	public float gravityWeight = 1f;

	private new Rigidbody2D rigidbody;

	void Awake() {
		rigidbody = GetComponent<Rigidbody2D>();
		if (mass == 0f)
			mass = rigidbody.mass;
	}

	public void AddVelocity(Vector2 acceleration, float time) {
		if (rigidbody.bodyType != RigidbodyType2D.Static)
			rigidbody.velocity += acceleration * time * gravityWeight;
	}

}