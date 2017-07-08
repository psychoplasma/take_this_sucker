using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager {
	public static string SettingsVolume = "GAME_PREFERENCES_VOLUME";
	public static string SettingsMusic = "GAME_PREFERENCES_MUSIC";
	public static string SettingsDifficulty = "GAME_PREFERENCES_DIFFICULTY";

	public enum DiffState {
		EASY,
		MEDIUM,
		HARD
	};

	public bool IsSoundOn {
		get {return isSoundOn;}
		set {isSoundOn = value;}
	}
	public bool IsMusicOn {
		get {return isMusicOn;}
		set {isMusicOn = value;}
	}
	public DiffState Difficulty {
		get {return diffState;}
		set {diffState = value;}
	}
		
	private GameObject settingsPanel;
	private Animator settingsAnimator;
	private Button buttonSound;
	private Button buttonMusic;
	private Button buttonDiff;
	private bool isSettingsOpen = false;
	private bool isSoundOn = true;
	private bool isMusicOn = true;
	private  DiffState diffState = DiffState.EASY;

	public SettingsManager(GameObject panel) {
		settingsPanel = panel;
		settingsAnimator = panel.GetComponent<Animator>();
		Button[] buttons = panel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons) {
			switch(b.name) {
			case "ButtonVolume":
				buttonSound = b;
				break;
			case "ButtonMusic":
				buttonMusic = b;
				break;
			case "ButtonDiff":
				buttonDiff = b;
				break;
			}
		}
		readSettings();
		setSettings(buttonSound, buttonMusic, buttonDiff);

		Debug.Log("Sound: " + (isSoundOn ? "On" : "Off") + 
			" Music: " + (isMusicOn ? "On" : "Off") + 
			" Difficulty: " + diffState.ToString());
	}
		
	public void AnimateSettingsPanel() {
		if (!isSettingsOpen) {
			settingsPanelSlideIn();
		} else {
			settingsPanelSlideOut();
			saveSettings();
		}
	}
		
	public void ButtonSoundHandler() {
		if (isSoundOn) {
			turnOffSound();
			setButtonImage(buttonSound, "Button_Mute");
		} else {
			turnOnSound();
			setButtonImage(buttonSound, "Button_Volume");
		}
	}

	public void ButtonMusicHandler() {
		if (isMusicOn) {
			turnOffMusic ();
			setButtonImage (buttonMusic, "Button_MusicOff");
		} else {
			turnOnMusic ();
			setButtonImage (buttonMusic, "Button_Music");
		}
	}

	public void ButtonDiffHandler() {
		switch (diffState) {
		case DiffState.EASY:
			setButtonImage (buttonDiff, "Diff_Medium");
			setDifficulty(DiffState.MEDIUM);
			break;
		case DiffState.MEDIUM:
			setButtonImage (buttonDiff, "Diff_Hard");
			setDifficulty(DiffState.HARD);
			break;
		case DiffState.HARD:
			setButtonImage (buttonDiff, "Diff_Easy");
			setDifficulty(DiffState.EASY);
			break;
		}
	}


	private void settingsPanelSlideIn() {
		settingsPanel.SetActive(true);

		settingsAnimator.Play("Panel_Settings_SlideIn");
		isSettingsOpen = true;
	}

	private void settingsPanelSlideOut() {
		settingsAnimator.Play("Panel_Settings_SlideOut");
		isSettingsOpen = false;
	}

	private void setButtonImage(Button button, string resourceName) {
		Texture2D texture = Resources.Load(resourceName) as Texture2D;
		Sprite sprite = Sprite.Create(texture, new Rect(0,0,texture.width,texture.height), new Vector2(0.5f,0.5f));
		button.GetComponent<UnityEngine.UI.Image>().sprite = sprite;

	}

	private void saveSettings() {
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt(SettingsVolume, isSoundOn ? -1 : 0);
		PlayerPrefs.SetInt(SettingsMusic, isMusicOn ? -1 : 0);
		PlayerPrefs.SetString(SettingsDifficulty, diffState.ToString());
	}

	private void readSettings() {
		try {
			isSoundOn = PlayerPrefs.GetInt(SettingsVolume) == -1 ? true : false;
		} catch(PlayerPrefsException e) {
			isSoundOn = true;
			Debug.Log("There is no such preferences:" + e.Message);
		}

		try {
			isMusicOn = PlayerPrefs.GetInt(SettingsMusic) == -1 ? true : false;
		} catch(PlayerPrefsException e) {
			isMusicOn = true;
			Debug.Log("There is no such preferences:" + e.Message);
		}

		try {
			string diff = PlayerPrefs.GetString(SettingsDifficulty);
			switch(diff) {
				case "EASY":
					diffState = DiffState.EASY;
					break;
				case "MEDIUM":
					diffState = DiffState.MEDIUM;
					break;
				case "HARD":
					diffState = DiffState.HARD;
					break;
				default:
					diffState = DiffState.EASY;
					break;
			}
		} catch(PlayerPrefsException e) {
			diffState = DiffState.EASY;
			Debug.Log("There is no such preferences:" + e.Message);
		}
	}

	private void setSettings(Button buttonSound, Button buttonMusic, Button buttonDiff) {
		setStateButtonSound(buttonSound);
		setStateButtonMusic(buttonMusic);
		setStateButtonDiff(buttonDiff);
	}
		
	private void setStateButtonSound(Button b) {
		if (isSoundOn) {
			setButtonImage(b, "Button_Volume");
		} else {
			setButtonImage(b, "Button_Mute");
		}
	}

	private void setStateButtonMusic(Button b) {
		if (isMusicOn) {
			setButtonImage (b, "Button_Music");
		} else {
			setButtonImage (b, "Button_MusicOff");
		}
	}

	private void setStateButtonDiff(Button b) {
		switch (diffState) {
		case DiffState.EASY:
			setButtonImage (b, "Diff_Easy");
			break;
		case DiffState.MEDIUM:
			setButtonImage (b, "Diff_Medium");
			break;
		case DiffState.HARD:
			setButtonImage (b, "Diff_Hard");
			break;
		}
	}

	//TODO: Implement the methods
	private void turnOffSound() {
		isSoundOn = false;
	}

	private void turnOnSound() {
		isSoundOn = true;
	}

	private void turnOffMusic() {
		isMusicOn = false;
	}

	private void turnOnMusic() {
		isMusicOn = true;
	}

	private void setDifficulty(DiffState diff) {
		switch (diff) {
		case DiffState.EASY:
			break;
		case DiffState.MEDIUM:
			break;
		case DiffState.HARD:
			break;
		}
		diffState = diff;
	}
}
