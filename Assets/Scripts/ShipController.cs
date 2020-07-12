using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GravityBody))]
[RequireComponent(typeof(Animator))]
public class ShipController : MonoBehaviour {
	public GameObject bullet;
	public GameObject deathParticles;

	[Header("High Health")]
	public float moveSpeed = 10f;
	public float steerDampTime = 0.5f;

	[Header("Low Health")]
	public float thrustForce = 1f;
	public float steerForce = 50f;

	[Header("Other Configs")]
	public float bulletSpeed = 100f;
	public Transform[] bulletEmitters;
	public float recoilForce = 2f;
	public float maxGravityResistance = 100f;

	public float maxHealth = 100f;
	public float health = 100f;
	public float damageCooldown = 1f;

	private float desiredRotation = 0f;
	private float steerVelocity = 0f; // For Mathf.SmoothDamp

	private new Rigidbody2D rigidbody;
	private GravityBody gravityBody;
	private Animator animator;

	// < current health, change in health >
	public event Action<float, float> onHealthChange = delegate { };
	public event Action onDeath = delegate { };

	private float inputWeight {
		// get => Mathf.InverseLerp(0.2f, 1f, health / maxHealth);
		get => health / maxHealth;
	}

	/// <summary>How much physics should play a role in the ship's maneuverability</summary>
	private float physicsWeight {
		get => 1 - inputWeight;
	}

	void Awake() {
		rigidbody = GetComponent<Rigidbody2D>();
		gravityBody = GetComponent<GravityBody>();
		animator = GetComponent<Animator>();

		gravityBody.gravityOffset = maxGravityResistance * inputWeight;
		onHealthChange += (_, __) => gravityBody.gravityOffset = maxGravityResistance * inputWeight;
	}

	// input should already be normalized (since Controls already normalizes it)
	public void Move(Vector2 input) {
		float initRotation = rigidbody.rotation;

		if (input.x != 0f || input.y != 0f) {
			// Unity uses degrees for transforms; In Mathf.Atan2, 0deg = Right
			desiredRotation = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg - 90f;
		}
		rigidbody.rotation = Mathf.SmoothDampAngle(rigidbody.rotation, desiredRotation, ref steerVelocity, steerDampTime / inputWeight, float.MaxValue * inputWeight, Time.deltaTime);

		var movement = input * moveSpeed * inputWeight * Time.deltaTime;
		rigidbody.position += movement;
		rigidbody.drag = inputWeight;
		rigidbody.angularDrag = inputWeight;

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

		// Recoil
		rigidbody.AddRelativeForce(Vector2.down * bulletEmitters.Length * recoilForce * physicsWeight, ForceMode2D.Impulse);
	}

	private float lastDamageTime = float.NegativeInfinity;
	public bool Damage(float intensity) {
		if (lastDamageTime + damageCooldown > Time.time)
			return false;

		lastDamageTime = Time.timeSinceLevelLoad;
		health -= intensity;

		if (health <= 0f)
			Kill();

		onHealthChange(health, intensity);
		animator.SetTrigger("Damage");
		return true;
	}

	public void Kill() {
		health = 0f;
		gameObject.SetActive(false);
		Instantiate(deathParticles, transform.position, Quaternion.identity);
		Debug.Log(name + " died");

		if (onDeath != null)
			onDeath();
	}

	/// Position is in global space
	public void Knockback(Projectile bullet) {
		Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

		// This is technically 2*energy but whatever. The force on impact is supposed to be E / d anyway. Let bullet.mass handle that
		var energy = bulletRB.velocity.normalized * bulletRB.velocity.sqrMagnitude * bullet.mass;
		rigidbody.AddForceAtPosition(energy, bulletRB.position);
	}

	public void Heal(float healPower) {
		health = Mathf.Min(health + healPower, maxHealth);
		onHealthChange(health, healPower);
		animator.SetTrigger("Heal");
	}

	public virtual void OnTriggerEnter2D(Collider2D other) {
		switch (other.tag) {
			case "Projectile":
				Projectile projectile = other.GetComponent<Projectile>();
				if (projectile == null) {
					Debug.LogWarning($"Collided with projectile without a Projectile component: {other.name}");
					Destroy(other.gameObject);
				} else if (projectile.sender != gameObject || projectile.lifetime > 1f) {
					Damage(projectile.damage);
					Knockback(projectile);
					Destroy(other.gameObject);
				}

				break;
			case "Celestial":
				// Asteroids are celestial but do not one-hit
				var asteroidInfo = other.GetComponent<Projectile>();
				if (asteroidInfo == null)
					Kill();
				else
					Damage(asteroidInfo.damage);
				break;
			case "Pickable":
				var info = other.GetComponent<HealCanister>();
				if (info != null)
					Heal(info.healPower);

				Destroy(other.gameObject);
				break;
		}
	}
}