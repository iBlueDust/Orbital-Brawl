﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GravityBody))]
public class ShipController : MonoBehaviour {
	public GameObject bullet;

	[Header("High Health")]
	public float moveSpeed = 10f;
	public float steerDampTime = 0.5f;

	[Header("Low Health")]
	public float thrustForce = 1f;
	public float steerForce = 50f;

	[Header("Other Configs")]
	public float bulletSpeed = 100f;
	public Transform[] bulletEmitters;

	public float maxHealth = 100f;
	public float health = 100f;
	[Range(0f, 1f)]
	public float switchControlsThreshold = 0.5f;

	private float desiredRotation = 0f;
	private float steerVelocity = 0f; // For Mathf.SmoothDamp

	private new Rigidbody2D rigidbody;
	private GravityBody gravityBody;

	[HideInInspector]
	public event Action onDeath;

	private float inputWeight {
		get => Mathf.InverseLerp(0.2f, 1f, health / maxHealth);
	}

	/// <summary>How much physics should play a role in the ship's maneuverability</summary>
	private float physicsWeight {
		get => 1 - inputWeight;
	}

	void Awake() {
		rigidbody = GetComponent<Rigidbody2D>();
		gravityBody = GetComponent<GravityBody>();
	}

	// input should already be normalized (since Controls already normalizes it)
	public void Move(Vector2 input) {
		float initRotation = rigidbody.rotation;

		if (input.x != 0f || input.y != 0f) {
			// Unity uses degrees for transforms; In Mathf.Atan2, 0deg = Right
			desiredRotation = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg - 90f;
		}
		rigidbody.rotation = Mathf.SmoothDampAngle(rigidbody.rotation, desiredRotation, ref steerVelocity, steerDampTime / inputWeight, float.MaxValue * inputWeight);

		var movement = input * moveSpeed * inputWeight * Time.deltaTime;
		// Sin and Cos are switched since Unity rotations face up when zero, in trigonometry, they face right when zero
		rigidbody.position += movement;
		// new Vector2(
		// 	Mathf.Sin(rigidbody.rotation * Mathf.Deg2Rad) * movement.x, -Mathf.Cos(rigidbody.rotation * Mathf.Deg2Rad) * movement.y
		// );


		// Introduce Physics
		rigidbody.AddRelativeForce(Vector2.up * Mathf.Max(0, input.y) * physicsWeight * thrustForce * Time.deltaTime * rigidbody.mass);
		rigidbody.AddTorque(-input.x * physicsWeight * steerForce * Time.deltaTime * rigidbody.mass);
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

		gravityBody.gravityWeight = physicsWeight;
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