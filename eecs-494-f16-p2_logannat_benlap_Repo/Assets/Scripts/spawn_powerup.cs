using UnityEngine;
using System.Collections;

//THIS SCRIPT SHOULD BE CONNECTED TO THE MAP, WILL KEEP TRACK OF HOW MANY POWERUPS ARE AVAILABLE AND SPAWN NEW ONES WHEN NECESSARY
public class spawn_powerup : MonoBehaviour {
	public static spawn_powerup instance; 

	public GameObject powerup; //powerup prefab
	public bool[] powerupsSpawned; //starting powerups, true if available
	public int currentlyAvailable; //number of powerups available on the map (not including death drops)
	public Vector3[] spawnPositions; //size must match powerupsSpawned
	public int coolDown; //time it takes for a powerup to respawn
	public int healthRecovery;
	public float oilLife;
	public float freezeTime;

	public static float totalPowerups; //total possible powerups on map, not including death drops

	// Use this for initialization
	void Awake(){
		instance = this;
		currentlyAvailable = powerupsSpawned.Length;
		totalPowerups = currentlyAvailable;
	}

	void Start () {
		print ("PRINT");
		print (totalPowerups);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		//determine if a default powerup needs to spawn, act accordingly
		currentlyAvailable = CountAvailable ();
		if (currentlyAvailable < totalPowerups) {
			spawnPowerup ();
		}
	}

	int CountAvailable(){
		int count = 0;
		for (int i = 0; i < totalPowerups; i++) {
			if (powerupsSpawned[i]) {
				count++;
			}
		}
		return count;
	}

	void spawnPowerup(){
		for (int i = 0; i < totalPowerups; i++) {
			if (!powerupsSpawned [i]) {
				
				powerupsSpawned [i] = true;
				Instantiate (powerup, spawnPositions [i], Quaternion.identity);
			}
		}
	}

	//called by a powerup when it gets picked up
	public void setIndexFalse (int index){
		//wait 'coolDown' seconds before settiing array value so that a new powerup spawns
		StartCoroutine(waitAndSet(index));
	}

	IEnumerator waitAndSet(int index){
		
		yield return new WaitForSeconds(coolDown);

		powerupsSpawned[index] = false;
	}

}
