using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {

	private GameObject weaponHolder;
	private GameObject scoreManager;

	public void SetWeaponHolder(GameObject obj) {
		weaponHolder = obj; 
	}

	public GameObject GetWeaponHolder() {
		return scoreManager;
	}

	public void SetScoreManager(GameObject obj) {
		scoreManager = obj; 
	}

	public GameObject GetScoreManager() {
		return weaponHolder;
	}


	void OnCollisionEnter(Collision col) {
		GameObject hit = col.gameObject;

		if(hit.name != "WeaponHolder") {
			weaponHolder.SetActive(true);
		}


		if (hit.name == "Body" || hit.name == "Eyeball") {
			fireCollisionFx(hit);
			stick2Object (hit.GetComponent<Transform>().parent.parent.gameObject);
			updateScore(hit);
		}

		if (hit.name == "TargetPlate") {
			stick2Object (hit.GetComponent<Transform>().parent.gameObject);
		}

		if (hit.name == "Weapon") {
		}

		if(WeaponManager.Instance().IsGameFinished) {
			finishGame();
		}

	}

	private void stick2Object(GameObject hit) {
		// Disable all physics on the colliding object.
		GetComponent<Rigidbody>().isKinematic = true;
		BoxCollider bc = GetComponent<BoxCollider>();
		Destroy(bc);

		// To solve the scaling issue when parenting
		// the colliding object, adding a dummy game
		// object as a parent of the colliding object.
		GameObject dummyParent = new GameObject();
		transform.parent = dummyParent.GetComponent<Transform>();

		dummyParent.GetComponent<Transform>().parent = hit.GetComponent<Transform>();
	}
		
	private void fireCollisionFx(GameObject hit) {
		GameObject targetGroup = hit.GetComponent<Transform>().parent.gameObject;
		Transform collisionFxHolder = targetGroup.GetComponent<Transform>().Find("CollisionFX");

		collisionFxHolder.position = transform.position;

		collisionFxHolder.gameObject.SetActive(false);
		collisionFxHolder.gameObject.SetActive(true);

	}

	// TODO: Score should be updated according to each part of body.
	private void updateScore(GameObject target) {
		scoreManager.GetComponent<ScoreManager>().UpdateScore(ScoreManager.HitType.HIT_SIMPLE);
	}

	private void finishGame() {
		DialogWindow finishWindow = DialogWindow.Instance();
		string infoText = "Do you want to play again?";
		finishWindow.Display(infoText, SceneHandler.LoadPlayScene, SceneHandler.LoadStartScene);
	}
}
