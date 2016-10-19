using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Vector3 direction;
	public string spawnedBy;
	public Vector3 start;
	public float speed;
	// Use this for initialization
	public virtual void Start () {
		start = this.gameObject.transform.position;
		Invoke ("Death", 10f);
	}

	// Update is called once per frame
	public virtual void FixedUpdate () {
		Move ();
	}

	public virtual void Move(){
		Vector3 temp = this.gameObject.transform.position;
		temp += direction * speed * Time.deltaTime;
		this.gameObject.transform.position = temp;

	}

	void Death() {
		Destroy (this.gameObject);
	}

	public virtual void OnCollisionEnter(Collision coll){}
}
