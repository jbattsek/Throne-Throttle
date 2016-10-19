using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class car_status : MonoBehaviour {

	public int damagePercentage;
	public int leaderboardPosition;

	public bool hasPowerup;
	public int powerNum;
	public int score;
	public int playerNum;

	public bool isLeader;
	public control_car car;
	public bool canTakeDamage = true;
	public bool doubleDamage;
	public float impulseAmount = 10.0f;
	public float invinsibleTime;
	public string power;
	public bool invincible = false;

	public GameObject oilSpill;
	public GameObject snowBall;
	public int numBalls;
	public GameObject bomb;
	public GameObject sword;
	public GameObject forcepush;


	public Sprite infinity;
	public Sprite startingSprite;

	private InputDevice inputDevice; 

	public Sprite[] powerImages;

	public bool canShootSnowball = true;
	public Material metallic;
	public Material original;

	public AudioSource sounds;

    void Awake()
    {
		if (car.playerNum >= GameLogic.S.numPlayers) {
			this.gameObject.SetActive (false);
		} else {
			GameLogic.S.cars [car.playerNum] = GetComponent<car_status> ();
		}
    }

	// Use this for initialization
	void Start () {
		playerNum = this.gameObject.GetComponent<control_car> ().playerNum;
		if (InputManager.Devices.Count >= playerNum && playerNum < GameLogic.S.numPlayers)
		{
			inputDevice = InputManager.Devices[playerNum];
		}
		else
		{
			gameObject.SetActive(false);
		}
		powerNum = 0;
		score = 0;
		damagePercentage = 0;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (invincible) {
			this.transform.GetChild (10).gameObject.SetActive (true);
		} else {
			this.transform.GetChild (10).gameObject.SetActive (false);
		}
		if (Mathf.Abs(this.transform.position.y) > 100 || Mathf.Abs(this.transform.position.x) > 300 || Mathf.Abs(this.transform.position.z) > 300) {
			GameLogic.S.cars [playerNum].canTakeDamage = true;
			GameLogic.S.cars [playerNum].damagePercentage = 0;
			this.score -= 20;
			DeathManager.Instance.KillPlayer (playerNum, true);
		}
		if (inputDevice.Action2.WasPressed ) {
			if (this.hasPowerup) {
				usePower (powerNum);
			}
		}

		if (powerNum == 1) {
			power = "health";
	
		} else if (powerNum == 2) {
			power = "force";
		}
		else if (powerNum == 3) {
			power = "oil";
		} else if (powerNum == 4) {
			power = "freeze";
		} else if (powerNum == 6) {
			power = "boost";
		} else if (powerNum == 5){
			power = "kingslayer";
		}else if (powerNum == 7){
			power = "bomb";
		}else if (powerNum == 8){
			power = "big";
		}
		else if (powerNum == 9){
			power = "invinsible";
		}
		else{
			power = "none";
		}
		//Updates scores and damages
		if (this.gameObject.tag == "Car0") {
			UI.instance.scoreTextP0.text = "Score: " + score;
			UI.instance.healthTextP0.text = "Damage: " + damagePercentage + "% "+power;
			UI.instance.PowerUpP0.sprite = powerImages[powerNum];
			UI.instance.timerTextP0.text = "Time Left: " + (int)GameLogic.S.timeLimit;
		} else if (this.gameObject.tag == "Car1") {
			if (GameLogic.S.numPlayers == 2) {
				UI.instance.scoreTextP2.text = "Score: " + GameLogic.S.cars[1].score;
				UI.instance.healthTextP2.text = "Damage: " + damagePercentage + "% "+power;
				UI.instance.PowerUpP2.sprite = powerImages[powerNum];
				UI.instance.timerTextP2.text = "Time Left: " + (int)GameLogic.S.timeLimit;
			} else {
				UI.instance.scoreTextP1.text = "Score: " + score;
				UI.instance.healthTextP1.text = "Damage: " + damagePercentage + "% "+power;
				UI.instance.PowerUpP1.sprite = powerImages[powerNum];
				UI.instance.timerTextP1.text = "Time Left: " + (int)GameLogic.S.timeLimit;
			}

		} else if (this.gameObject.tag == "Car2") {
			UI.instance.scoreTextP2.text = "Score: " + score;
			UI.instance.healthTextP2.text = "Damage: " + damagePercentage + "% "+power;
			UI.instance.PowerUpP2.sprite = powerImages[powerNum];
			UI.instance.timerTextP2.text = "Time Left: " + (int)GameLogic.S.timeLimit;
		} else if (this.gameObject.tag == "Car3") {
			UI.instance.scoreTextP3.text = "Score: " + score;
			UI.instance.healthTextP3.text = "Damage: " + damagePercentage + "% " +power;
			UI.instance.PowerUpP3.sprite = powerImages[powerNum];
			UI.instance.timerTextP3.text = "Time Left: " + (int)GameLogic.S.timeLimit;
		}
		int numHigher = 0;
		for (int i = 0; i < GameLogic.S.numPlayers; i++) {
			if (GameLogic.S.cars [i].score > score) {
				numHigher += 1;
			}
		}
		leaderboardPosition = 1 + numHigher;

	}

	void usePower(int Num){
		

		//HEALTH POWER
		if (Num == 1) {
			damagePercentage = (damagePercentage > spawn_powerup.instance.healthRecovery) ? damagePercentage - spawn_powerup.instance.healthRecovery : 0;
			hasPowerup = false;
			powerNum = 0;
			sounds.clip = GameLogic.S.repair;
			sounds.Play();
		}
		//FORCE PUSH
		else if (Num == 9) {
			hasPowerup = false;
			powerNum = 0;

			sounds.clip = GameLogic.S.push;
			sounds.Play();

			Vector3 tempPos = this.gameObject.transform.position;
			GameObject forcePush = (GameObject) Instantiate (forcepush, tempPos, Quaternion.identity);
			forcePush.GetComponent<forcepush> ().spawnedBy = this.gameObject.tag;
		}
		//OIL SPILL
		else if (Num == 3) {
			hasPowerup = false;
			powerNum = 0;
			sounds.clip = GameLogic.S.oilDrop;
			sounds.Play();
			Vector3 tempPos = this.gameObject.transform.position + transform.forward * -1;
			tempPos.y = .2f;
			GameObject spill = (GameObject) Instantiate (oilSpill, tempPos, Quaternion.identity);

			spill.GetComponent<oil_spill> ().spawnedBy = this.gameObject.tag;


		} 
		//SnowBall
		else if (Num == 4) {
			if (canShootSnowball) {
				StartCoroutine (shootSnowball ());
			}
		} 
		//Homing range attack on king
		else if (Num == 5) {
			Vector3 tempPos = this.gameObject.transform.position;
			tempPos.y += 5f;
			GameObject kingSlayer = (GameObject) Instantiate (sword, tempPos, Quaternion.identity);

			sounds.clip = GameLogic.S.sword;
			sounds.Play();

			kingSlayer.GetComponent<Sword> ().spawnedBy = this.gameObject.tag;
			hasPowerup = false;
			powerNum = 0;
		} 
		//Unlimittied boost
		else if (Num == 6) {
			hasPowerup = false;
			powerNum = 0;
			this.gameObject.GetComponent<control_car> ().unlimittedBoost = true;
			sounds.clip = GameLogic.S.boost;
			sounds.Play();
			if (playerNum == 0) {
				UI.instance.boostBarP0.GetComponent<Image> ().sprite = infinity;
			} else if (playerNum == 1) {
				UI.instance.boostBarP1.GetComponent<Image> ().sprite = infinity;
			} else if (playerNum == 2) {
				UI.instance.boostBarP2.GetComponent<Image> ().sprite = infinity;
			} else if (playerNum == 3) {
				UI.instance.boostBarP3.GetComponent<Image> ().sprite = infinity;
			}
			Invoke ("endBoost", 10f);
		}//BOMB 
		else if (Num == 7) {
			hasPowerup = false;
			powerNum = 0;
			Vector3 tempPos = this.gameObject.transform.position + transform.forward * -7f;
			tempPos.y = 1.2f;
			GameObject nuke = (GameObject) Instantiate (bomb, tempPos, Quaternion.identity);
			nuke.GetComponent<Bomb> ().spawnedBy = this.gameObject.tag;
			
		} 
		//double damage/size
		else if (Num == 8) {
			doubleDamage = true;

			sounds.clip = GameLogic.S.metal;
			sounds.Play();

			this.GetComponent<Rigidbody> ().mass *= 10f;
			this.transform.GetChild (0).GetComponent<Renderer> ().material = metallic;
			this.transform.GetChild (1).GetComponent<Renderer> ().material = metallic;
			this.transform.GetChild (2).GetComponent<Renderer> ().material = metallic;
			Invoke ("Halfdamage", 5f);
			hasPowerup = false;
			powerNum = 0;
			
		} 
		//invinsible
		else if (Num == 2)
		{
			sounds.clip = GameLogic.S.invincible;
			sounds.Play();
			canTakeDamage = false;
			invincible = true;
			Invoke ("REvinsible", invinsibleTime);
			hasPowerup = false;
			powerNum = 0;
			
		}

	}

	void Halfdamage(){
		doubleDamage = false;
		this.transform.GetChild (0).GetComponent<Renderer> ().material = original;
		this.transform.GetChild (1).GetComponent<Renderer> ().material = original;
		this.transform.GetChild (2).GetComponent<Renderer> ().material = original;
		this.GetComponent<Rigidbody> ().mass *= 0.1f;
	}

	void REvinsible(){
		canTakeDamage = true;
		invincible = false;
	}

	void endBoost(){
		this.gameObject.GetComponent<control_car> ().unlimittedBoost = false;
		if (playerNum == 0) {
			UI.instance.boostBarP0.GetComponent<Image> ().sprite = startingSprite;
		} else if (playerNum == 1) {
			UI.instance.boostBarP1.GetComponent<Image> ().sprite = startingSprite;
		} else if (playerNum == 2) {
			UI.instance.boostBarP2.GetComponent<Image> ().sprite = startingSprite;
		} else if (playerNum == 3) {
			UI.instance.boostBarP3.GetComponent<Image> ().sprite = startingSprite;
		}
	}


	//WEAK: 1,2,3
	//MEDIUM: 4,5,6
	//STRONG: 7,8,9
	public void getPowerup(){
		sounds.clip = GameLogic.S.powerup;
		sounds.Play();

		hasPowerup = true;
		powerNum = Random.Range (1, 101);
		if (leaderboardPosition == 1) {
			if (powerNum >= 1 && powerNum < 75) {
				powerNum = Random.Range (1, 4);
			} else if (powerNum < 90) {
				powerNum = Random.Range (4, 7);
			} else {
				powerNum = Random.Range (7, 10);
			}
		} else if (leaderboardPosition == 2) {
			if (powerNum >= 1 && powerNum < 21) {
				powerNum = Random.Range (1, 4);
			} else if (powerNum < 81) {
				powerNum = Random.Range (4, 7);
			} else {
				powerNum = Random.Range (7, 10);
			}
		} else if (leaderboardPosition == 3) {
			if (powerNum >= 1 && powerNum < 11) {
				powerNum = Random.Range (1, 4);
			} else if (powerNum < 71) {
				powerNum = Random.Range (4, 7);
			} else {
				powerNum = Random.Range (7, 10);
			}
		} else {
			if (powerNum >= 1 && powerNum < 75) {
				powerNum = Random.Range (1, 4);
			} else if (powerNum < 90) {
				powerNum = Random.Range (4, 7);
			} else {
				powerNum = Random.Range (7, 10);
			}
		}
		if (powerNum == 4) {
			numBalls = 3;
		}



	}


	void OnTriggerEnter(Collider coll){
		float velocity = this.gameObject.GetComponent<Rigidbody> ().velocity.magnitude * 200000f;
		if (velocity > 15f) {
			velocity = 15f;
		}
		velocity = (int)velocity;

		if (velocity >= 1f) {
			if (coll.gameObject.tag == "Car0" && GameLogic.S.cars[0].GetComponent<car_status>().canTakeDamage) {
				StartCoroutine (takeDamage (0, velocity));
			} else if (coll.gameObject.tag == "Car1"  && GameLogic.S.cars[1].GetComponent<car_status>().canTakeDamage) {
				StartCoroutine (takeDamage (1, velocity));
			} else if (coll.gameObject.tag == "Car2"  && GameLogic.S.cars[2].GetComponent<car_status>().canTakeDamage) {
				StartCoroutine (takeDamage (2, velocity));
			} else if (coll.gameObject.tag == "Car3" && GameLogic.S.cars[3].GetComponent<car_status>().canTakeDamage) {
				StartCoroutine (takeDamage (3, velocity));
			}
		}
	}

	void OnTriggerStay(Collider coll) {
		if (coll.gameObject.tag == "KingPlatform") {
			if (this.gameObject.tag == "Car0") {
				king_of_the_hill.instance.carsInRing [0] = true;
			} else if (this.gameObject.tag == "Car1") {
				king_of_the_hill.instance.carsInRing [1] = true;
			} else if (this.gameObject.tag == "Car2") {
				king_of_the_hill.instance.carsInRing [2] = true;
			} else if (this.gameObject.tag == "Car3") {
				king_of_the_hill.instance.carsInRing [3] = true;
			}
		}
	}

	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "KingPlatform") {
			if (this.gameObject.tag == "Car0") {
				king_of_the_hill.instance.carsInRing [0] = false;
			} else if (this.gameObject.tag == "Car1") {
				king_of_the_hill.instance.carsInRing [1] = false;
			} else if (this.gameObject.tag == "Car2") {
				king_of_the_hill.instance.carsInRing [2] = false;
			} else if (this.gameObject.tag == "Car3") {
				king_of_the_hill.instance.carsInRing [3] = false;
			}
		} else if (coll.gameObject.tag == "Oil") {
			this.gameObject.GetComponent<control_car> ().inOil = false; 
		}

	}

	void OnCollisionEnter(Collision coll) {

	}
		
	public IEnumerator takeDamage(int index, float velocity)
	{
		car.damageVibrating = true;
		GameLogic.S.cars[index].car.damageVibrating = true;

		GameLogic.S.cars[index].inputDevice.Vibrate(10);
		inputDevice.Vibrate(10);

		GameLogic.S.cars[index].StopVibration();
		StopVibration();

		if (GameLogic.S.cars [index].canTakeDamage) {
			sounds.clip = GameLogic.S.collide;
			sounds.Play();
			GameLogic.S.cars [index].canTakeDamage = false;
			if (doubleDamage) {
				GameLogic.S.cars [index].damagePercentage += (int)(velocity*2);
			} else {
				GameLogic.S.cars [index].damagePercentage += (int)velocity;
			}
			GameLogic.S.cars [index].GetComponent<Rigidbody> ().AddForce
		    (transform.forward * velocity * GameLogic.S.cars [index].damagePercentage / 20, ForceMode.Impulse);
			if (GameLogic.S.cars [index].damagePercentage >= 70 && velocity >= 5) {
				GameLogic.S.cars [index].canTakeDamage = true;
				GameLogic.S.cars [index].damagePercentage = 0;
				this.score += 20;
				DeathManager.Instance.KillPlayer (index, false);
				if (GameLogic.S.cars [index].GetComponent<car_status> ().isLeader) {
					this.score += 10;
				}
			}
			yield return new WaitForSeconds (0.5f);

			GameLogic.S.cars [index].car.stopInput = false;

			yield return new WaitForSeconds (1.5f);
			GameLogic.S.cars [index].canTakeDamage = true;
		}
	}

	public void SwordHit(float damage)
	{
		StartCoroutine(takeDamage(playerNum, damage));
	}

	IEnumerator shootSnowball() {
		sounds.clip = GameLogic.S.snowball;
		sounds.Play();
		canShootSnowball = false;
		Vector3 tempPos = this.gameObject.transform.position + 9f * transform.forward;
		GameObject snow = (GameObject) Instantiate (snowBall, tempPos, Quaternion.identity);
		snow.GetComponent<snowball> ().spawnedBy = this.gameObject.tag;
		snow.GetComponent<snowball> ().direction = transform.forward;
		numBalls--;
		if (numBalls == 0) {
			hasPowerup = false;
			powerNum = 0;
		}

		yield return new WaitForSeconds (1);
		canShootSnowball = true;
	}

	void StopVibration()
	{
		Invoke("stopInvoke", .3f);
	}

	void stopInvoke()
	{
		inputDevice.Vibrate(0);
		car.damageVibrating = false;
	}

}
