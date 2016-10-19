using UnityEngine;
using System.Collections;

public class where_to : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 lookAtPos = king_of_the_hill.instance.KingRing.transform.position;
		lookAtPos.y += 5f;
		this.transform.LookAt (lookAtPos);
		transform.RotateAround (transform.position, transform.up, 90f);
	}
}