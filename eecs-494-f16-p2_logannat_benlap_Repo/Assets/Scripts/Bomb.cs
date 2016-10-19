using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public string spawnedBy;
	public float travelDistance;
	public float fireSpeed;
	GameObject fire;
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		print ("start");
		fire = this.gameObject.transform.GetChild (3).gameObject;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (travelDistance > 0f) {
			
			Vector3 firePos = fire.gameObject.transform.position;
			firePos.y -= fireSpeed * Time.deltaTime;
			fire.gameObject.transform.position = firePos;
			travelDistance -= fireSpeed * Time.fixedDeltaTime;
		} else {
			print ("stop");
			GameObject x =(GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
			x.GetComponent<explode> ().spawnedBy = spawnedBy;
			Destroy (this.gameObject);
		}
	}
}
