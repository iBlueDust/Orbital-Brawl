using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour {
	public GameObject bullet;

	[Header("Configuration")]
	public float moveSpeed = 10f;
	public float steerSpeed = 10f;
	public float steerDampTime = 0.5f; // 500ms?
	public float bulletSpeed = 100f;
	public Transform[] bulletEmitters;

	public float health = 100f;

	private float steerVelocity = 0f; // For Mathf.SmoothDamp

	private new Rigidbody2D rigidbody;

	[HideInInspector]
	public event Action onDeath;

	void Awake() {
		rigidbody = GetComponent<Rigidbody2D>();
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
			float degrees = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg - 90f;
			rigidbody.rotation = Mathf.SmoothDampAngle(rigidbody.rotation, degrees, ref steerVelocity, steerDampTime, float.PositiveInfinity);
		}
		input *= moveSpeed * Time.deltaTime;
		rigidbody.position += input;
	}

	public void Fire() {
		foreach (var emitter in bulletEmitters) {
			var b = Instantiate(bullet, emitter.position, transform.rotation);
			b.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
			b.GetComponent<Projectile>().sender = gameObject;
		}
	}

	public void Damage(float intensity) {
		health -= intensity;

		if (health <= 0f)
			Kill();
	}

	public void Kill() {
		health = 0f;
		gameObject.SetActive(false);
		Debug.Log(name + " died");

		if (onDeath != null)
			onDeath();
	}

	public virtual void OnTriggerEnter2D(Collider2D other) {
		Projectile info = other.GetComponent<Projectile>();

		switch (other.tag) {
			case "Projectile":
				if (info == null) {
					Debug.LogWarning($"Collided with projectile without a Projectile component: {other.name}");
					Destroy(other.gameObject);
				} else if (info.sender != gameObject || info.lifetime > 1f) {
					Damage(info.damage);
					Destroy(other.gameObject);
				}

				break;
			case "Celestial":
				if (info == null)
					Kill();
				else
					Damage(info.damage);
				break;
		}
	}
}