using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class king_of_the_hill : MonoBehaviour {
	bool give_points = true;
	public bool [] carsInRing;
	public static king_of_the_hill instance;

	bool moveSpot = true;
	public GameObject arrow;
	public GameObject KingRing;
	public GameObject GoldCoin;
	public GameObject points;

	public int randomPosition = -1;

	public List<Vector3> spawnSpots = new List<Vector3>();

	public GameObject SpawnPointAnchor;

	int numberOfCarsInRing() {
		int number = 0;

		for (int i = 0; i < 4; ++i) {
			if (carsInRing [i]) {
				number++;
			}
		}

		return number;
	}

	// Use this for initialization
	void Start () {
		instance = this;

		if (SpawnPointAnchor) {
			foreach (Transform child in SpawnPointAnchor.transform)
			{
				spawnSpots.Add (child.position);
			}
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (moveSpot) {
			StartCoroutine(moveRing());
		}
	}

	void OnTriggerStay(Collider coll) {
		if (give_points && numberOfCarsInRing() == 1 && coll.gameObject.name != "bottomBody"
			&& coll.gameObject.tag != "GoldCoin") {
			StartCoroutine (gainPointsInRing (coll));
		}
	}

	IEnumerator moveRing() {
		moveSpot = false;
		int random = randomPosition;
		if (randomPosition == -1) {
			random = 0;
			randomPosition = 0;
		} else {
			while (randomPosition == random) {
				random = Random.Range (0, spawnSpots.Count);
			}
			randomPosition = random;
		}
		this.transform.position = spawnSpots [random];
		KingRing.transform.position = spawnSpots [random];
		Vector3 arrowPosition = arrow.transform.position;
		arrowPosition.y += 0.5f;
		arrow.transform.position = arrowPosition;
		Vector3 coinPosition = KingRing.transform.position;
		coinPosition.y += 5.0f;
		GameObject goldCoin = (GameObject)Instantiate (GoldCoin, coinPosition, Quaternion.identity);
		yield return new WaitForSeconds (10);
		Destroy (goldCoin.gameObject);
		moveSpot = true;
	}

	IEnumerator gainPointsInRing(Collider coll) {
		if (coll.gameObject.GetComponent<car_status>())
		{
			give_points = false;
			print(coll.gameObject.name);
			var carStat = coll.gameObject.GetComponent<car_status>();
			carStat.score += 10;
			carStat.sounds.clip = GameLogic.S.points;
			carStat.sounds.Play();
			Vector3 pos = coll.gameObject.transform.position;
			pos.y += 5f;
			Instantiate(points, pos, coll.gameObject.transform.rotation);
			yield return new WaitForSeconds(2);
			give_points = true;

		}
	}
}
