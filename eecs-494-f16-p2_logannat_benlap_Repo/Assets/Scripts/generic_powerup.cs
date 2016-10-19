using UnityEngine;
using System.Collections;

public class generic_powerup : MonoBehaviour {

	public int index; //index in the spawn_powerups array

	Vector3 position; 
	public float rotationRate;
	public float bobRate;
	float tempY;

	public bool deathDrop = false; //true if this powerup was dropped by a car that got destroyed

	// Use this for initialization
	void Awake() {
		index = -1;
		position = this.gameObject.transform.position;
		tempY = position.y;
		//determines if this powerup was spawned by map or by death
		for (int i = 0; i < spawn_powerup.totalPowerups; i++) {
			if (position == spawn_powerup.instance.spawnPositions [i]) {
				index = i;
				break;
			}
		}
	}

	void Start () {


	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (index == -1) {
			deathDrop = true;
		} else {
			deathDrop = false;
		}

		Move ();

	}

	void Move (){
		transform.Rotate(0, Time.deltaTime * rotationRate, 0, Space.World);
		Vector3 tempPos = transform.position;
		tempPos.y =  tempY + .2f * Mathf.Sin(bobRate*Time.time);
		transform.position = tempPos;

	}


	void OnTriggerEnter (Collider coll){
		if (coll.CompareTag ("Car0") || coll.CompareTag ("Car1") || coll.CompareTag ("Car2") || coll.CompareTag ("Car3")) {
			if (coll.GetComponent<car_status>()) {
				if (this.gameObject.tag == "GoldCoin")
				{
					var carStat = coll.GetComponent<car_status>();
					carStat.score += 10;
					carStat.sounds.clip = GameLogic.S.coin;
					carStat.sounds.Play();
					Destroy(this.gameObject);
				}
				else if (!coll.GetComponent<car_status>().hasPowerup)
				{
					coll.GetComponent<car_status>().getPowerup();
					if (deathDrop)
					{
						Destroy(this.gameObject);
					}
					else
					{
						//tell the map that a new powerup should spawn
						spawn_powerup.instance.setIndexFalse(index);
						Destroy(this.gameObject);
					}
				}
			}
		}
	}
}
