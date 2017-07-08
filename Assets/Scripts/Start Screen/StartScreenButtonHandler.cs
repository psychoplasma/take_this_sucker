using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenButtonHandler : MonoBehaviour {

	public GameObject PanelSettings;
	public GameObject PanelStats;

	private SettingsManager settingsManager;

	void Awake() {
		settingsManager = new SettingsManager(PanelSettings);
	}
		
	public void ButtonPlayHandler() {
		SceneHandler.LoadPlayScene();
	}

	public void ButtonStatsHandler() {
		PanelStats.SetActive(true);
	}

	public void ButtonQuitHandler() {
		quitGame();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if(PanelStats.activeSelf) {
				PanelStats.SetActive(false);
			} else {
				quitGame();
			}
		}
	}

	public void ButtonStatsBackHandler() {
		PanelStats.SetActive(false);
	}

	public void ButtonSettingsHandler() {
		settingsManager.AnimateSettingsPanel();
	}

	public void ButtonVolumeHandler() {
		settingsManager.ButtonSoundHandler();
	}

	public void ButtonMusicHandler() {
		settingsManager.ButtonMusicHandler();
	}

	public void ButtonDiffHandler() {
		settingsManager.ButtonDiffHandler();
	}
		
	private void quitGame() {
		Application.Quit();
		Debug.Log("Application is going to quit!");
	}

}
