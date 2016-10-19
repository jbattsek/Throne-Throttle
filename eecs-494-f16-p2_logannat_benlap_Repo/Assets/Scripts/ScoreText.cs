using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
	public Image OrangeL;
	public Image OrangeR;
	public Image BlueL;
	public Image BlueR;
	public Image GreenL;
	public Image GreenR;
	public Image RedL;
	public Image RedR;

	// Use this for initialization
	void Awake ()
    {
		//Left: (x = -100)
			//1: -54
			//2: -96.7
			//3: -136.9
			//4: -178
		//Right: (x = 100)

        var Text = GetComponent<Text>();

        if (name == "Winner")
        {
            Text.text = GameLogic.S.winnerText;
			Text.color = Color.red;
        }
        else
        {
           Text.text = GameLogic.S.scores;
        }
	}
}
