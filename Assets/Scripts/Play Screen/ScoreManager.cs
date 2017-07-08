using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text ScoreBar;
	public enum HitType {
		HIT_SIMPLE,
		HIT_MODERATE,
		HIT_HIGH,
		HIT_SUPER
	};

	private const int HIT_SIMPLE_VAL = 50;
	private const int HIT_MODERATE_VAL = 100;
	private const int HIT_HIGH_VAL = 200;
	private const int HIT_SUPER_VAL = 500;
	private const int HIT_LOW_TONE_VAL = 1000;
	private const int HIT_HIGH_TONE_VAL = 2000;

	private static int score;

	void Awake() {
		score = 0;
	}
		
	public void UpdateScore(HitType hitType) {
		switch (hitType) {
		case HitType.HIT_SIMPLE:
			score += HIT_SIMPLE_VAL;
			break;
		case HitType.HIT_MODERATE:
			score += HIT_MODERATE_VAL;
			break;
		case HitType.HIT_HIGH:
			score += HIT_HIGH_VAL;
			break;
		case HitType.HIT_SUPER:
			score += HIT_SUPER_VAL;
			break;
		}

		updateScoreBar();
	}

	void updateScoreBar() {
		ScoreBar.text = score.ToString();
	}
}
