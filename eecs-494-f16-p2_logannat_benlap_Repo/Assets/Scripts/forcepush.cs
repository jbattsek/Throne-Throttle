using UnityEngine;
using System.Collections;

public class forcepush : MonoBehaviour {

	public string spawnedBy;

	void Start () {

		Invoke ("die", 5f);
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 scale = this.transform.localScale;
		scale.x += 5.0f * Time.deltaTime;
		scale.y += 5.0f * Time.deltaTime;
		scale.z += 5.0f * Time.deltaTime;
		this.transform.localScale = scale;
	}

	public void OnTriggerStay(Collider coll){

		if (coll.gameObject.tag != spawnedBy) {
			if (coll.gameObject.tag == "Car0") {
				GameLogic.S.cars [0].GetComponent<Rigidbody> ().AddForce (transform.forward * 3, ForceMode.Impulse);
			} else if (coll.gameObject.tag == "Car1") {
				GameLogic.S.cars [1].GetComponent<Rigidbody> ().AddForce (transform.forward * 3, ForceMode.Impulse);
			} else if (coll.gameObject.tag == "Car2") {
				GameLogic.S.cars [2].GetComponent<Rigidbody> ().AddForce (transform.forward * 3, ForceMode.Impulse);
			} else if (coll.gameObject.tag == "Car3") {
				GameLogic.S.cars [3].GetComponent<Rigidbody> ().AddForce (transform.forward * 3, ForceMode.Impulse);
			}

		}

	}

	void die(){
		//come back to this, need to keep array of everything in oil at given time, set to not in oil after 
		Destroy (this.gameObject);
	}
}
