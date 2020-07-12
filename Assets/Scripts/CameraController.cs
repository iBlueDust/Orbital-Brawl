using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
	public float dampTime = 0.2f;
	public float screenEdgeBuffer = 4f;
	public float minSize = 6.5f;
	[HideInInspector] public LinkedList<GameObject> targets;


	private new Camera camera;
	private float zoomSpeed;
	private Vector3 moveVelocity;
	private Vector3 desiredPosition;


	private void Awake() {
		camera = GetComponent<Camera>();
		targets = new LinkedList<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
	}


	private void LateUpdate() {
		Move();
		Zoom();
	}


	private void Move() {
		FindAveragePosition();

		transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime, float.PositiveInfinity, Time.deltaTime);
	}


	private void FindAveragePosition() {
		Vector3 averagePos = new Vector3();
		int numTargets = 0;

		for (var node = targets.First; node != null; node = node.Next) {
			var target = node.Value;

			if (target == null) {
				targets.Remove(node);
				continue;
			}

			averagePos += target.transform.position;
			numTargets++;
		}

		if (numTargets > 0) {
			averagePos /= numTargets;
			averagePos.z = transform.position.z;
			desiredPosition = averagePos;
		}
	}


	private void Zoom() {
		float requiredSize = FindRequiredSize();
		camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, requiredSize, ref zoomSpeed, dampTime);
	}


	private float FindRequiredSize() {
		Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPosition);

		float size = 0f;

		for (var node = targets.First; node != null; node = node.Next) {
			var target = node.Value;

			if (target == null) {
				targets.Remove(node);
				continue;
			}

			Vector3 targetLocalPos = transform.InverseTransformPoint(target.transform.position);

			Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

			size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
			size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / camera.aspect);
		}

		size += screenEdgeBuffer;

		size = Mathf.Max(size, minSize);

		return size;
	}


	public void SetStartPositionAndSize() {
		FindAveragePosition();

		transform.position = desiredPosition;

		camera.orthographicSize = FindRequiredSize();
	}
}