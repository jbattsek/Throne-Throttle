using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
	public Text scoreTextP0;
	public Text healthTextP0;
	public GameObject boostBarP0;
	public Image PowerUpP0;
	public Text timerTextP0;

	public Text scoreTextP1;
	public Text healthTextP1;
	public GameObject boostBarP1;
	public Image PowerUpP1;
	public Text timerTextP1;

	public Text scoreTextP2;
	public Text healthTextP2;
	public GameObject boostBarP2;
	public Image PowerUpP2;
	public Text timerTextP2;

	public Text scoreTextP3;
	public Text healthTextP3;
	public GameObject boostBarP3;
	public Image PowerUpP3;
	public Text timerTextP3;

	public GameObject countdownP0;
	public GameObject countdownP1;
	public GameObject countdownP2;
	public GameObject countdownP3;

	public GameObject centerCountdown;
	public GameObject centerCDP1;
	public GameObject centerCDP2;

	public static UI instance;

	// Use this for initialization
	void Start () {
		instance = this;

		if (GameLogic.S.numPlayers == 1) {
			//center countdown p1
			countdownP0.transform.position = centerCountdown.transform.position;
		}

		if (GameLogic.S.numPlayers == 2) {
			countdownP0.transform.position = centerCDP1.transform.position;
			countdownP1.transform.position = centerCDP2.transform.position;
		}

		if (GameLogic.S.numPlayers <= 2) {
			scoreTextP1.gameObject.SetActive (false);
			healthTextP1.gameObject.SetActive (false);
			boostBarP1.gameObject.SetActive (false);
			PowerUpP1.gameObject.SetActive (false);
			timerTextP1.gameObject.SetActive (false);
			countdownP1.SetActive (false);
			countdownP1.GetComponent<CountdownTicker> ().enabled = false;
		}
		if (GameLogic.S.numPlayers <= 1) {
			scoreTextP2.gameObject.SetActive (false);
			healthTextP2.gameObject.SetActive (false);
			boostBarP2.gameObject.SetActive (false);
			PowerUpP2.gameObject.SetActive (false);
			timerTextP2.gameObject.SetActive (false);
			countdownP2.SetActive (false);
			countdownP2.GetComponent<CountdownTicker> ().enabled = false;
		}
		if (GameLogic.S.numPlayers <= 3) {
			scoreTextP3.gameObject.SetActive (false);
			healthTextP3.gameObject.SetActive (false);
			boostBarP3.gameObject.SetActive (false);
			PowerUpP3.gameObject.SetActive (false);
			timerTextP3.gameObject.SetActive (false);
			countdownP3.SetActive (false);
			countdownP3.GetComponent<CountdownTicker> ().enabled = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
}
