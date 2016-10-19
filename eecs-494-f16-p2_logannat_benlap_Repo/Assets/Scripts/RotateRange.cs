using UnityEngine;
using System.Collections;

public class RotateRange : MonoBehaviour {

	public float Range;
	public float Origin;
	public float Freq;


	void Update () {
		transform.eulerAngles = new Vector3 (0f,Origin + (Range * Mathf.Sin(Freq * Time.time)),0f);
	}
}
