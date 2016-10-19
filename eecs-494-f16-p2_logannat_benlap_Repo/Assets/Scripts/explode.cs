using UnityEngine;
using System.Collections;

public class explode : MonoBehaviour {
	public string spawnedBy;
	public float damage;
	public float life;
	// Use this for initialization
	void Start () {
		Invoke ("end", life);
	}

	void end(){
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag != spawnedBy && coll.gameObject.GetComponent<car_status>()) {
			if (coll.gameObject.tag == "Car0") {
				StartCoroutine (coll.gameObject.GetComponent<car_status>().takeDamage (0, damage));
			} else if (coll.gameObject.tag == "Car1") {
				StartCoroutine (coll.gameObject.GetComponent<car_status>().takeDamage (1, damage));
			} else if (coll.gameObject.tag == "Car2") {
				StartCoroutine (coll.gameObject.GetComponent<car_status>().takeDamage (2, damage));
			} else if (coll.gameObject.tag == "Car3") {
				StartCoroutine (coll.gameObject.GetComponent<car_status>().takeDamage (3, damage));
			}
		}
	}
}
