using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AsteroidBelt : MonoBehaviour {
	public uint spawnCount = 300;
	[Tooltip("Minimum distance between the centerpoints of any two asteroids")]
	public float minDistance = .1f;
	public float minSize = .5f;
	public float maxSize = 3f;
	public float minRotateSpeed = .01f;
	public float maxRotateSpeed = .1f;
	public float minOrbitSpeed = 0.001f;
	public float maxOrbitSpeed = 0.001f;
	public float maxDamage = 50f;
	public float maxMass = 300f;

	public float spawnRangeWidth = 5f;
	public Transform center;
	public GameObject[] asteroids;


	private static readonly uint BATCH_SIZE = 100;
	private static readonly uint MAX_SAMPLES = 30;

	private float innerRadius;
	private float outerRadius;

	void Start() {
		innerRadius = (transform.position - center.position).magnitude;
		outerRadius = innerRadius + spawnRangeWidth;

		StartCoroutine(SpawnAsteroids());
	}

	// Poisson disc sampling
	private IEnumerator SpawnAsteroids() {
		float innerCircumference = Mathf.PI * 2 * innerRadius;

		// [sector, track]
		int sectorCount = Mathf.CeilToInt(innerCircumference / minDistance);
		int trackCount = Mathf.CeilToInt(spawnRangeWidth / minDistance);

		// Indices start from 1 in this grid since it initializes to zero
		// Zero is used here to indicate that there are no points in the gridtile.
		int[, ] grid = new int[sectorCount, trackCount];
		List<Vector2> spawnPoints = new List<Vector2>((int) spawnCount);

		// Instead of calling RNG to select an asteroid prefab, I'm just using an index
		// that is incremented for every sample and wraps around to not exceed asteroids.Length
		int prefabIndex = 0;

		uint asteroidsToSpawn = spawnCount;
		while (asteroidsToSpawn > 0) {
			// Spawn asteroids in batches of {BATCH_SIZE}
			for (int i = 0; i < BATCH_SIZE && asteroidsToSpawn > 0; i++) {
				for (int s = 0; s < MAX_SAMPLES; s++) {
					float angle = Random.Range(0f, Mathf.PI * 2);
					float radius = Random.Range(innerRadius, outerRadius);
					Vector2 position = PolarToCartesian(angle, radius);

					int currentSector = Mathf.FloorToInt((angle / (Mathf.PI * 2)) * innerCircumference / minDistance);
					int currentTrack = Mathf.FloorToInt((radius - innerRadius) / minDistance);

					int startSector = currentSector - 1;
					int startTrack = currentTrack - 1;
					// Out of bounds check
					if (startSector < 0) startSector = 0;
					if (startTrack < 0) startTrack = 0;

					// Check surrounding grid tiles
					for (int a = startSector; a <= startSector + 2 && a < sectorCount; a++) {
						for (int r = startTrack; r <= startTrack + 2 && r < trackCount; r++) {
							int index = grid[a, r] - 1;
							if (index >= 0 && (spawnPoints[index] - position).sqrMagnitude < minDistance * minDistance) {
								// Sample is INVALID
								goto invalidSpawnShortcut;
							}
						}
					}
					// Sample is valid

					// ALWAYS ADD BEFORE QUERYING LENGTH
					// We are using a indexing system that starts from 1 remember?
					spawnPoints.Add(position);
					grid[currentSector, currentTrack] = spawnPoints.Count;

					GameObject prefab = asteroids[prefabIndex];
					float rotation = Random.Range(0f, Mathf.PI * 2);
					var asteroid = Instantiate(prefab, position, Quaternion.AngleAxis(rotation, Vector3.forward), transform);
					var scale = Random.value;
					asteroid.transform.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, scale);

					var controller = asteroid.GetComponent<PlanetController>();
					controller.orbitCenter = center;
					controller.orbitSpeed = Random.Range(minOrbitSpeed, maxOrbitSpeed);
					controller.rotationSpeed = Random.Range(minRotateSpeed, maxRotateSpeed);

					asteroid.GetComponent<Projectile>().damage = scale * scale * maxDamage; // 2x the size means 4x the area
					asteroid.GetComponent<Rigidbody2D>().mass = scale * scale * maxMass; // same here

					// Don't mind me, taking care of asteroid prefab randomization
					prefabIndex = (prefabIndex + 1) % asteroids.Length;
					continue;

					// For invalid samples to break the check loop
					invalidSpawnShortcut : break;
				}

				asteroidsToSpawn--;
			}

			// Render next frame
			yield return null;
		}
	}


	private Vector2 PolarToCartesian(float angle, float radius) {
		return new Vector2(
			Mathf.Cos(angle) * radius,
			Mathf.Sin(angle) * radius
		);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(center.position, innerRadius);
		Gizmos.DrawWireSphere(center.position, outerRadius);
	}
}