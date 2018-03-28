using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterHolder : MonoBehaviour {

	public static LetterHolder instance;

	public SpriteRenderer[] Letters;

	public Sprite[] Cons;

	public Sprite[] Vowel;

	// Use this for initialization
	void Start () {
        Debug.Log("LetterHolder Script");

		instance = this;

		Letters = gameObject.GetComponentsInChildren<SpriteRenderer> ();

		Letters[0].sprite = Cons[18];
		Letters[1].sprite = Cons[22];
		Letters[2].sprite = Cons[2];
		Letters[3].sprite = Cons[9];
		Letters[4].sprite = Cons[3];
		Letters[5].sprite = Vowel[0];
		Letters[6].sprite = Vowel[13];
	}

	public Sprite GetRandom() {
		bool useCons = (Random.Range (0, 2) == 0);
		return (useCons) ? Cons [Random.Range (0, Cons.Length)] : Vowel [Random.Range (0, Vowel.Length)]; 
	}
	
	// Update is called once per frame
	void Update () {
	}
}
