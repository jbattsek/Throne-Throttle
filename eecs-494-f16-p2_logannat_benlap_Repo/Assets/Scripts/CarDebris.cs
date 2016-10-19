using UnityEngine;
using System.Collections;

public class CarDebris : MonoBehaviour {

	public Material[] 	CarColors;
	public GameObject[] Debris;

	// Use this for initialization
	void Start () {
		Invoke ("Kill", 10f);
	}

	public void SetColor (int playerIndex)
	{
		for (int i = 0; i < Debris.Length; i++)
		{
		Debris[i].GetComponent<Renderer>().material = CarColors [playerIndex];
		}

	}

	void Kill()
	{
		Destroy (gameObject);
	}
		
}
