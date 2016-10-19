using UnityEngine;
using System.Collections;


/// <summary>
/// Used to float a static object on a sin curve
/// </summary>
public class Float : MonoBehaviour {

	public float Amp;
	public float Frequency;
	
	public Vector3 Origin;

	// Use this for initialization
	void Awake () {
		Origin = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3(0f,Amp*Mathf.Sin(Frequency*Time.time),0f) + Origin;
	}
}
