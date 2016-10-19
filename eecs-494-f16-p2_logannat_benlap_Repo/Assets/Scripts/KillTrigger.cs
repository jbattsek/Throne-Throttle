using UnityEngine;
using System.Collections;

public class KillTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Car0") {
			DeathManager.Instance.KillPlayer(0, true);	
		}
		if (other.tag == "Car1") {
			DeathManager.Instance.KillPlayer(1, true);	
		}
		if (other.tag == "Car2") {
			DeathManager.Instance.KillPlayer(2, true);	
		}
		if (other.tag == "Car3") {
			DeathManager.Instance.KillPlayer(3, true);	
		}
	}

}
