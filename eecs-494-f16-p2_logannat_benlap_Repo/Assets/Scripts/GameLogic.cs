using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public static GameLogic S; //Singleton
    public int numPlayers = 3;
    public car_status[] cars;
    public float timeLimit = 120f;
    private float resetTime;
    public string winnerText;
    public string scores;
    private bool isScoreScreen = false;
    public bool isInMatch = false;
	public AudioSource bgm;
	public AudioSource countDown;
    private float scoreDelay = 1f;
    private float scoreDelayPassed = 0f;

	public AudioClip wilhelm, repair, collide, coin, oil, frozen, 
		boost, explosion, points, newKing, invincible, snowball, sword, 
		push, metal, powerup, oilDrop;
	public AudioClip[] countDownSounds = new AudioClip[4];

	void Awake()
    {
        if (!S)
        {
            S = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {
		numPlayers = 0;
		resetTime = timeLimit;
		//SetCarInput (false);
    }

    void FixedUpdate()
    {
		if (isInMatch)
        {
			determineLeader ();
            if (timeLimit > 0)
            {
                timeLimit -= Time.deltaTime;
            }
            else
            {
                if (!isScoreScreen)
                {
                    EndMatch();
                }
            }
        }

        if (isScoreScreen)
        {
            scoreDelayPassed += Time.deltaTime;
            if (scoreDelayPassed >= scoreDelay && InputManager.ActiveDevice.AnyButton.WasPressed)
            {
                SceneManager.LoadScene("Scene_Menu_no_input");
                isScoreScreen = false;
            }

        }
    }

    public void StartMatch ()
    {
        SceneManager.LoadScene("Scene_0");
		cars = new car_status[numPlayers];
	
		Invoke ("DelayStartMatch", 3f);
	}

	public void DelayStartMatch()
	{
		isInMatch = true;
		timeLimit = resetTime;
		bgm.Play();
	}

    void EndMatch()
    {
		bgm.Stop();

		// Resets vibrations at end
		for (int i = 0; i < numPlayers; i ++ )
		{
			InputManager.Devices[i].Vibrate(0);
		}

        car_status winner = cars[0];
        int winnerNum = -1;
        scores = "Scores:\n";
        for (int i = 0; i < cars.Length; i++)
        {
            scores = scores + "Player " + (i + 1) + ": " + cars[i].score + "\n";
			if (cars[i].isLeader)
            {
                winner = cars[i];
                winnerNum = i;
            }
        }
		if (winnerNum != -1) {
			winnerText = "Player " + (winnerNum + 1) + " WINS";
		} else {
			winnerText = "DRAW";
		}
		isInMatch = false;
        isScoreScreen = true;
        SceneManager.LoadScene("Scene_Score");
		StartCoroutine(VibrateWinner(winnerNum));
	}

	void determineLeader (){
		int max = 0;
		int index = -1;
		int numTied = 0;
		for (int i = 0; i < numPlayers; i++) {
			if (cars [i].score > max) {
				max = cars [i].score;
				index = i;
				numTied = 0;
			} else if (cars [i].score == max) {
				numTied++;
			}
		}
		for (int i = 0; i < numPlayers; i++) {
			if (numTied == 0) {
				if (i == index) {
					if (!cars[i].isLeader)
					{
						cars[i].sounds.clip = newKing;
						cars[i].sounds.Play();
					}
					cars [i].isLeader = true;
					cars[i].gameObject.transform.GetChild(9).gameObject.SetActive(true);
				} else {
					cars [i].isLeader = false;
					cars[i].gameObject.transform.GetChild(9).gameObject.SetActive(false);
				}
			}
			else {
				cars [i].isLeader = false;
				cars[i].gameObject.transform.GetChild(9).gameObject.SetActive(false);
			}
		}
	}

	IEnumerator VibrateWinner(int winner)
	{
		yield return new WaitForSeconds(.5f);
		for (int i = 0; i < numPlayers; i++)
		{
			InputManager.Devices[i].Vibrate(0);
		}
		InputManager.Devices[winner].Vibrate(10);
		yield return new WaitForSeconds(5f);
		InputManager.Devices[winner].Vibrate(0);
	}
}
