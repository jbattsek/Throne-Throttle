using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBars : MonoBehaviour {

	public Image[] Bars;
	public float[] Scores;
	public float TotalScore;
	// Use this for initialization
	void Start () {
		TotalScore = 0;
		for (int i = 0; i < Bars.Length; i++) {
			Bars [i].fillAmount = 0f;
		}
		for (int i = 0; i < GameLogic.S.numPlayers; i++) {
			
			Scores [i] = GameLogic.S.cars [i].score;
			TotalScore += GameLogic.S.cars [i].score;
		}


		for (int i = 0; i < GameLogic.S.numPlayers; i++) {
			Bars [i].fillAmount = Scores[i]/TotalScore;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
