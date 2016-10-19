using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class oil_spill : MonoBehaviour {

	bool[] dict = { false, false, false, false };

	public string spawnedBy;
	// Use this for initialization
	void Start () {
		
		Invoke ("die", spawn_powerup.instance.oilLife);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag != spawnedBy) {
			if ((coll.gameObject.tag == "Car0" ||
			    coll.gameObject.tag == "Car1" ||
			    coll.gameObject.tag == "Car2" ||
			    coll.gameObject.tag == "Car3") && coll.gameObject.GetComponent<car_status>()) {
				var carStat = coll.gameObject.GetComponent<car_status>();
				coll.gameObject.GetComponent<control_car> ().inOil = true;
				carStat.sounds.clip = GameLogic.S.oil;
				carStat.sounds.Play();



				dict [carStat.playerNum] = true;
			}
		}
	}
	void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag != spawnedBy) {
			if ((coll.gameObject.tag == "Car0" ||
				coll.gameObject.tag == "Car1" ||
				coll.gameObject.tag == "Car2" ||
				coll.gameObject.tag == "Car3") && coll.gameObject.GetComponent<control_car>()) {
				coll.gameObject.GetComponent<control_car> ().inOil = true;
				var carStat = coll.gameObject.GetComponent<car_status>();
				dict [carStat.playerNum] = true;

			}
		}
	}




	void die(){
		
		for (int i = 0; i < GameLogic.S.numPlayers; i++)
		{
			if(dict[i]){
				GameLogic.S.cars [i].GetComponent<control_car> ().inOil = false; 
			}	
		}
		Destroy (this.gameObject);
	}
}
