using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public float damage = 1f;
	public float maxLifetime = 60f;
	public float mass = 1f;
	public GameObject sender;

	[HideInInspector]
	public float lifetime = 0f;

	private void Update() {
		lifetime += Time.deltaTime;
		if (lifetime > maxLifetime)
			Destroy(gameObject);
	}
}