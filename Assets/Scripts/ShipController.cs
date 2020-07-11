using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour {
	public GameObject bullet;

	[Header("Configuration")]
	public float moveSpeed = 10f;
	public float steerSpeed = 10f;
	public float bulletSpeed = 100f;
	public float steerDampTime = 0.5f; // 500ms?

	private float steerVelocity = 0f; // For Mathf.SmoothDamp

	private Rigidbody2D rigidbody;

	void Awake() {
		rigidbody = GetComponent<Rigidbody2D>();
	}


	void FixedUpdate() {

	}

	public void Thrust(float input) {
		// rigidbody.AddRelativeForce(Vector2.up * Time.deltaTime * moveSpeed);
		rigidbody.velocity = transform.up * moveSpeed * input;
	}

	public void Steer(float input) {
		// rigidbody.AddTorque(input * Time.deltaTime * steerSpeed);
		rigidbody.angularVelocity = -input * steerSpeed;
	}

	public void Move(Vector2 input) {
		if (input.sqrMagnitude > 1)
			input.Normalize();

		if (input.x != 0f || input.y != 0f) {
			// Unity uses degrees for transforms; In Mathf.Atan2, 0deg = Right
			float degrees = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg + 90f;
			rigidbody.rotation = Mathf.SmoothDampAngle(rigidbody.rotation, degrees, ref steerVelocity, steerDampTime, float.PositiveInfinity);
		}
		input *= moveSpeed * Time.deltaTime;
		rigidbody.position += input;
	}

	public void Fire() {
		var b = Instantiate(bullet, transform.position, transform.rotation);
		b.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
	}
}