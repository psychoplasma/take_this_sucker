using UnityEngine;
using System.Collections;

public class SettingsButtonHandler : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			goBack();
		}
	}

	public void ButtonBackHandler() {
		goBack();
	}

	public void ButtonTargetHandler() {
		
	}

	private void goBack() {
		SceneHandler.LoadStartScene();
	}
}
