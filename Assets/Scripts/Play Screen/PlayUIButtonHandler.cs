using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class PlayUIButtonHandler : MonoBehaviour {

	public GameObject PanelSettings;

	private SettingsManager settingsManager;

	void Awake() {
		settingsManager = new SettingsManager(PanelSettings);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if(DialogWindow.Instance().isDialogWindowActive()) {
				DialogWindow.Instance().Close();
				if(WeaponManager.Instance().IsGameFinished) {
					goBack();
				}
			} else {
				showQuitWindow();
			}

		}
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
		
	private void goBack() {
		SceneHandler.LoadStartScene();
	}

	private void showQuitWindow() {
		DialogWindow dialogWindow = DialogWindow.Instance();

		if (!dialogWindow.dialogWindowObject.activeSelf) {
			string infoText = "Do you want to quit?";
			dialogWindow.Display(infoText, goBack, emptyMethod);
		} else {
			goBack();
		}
	}

	private void emptyMethod() {
	}
}
