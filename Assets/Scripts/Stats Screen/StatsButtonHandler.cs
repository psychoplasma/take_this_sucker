using UnityEngine;
using System.Collections;

public class StatsButtonHandler : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			goBack();
		}
	}

	public void ButtonBackHandler() {
		goBack();
	}

	private void goBack() {
		SceneHandler.LoadStartScene();
	}
}
