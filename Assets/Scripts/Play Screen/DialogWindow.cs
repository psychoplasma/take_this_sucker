using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogWindow : MonoBehaviour {

	public Text info;
	public Image iconImage;
	public Button okButton;
	public Button cancelButton;
	public GameObject dialogWindowObject;

	private static DialogWindow dialogWindow;

	public static DialogWindow Instance() {
		if (!dialogWindow) {
			dialogWindow = FindObjectOfType(typeof (DialogWindow)) as DialogWindow;
			if (!dialogWindow)
				Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}

		return dialogWindow;
	}

	public void Display(string info, UnityAction okEvent, UnityAction cancelEvent) {
		dialogWindowObject.SetActive(true);
		suspendGame();

		okButton.onClick.RemoveAllListeners();
		okButton.onClick.AddListener(okEvent);
		okButton.onClick.AddListener(Close);

		cancelButton.onClick.RemoveAllListeners();
		cancelButton.onClick.AddListener(cancelEvent);
		cancelButton.onClick.AddListener(Close);

		this.info.text = info;
	}

	public void Close() {
		dialogWindowObject.SetActive(false);
		resumeGame();
	}

	public bool isDialogWindowActive() {
		return dialogWindowObject.activeSelf;
	}

	private void suspendGame() {
		Time.timeScale = 0;
	}

	private void resumeGame() {
		Time.timeScale = 1;
	}
}
