using UnityEngine;
using System.Collections;

public class snowball : Projectile {



	
	public override void OnCollisionEnter(Collision coll){
		
		if (coll.gameObject.tag != spawnedBy) {
			if (coll.gameObject.tag == "Car0" ||
			    coll.gameObject.tag == "Car1" ||
			    coll.gameObject.tag == "Car2" ||
			    coll.gameObject.tag == "Car3") {
				coll.gameObject.GetComponent<control_car> ().frozen = true;
				var carStat = coll.gameObject.GetComponent<car_status>();
				carStat.sounds.clip = GameLogic.S.frozen;
				carStat.sounds.Play();

			}
			Destroy (this.gameObject);

		}

	}


}
