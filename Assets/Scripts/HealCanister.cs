using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HealCanister : MonoBehaviour {
	[Range(0f, 360f)]
	public float maxSpinSpeed;

	public float healPower = 30f;

	private new Rigidbody2D rigidbody;

	void Awake() {
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.angularVelocity = Random.Range(-maxSpinSpeed, maxSpinSpeed);
	}

	public void SetVelocity(Vector2 velocity) {
		rigidbody.velocity = velocity;
	}
}