using UnityEngine;
using System.Collections;
using InControl;

public class CameraLook : MonoBehaviour
{
    public float rotationSpeed = 500f;
    public float maxRotation = 120f;
    public InputDevice inputDevice;
    public Camera cameraObject;
    public float cameraSnapTime = .1f;
    public float cameraDelta = 0;
	public control_car carCont;
    public Transform car;
	private int playerNum;

    void Start()
    {
		playerNum = carCont.playerNum;
        if (InputManager.Devices.Count >= playerNum && playerNum < GameLogic.S.numPlayers)
        {
            inputDevice = InputManager.Devices[playerNum];
        }
        else
        {
            gameObject.SetActive(false);
        }

        // Handles 2 player split screen
        switch (GameLogic.S.numPlayers)
        {
            case (1):
                cameraObject.rect = new Rect(0, 0, 1, 1);
                break;
            case (2):
                if (playerNum == 0)
                {
                    cameraObject.rect = new Rect(0, .5f, 1, .5f);
                }
                if (playerNum == 1)
                {
                    cameraObject.rect = new Rect(0, 0, 1, .5f);
                }
                break;
            case (3):
                if (playerNum == 0)
                {
                    cameraObject.rect = new Rect(0, .5f, .5f, .5f);
                }
                if (playerNum == 1)
                {
                    cameraObject.rect = new Rect(0.5f, 0.5f, .5f, .5f);
                }
                if (playerNum == 2)
                {
                    cameraObject.rect = new Rect(0, 0, .5f, .5f);
                }
                break;
            case (4):
                if (playerNum == 0)
                {
                    cameraObject.rect = new Rect(0, .5f, .5f, .5f);
                }
                if (playerNum == 1)
                {
                    cameraObject.rect = new Rect(0.5f, 0.5f, .5f, .5f);
                }
                if (playerNum == 2)
                {
                    cameraObject.rect = new Rect(0, 0, .5f, .5f);
                }
                if (playerNum == 3)
                {
                    cameraObject.rect = new Rect(0.5f, 0, .5f, .5f);
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cameraDelta += Time.deltaTime;
        if (cameraDelta >= cameraSnapTime && transform.rotation.y != car.rotation.y)
        {
            if(transform.localRotation.y > 0)
            {
                transform.Rotate(0, -(rotationSpeed * Time.deltaTime), 0);
            }
            if (transform.localRotation.y < 0)
            {
                transform.Rotate(0, (rotationSpeed * Time.deltaTime), 0);
            }
            if (transform.localRotation.y <= 4 && transform.localRotation.y >= 361)
            {
                transform.Rotate( 0, 0 , 0);
            }
        }
        if (inputDevice.RightStickX != 0)
        {
            var rotation = rotationSpeed * Time.deltaTime * inputDevice.RightStickX;

            bool isRotatedRight = transform.localRotation.eulerAngles.y >= maxRotation && transform.localRotation.eulerAngles.y <= 180;
            bool isRotatedLeft = transform.localRotation.eulerAngles.y <= 360 -maxRotation && transform.localRotation.eulerAngles.y >= 180;

            if ((transform.localRotation.eulerAngles.y < maxRotation) || (transform.localRotation.eulerAngles.y > 360-maxRotation ))
            {
                transform.Rotate(0, rotation, 0);
            }
            if (isRotatedRight && rotation < 0 )
            {
                transform.Rotate(0, rotation, 0);
            }
            if (isRotatedLeft && rotation > 0)
            {
                transform.Rotate(0, rotation, 0);
            }
            cameraDelta = 0;
        }
    }
}
