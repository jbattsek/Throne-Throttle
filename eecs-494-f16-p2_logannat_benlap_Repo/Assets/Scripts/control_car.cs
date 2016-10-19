using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class control_car : MonoBehaviour {

	//Public
    public Transform[] wheels;
    public float motorPower = 150.0f;
    public float maxTurn = 30.0f;
	public float carSpeed = 1.0f;
	public float boostAmount = 10.0f;
	public float boostFillSpeed = 10.0f;
	public float boostUseSpeed = 10.0f;
	public bool inOil;
	public bool unlimittedBoost;
	public bool frozen;
	public bool damageVibrating = false;
    public int playerNum;
    private InputDevice inputDevice;
	private int carIndex;

	public bool inputDisabled = true;

	public Image progress;
	public Image secondaryProgress;

	//Private
    float instantePower = 0.0f;
    float brake = 0.0f;
    float wheelTurn = 0.0f;
	bool canBoost = true;
	public bool stopInput = false;

	public GameObject ice;
	bool freezeStarted;
	GameObject destroyIce;

    Rigidbody myRigidbody;

	void Awake() {
		if (playerNum >= GameLogic.S.numPlayers) {
			this.gameObject.SetActive (false);
		}
		if (GameLogic.S.numPlayers == 2 && this.gameObject.tag == "Car1") {
			progress = secondaryProgress;
		}
	}

	// Use this for initialization
	void Start () {
		carIndex = playerNum;
        myRigidbody = this.gameObject.GetComponent<Rigidbody>();
        myRigidbody.centerOfMass = new Vector3(0, 0.0f, 0.0f);
		progress.fillAmount = 0;
		progress.type = Image.Type.Filled;
        
        if (InputManager.Devices.Count >= playerNum && playerNum < GameLogic.S.numPlayers)
        {
            inputDevice = InputManager.Devices[playerNum];
        }
        else
        {
            gameObject.SetActive(false);
        }
		freezeStarted = false;

		//For the countdown
		inputDisabled = true;
		Invoke ("EnableInput", 3.1f);

    }

	void EnableInput()
	{
		inputDisabled = false;
	}

    // Update is called once per frame
    void FixedUpdate() {
		if (!inputDisabled) {
			if (inOil) {
				//make go slow
				motorPower = 30f; 

			} else {
				motorPower = 150f;
			}
			if (frozen) {
				if (!freezeStarted) {
					freezeStarted = true;
					destroyIce = (GameObject)Instantiate (ice, this.gameObject.transform.GetChild (0).transform.position, Quaternion.identity);
					Invoke ("freeze", spawn_powerup.instance.freezeTime);
				} else {
					destroyIce.transform.position = this.gameObject.transform.GetChild (0).transform.position;
				}
				return;
			}

			if (unlimittedBoost) {
				progress.fillAmount = 1f;

			}

			if (progress.fillAmount >= 0.10) {
				canBoost = true;
			} else {
				canBoost = false;
			}
			progress.fillAmount += Time.deltaTime * boostFillSpeed;	
        
			if (!stopInput) {
				instantePower = inputDevice.LeftStickY * motorPower * Time.deltaTime;
				wheelTurn = inputDevice.LeftStickX * maxTurn;
			}
			if ((inputDevice.LeftStickX >= 0.90f || inputDevice.LeftStickX <= -0.90f) && Mathf.Abs (inputDevice.LeftStickY) <= 0.2) {
				instantePower = 0.8f * motorPower * Time.deltaTime;
			}
			brake = inputDevice.LeftBumper.IsPressed ? myRigidbody.mass * 0.1f : 0.0f;

			//turn collider
			getCollider (0).steerAngle = wheelTurn;
			getCollider (1).steerAngle = wheelTurn;
			//this.transform.eulerAngles = new Vector3 (0, 
			//getCollider (0).steerAngle, 
			//wheels [0].localEulerAngles.z);
			Vector3 rotation = this.transform.rotation.eulerAngles;
			rotation.y += getCollider (0).steerAngle * 0.03f;
			this.transform.rotation = Quaternion.Euler (rotation);
			if (inputDevice.RightBumper.IsPressed && canBoost && !stopInput) {
				instantePower = 1 * motorPower * Time.deltaTime; //Going forward
				progress.fillAmount -= Time.deltaTime * boostUseSpeed;
				this.gameObject.GetComponent<Rigidbody> ().AddForce (transform.forward * boostAmount, ForceMode.Impulse);
				if (!damageVibrating) {
					inputDevice.Vibrate (2.5f);
				}
			} else {
				if (!damageVibrating) {
					inputDevice.Vibrate (0);
				}
			}
 

			//turn wheels
			wheels [0].localEulerAngles = new Vector3 (wheels [0].localEulerAngles.x,
				getCollider (0).steerAngle - wheels [0].localEulerAngles.z + 90,
				wheels [0].localEulerAngles.z);
			wheels [1].localEulerAngles = new Vector3 (wheels [1].localEulerAngles.x,
				getCollider (1).steerAngle - wheels [1].localEulerAngles.z + 90,
				wheels [1].localEulerAngles.z);

			//spin wheels
			wheels [0].Rotate (0, getCollider (0).rpm / 60 * 360 * Time.deltaTime, 0);
			wheels [1].Rotate (0, getCollider (1).rpm / 60 * 360 * Time.deltaTime, 0);
			wheels [2].Rotate (0, getCollider (2).rpm / 60 * 360 * Time.deltaTime, 0);
			wheels [3].Rotate (0, getCollider (3).rpm / 60 * 360 * Time.deltaTime, 0);

			//brakes
			if (brake > 0.0f) {
				getCollider (0).brakeTorque = brake;
				getCollider (1).brakeTorque = brake;
				getCollider (2).brakeTorque = brake;
				getCollider (3).brakeTorque = brake;
				getCollider (2).motorTorque = 0.0f;
				getCollider (3).motorTorque = 0.0f;
				carSpeed = 0f;
			} else {
				getCollider (0).brakeTorque = 0.0f;
				getCollider (1).brakeTorque = 0.0f;
				getCollider (2).brakeTorque = 0.0f;
				getCollider (3).brakeTorque = 0.0f;
				getCollider (2).motorTorque = instantePower;
				getCollider (3).motorTorque = instantePower;
				carSpeed = instantePower * 0.20f;
			}

			//Move the car
			if (carSpeed != 0) {
				this.transform.position += transform.forward * carSpeed;
			}
		}
    }

	int getIndex() {
		if (this.tag == "Car0") {
			return 0;
		} else if (this.tag == "Car1") {
			return 1;
		} else if (this.tag == "Car2") {
			return 2;
		} else if (this.tag == "Car3") {
			return 3;
		} else {
			print ("ERROR! CAR TAG NOT SET!!");
			return -1;
		}
	}

    WheelCollider getCollider(int n)
    {
        return wheels[n].gameObject.GetComponent<WheelCollider>();
    }

	void freeze(){
		frozen = false;
		freezeStarted = false;
		Destroy (destroyIce.gameObject);
	}

}
