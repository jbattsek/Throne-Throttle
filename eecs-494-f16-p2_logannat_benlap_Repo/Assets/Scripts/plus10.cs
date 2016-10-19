using UnityEngine;
using System.Collections;

public class plus10 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("done", 2f);
	}
	void done(){
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 tempPos = transform.position;
		tempPos.y += 1f * Time.fixedDeltaTime;
		transform.position = tempPos;
	}
}
