using UnityEngine;
using System.Collections;

/// <summary>
/// Handles all player deaths and respawning
/// </summary>
public class DeathManager : MonoBehaviour {

	private static DeathManager _instance;
	public static DeathManager Instance
	{
		get {return _instance;}
	}

	public GameObject[] Car;

	public Vector3[] OriginSpawnPoints;
	public Quaternion[] OriginRotation;

	public Vector3[] MapSpawnPoints;

	public GameObject DebrisPrefab;

	public bool[]	WasRecentlyUsed;	//did a player just spawn here?

	public float RespawnTime;

	// Use this for initialization
	void Start () {
		_instance = this;
		for (int i = 0; i < Car.Length; i++) {
			OriginSpawnPoints [i] = Car[i].transform.position;
			OriginRotation[i] = Car[i].transform.rotation;
		}
	}
		

	public void ResetPlayerStats(int playerIndex)
	{
	}

	public void ResetPlayerTransform(int playerIndex)
	{
	}

	public void RespawnPlayer(int playerIndex)
	{
		ResetPlayerStats (playerIndex);
		ResetPlayerTransform (playerIndex);
		MovePlayerToOrigin (playerIndex);
	}
	public void MovePlayerToOrigin(int playerIndex)
	{
		Car[playerIndex].GetComponent<Rigidbody>().velocity = new Vector2(0f,0f);
		Car[playerIndex].transform.position = OriginSpawnPoints[playerIndex];
		Car[playerIndex].transform.rotation = OriginRotation[playerIndex];
		Vector3 position = Car [playerIndex].transform.position;
		position.y += 10;
		Car [playerIndex].transform.position = position;
	}

	public void KillPlayer(int playerIndex, bool isFall)
	{
		StartCoroutine (killPlayer (playerIndex, isFall));
	}

	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator killPlayer(int playerIndex, bool isFall) {
		GameLogic.S.cars [playerIndex].canTakeDamage = false;
		if (isFall)
		{
			GameLogic.S.cars[playerIndex].sounds.clip = GameLogic.S.wilhelm;
		}
		else
		{
			GameLogic.S.cars[playerIndex].sounds.clip = GameLogic.S.explosion;
			//create explosion
			var go = Instantiate(DebrisPrefab) as GameObject;
			go.transform.position = GameLogic.S.cars [playerIndex].transform.position;
			go.GetComponent<CarDebris> ().SetColor (playerIndex);
		}
		GameLogic.S.cars[playerIndex].sounds.Play();
		GameLogic.S.cars [playerIndex].score -= 20;
		if (GameLogic.S.cars [playerIndex].score < 0) {
			GameLogic.S.cars [playerIndex].score = 0;
		}
		MovePlayerToOrigin(playerIndex);
		GameLogic.S.cars [playerIndex].GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		GameLogic.S.cars [playerIndex].damagePercentage = 0;
		GameLogic.S.cars [playerIndex].hasPowerup = false;
		GameLogic.S.cars [playerIndex].powerNum = 0;
		GameLogic.S.cars [playerIndex].GetComponent<Rigidbody> ().velocity = Vector3.zero;
		yield return new WaitForSeconds (2);
		GameLogic.S.cars [playerIndex].canTakeDamage = true;
		GameLogic.S.cars [playerIndex].GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;

	}
}
