using UnityEngine;
using System.Collections;

public class TargetPlateBehaviour : MonoBehaviour {

	public Transform targetPlate;
	public float rotationSpeed = 5f;

	void Start () {
		// Disable collision effect
		disableCollisionFx();
	}
	

	void Update () {
		targetPlate.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
	}

	void disableCollisionFx() {
		targetPlate.Find("Target").Find("CollisionFX").gameObject.SetActive(false);
	}
}
