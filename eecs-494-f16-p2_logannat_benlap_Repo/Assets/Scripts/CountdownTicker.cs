using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class CountdownTicker : MonoBehaviour {

	public Text T;
	public int Timer = 3;
	private int shakeIndex = 0;

	// Use this for initialization
	void Start ()
	{
		GameLogic.S.countDown.clip = GameLogic.S.countDownSounds[Timer];
		GameLogic.S.countDown.Play();
		StartCoroutine(ShakePlayer(shakeIndex));

		Invoke("Tick", 1f);
	}
	
	void Tick()
	{
		if (!enabled) {
			return;
		}
		GameLogic.S.countDown.clip = GameLogic.S.countDownSounds[Timer - 1];
		GameLogic.S.countDown.Play();
		shakeIndex++;
		if (shakeIndex < GameLogic.S.numPlayers)
		{
			StartCoroutine(ShakePlayer(shakeIndex));
		}

		Timer--;
		T.text = Timer.ToString();
		if (Timer > 0) {
			Invoke("Tick", 1f);
		} else {
			T.text = "GO!";
			Invoke ("Disappear", .5f);
		}
	}
	void Disappear()
	{
		T.enabled = false;
	}
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ShakePlayer (int index)
	{
		GameLogic.S.cars[index].car.damageVibrating = true;
		var controller = InputManager.Devices[index];
		controller.Vibrate(10);
		yield return new WaitForSeconds(1);
		GameLogic.S.cars[index].car.damageVibrating = false;
		controller.Vibrate(0);
	}
}
