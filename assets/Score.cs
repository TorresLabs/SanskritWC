using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static Score instance;

	int score = 0;
	Text scoreText;

	// Use this for initialization
	void Start () {
		instance = this;

		scoreText = GetComponent<Text> ();
		AddToScore (0);
	}

	public void AddToScore(string name) {
		if (name.ToLower ().Contains ("empty")) {
			AddToScore (1);
		} else if (name.ToLower ().Contains ("2")) {
			AddToScore (2);
		} else if (name.ToLower ().Contains ("3")) {
			AddToScore (3);
		}
	}

	public void AddToScore(int value) {
		score += value;
		scoreText.text = "Points scored: "+score.ToString();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
