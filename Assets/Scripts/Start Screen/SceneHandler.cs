using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

	static public string startScene = "StartScreen";
	static public string playScene = "PlayScreen";
	static public string statsScene = "StatsScreen";


	static public void LoadPlayScene() {
		SceneManager.LoadScene(playScene, LoadSceneMode.Single);
	}

	static public void LoadStatsScene() {
		SceneManager.LoadScene(statsScene, LoadSceneMode.Single);
	}

	static public void LoadStartScene() {
		SceneManager.LoadScene(startScene, LoadSceneMode.Single);
	}

}
