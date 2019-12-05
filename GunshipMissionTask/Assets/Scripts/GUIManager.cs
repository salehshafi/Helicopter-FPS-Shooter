using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

	private static GUIManager instance;
	public static GUIManager Instance
	{
		get
		{
			return instance;
		}
		
	}
	
	void Awake()
	{
		instance = this;
	}

	public GameObject mainMenuObj;
	public GameObject emptyMenuObj;
	public GameObject pauseMenuObj;
	public GameObject gameMenuObj;
	public GameObject gameOverMenuObj;
	public GameObject infoMenuObj;
	public GameObject quitMenuObj;

	public GameObject crossHairs;

	public enum mainstates {MAIN,GAME,EMPTYMENU,PAUSE,INFO,GAMEOVER,QUIT};

	public mainstates currentState;

	public HelicopterController hcController;
	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsScript;

	public Text gameOverScore;
	public Text gameScore;

	public Texture2D cursor;
	// Use this for initialization
	void Start () {

		Cursor.SetCursor (cursor, Vector2.zero, CursorMode.ForceSoftware);

		currentState = mainstates.MAIN;
		fpsScript.enabled = false;
		if (PlayerPrefs.GetInt ("restart", 0) == 1) 
		{
			PlayerPrefs.SetInt ("restart", 0);
			PlayerPrefs.Save();
			StartMission ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentState == mainstates.MAIN && mainMenuObj.activeSelf == false) 
		{
			Time.timeScale=1;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			mainMenuObj.SetActive(true);
			emptyMenuObj.SetActive(false);
			pauseMenuObj.SetActive(false);
			gameMenuObj.SetActive(false);
			gameOverMenuObj.SetActive(false);
			infoMenuObj.SetActive(false);
			quitMenuObj.SetActive(false);
		}

		else if (currentState == mainstates.EMPTYMENU && emptyMenuObj.activeSelf == false) 
		{
			Time.timeScale=1;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			mainMenuObj.SetActive(false);
			emptyMenuObj.SetActive(true);
			pauseMenuObj.SetActive(false);
			gameMenuObj.SetActive(false);
			gameOverMenuObj.SetActive(false);
			infoMenuObj.SetActive(false);
			quitMenuObj.SetActive(false);
		}

		else if (currentState == mainstates.PAUSE && pauseMenuObj.activeSelf == false) 
		{
			Time.timeScale=0;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			mainMenuObj.SetActive(false);
			emptyMenuObj.SetActive(false);
			pauseMenuObj.SetActive(true);
			gameMenuObj.SetActive(false);
			gameOverMenuObj.SetActive(false);
			infoMenuObj.SetActive(false);
			quitMenuObj.SetActive(false);
		}

		else if (currentState == mainstates.GAME && gameMenuObj.activeSelf == false) 
		{
			Time.timeScale=1;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			fpsScript.enabled = true;
			mainMenuObj.SetActive(false);
			emptyMenuObj.SetActive(false);
			pauseMenuObj.SetActive(false);
			gameMenuObj.SetActive(true);
			gameOverMenuObj.SetActive(false);
			infoMenuObj.SetActive(false);
			quitMenuObj.SetActive(false);
		}
	
		else if (currentState == mainstates.GAMEOVER && gameOverMenuObj.activeSelf == false) 
		{
			Time.timeScale=1;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			fpsScript.enabled = true;
			mainMenuObj.SetActive(false);
			emptyMenuObj.SetActive(false);
			pauseMenuObj.SetActive(false);
			gameMenuObj.SetActive(false);
			gameOverMenuObj.SetActive(true);
			infoMenuObj.SetActive(false);
			quitMenuObj.SetActive(false);
		}
		else if (currentState == mainstates.INFO && infoMenuObj.activeSelf == false) 
		{
			Time.timeScale=1;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			fpsScript.enabled = true;
			mainMenuObj.SetActive(false);
			emptyMenuObj.SetActive(false);
			pauseMenuObj.SetActive(false);
			gameMenuObj.SetActive(false);
			gameOverMenuObj.SetActive(false);
			infoMenuObj.SetActive(true);
			quitMenuObj.SetActive(false);
		}

		else if (currentState == mainstates.QUIT && quitMenuObj.activeSelf == false) 
		{
			Time.timeScale=1;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			fpsScript.enabled = true;
			mainMenuObj.SetActive(false);
			emptyMenuObj.SetActive(false);
			pauseMenuObj.SetActive(false);
			gameMenuObj.SetActive(false);
			gameOverMenuObj.SetActive(false);
			infoMenuObj.SetActive(false);
			quitMenuObj.SetActive(true);
		}

		if ( Input.GetKeyUp (KeyCode.Escape)) 
		{
			if(currentState==mainstates.GAME)
			{
				currentState = mainstates.PAUSE;
				hcController.GetComponent<AudioSource> ().Stop ();
				GetComponent<AudioSource> ().volume=1;
			}
			else if(currentState==mainstates.PAUSE)
				ResumeMission();

			else if(currentState==mainstates.MAIN)
				currentState = mainstates.QUIT;

			else if(currentState==mainstates.QUIT)
				currentState = mainstates.MAIN;

			else if(currentState==mainstates.INFO)
				currentState = mainstates.MAIN;
		}

		if (currentState==mainstates.GAME && Input.GetKeyUp (KeyCode.C) && CameraSwitch.Instance!=null) 
		{
			CameraSwitch.Instance.NextCamera();
		}

		gameScore.text = "score: " + ScoreManager.Instance.score + "/" + ScoreManager.Instance.maxScore;
		gameOverScore.text = "score: " + ScoreManager.Instance.score + "/" + ScoreManager.Instance.maxScore;
	}

	public void StartMission()
	{
		currentState = mainstates.EMPTYMENU;
		//MultipleCameraSwitchScript.Instance.TurnOnCameraNumber (2);
		MultipleCameraSwitchScript.Instance.menuCamera.transform.parent.gameObject.GetComponent<RotateAccelerate> ().enabled = false;
		hcController.StartElevation ();
		hcController.GetComponent<AudioSource> ().Play ();
		GetComponent<AudioSource> ().volume=0.5f;

	}

	public void MainMenu()
	{

		MultipleCameraSwitchScript.Instance.TurnOnCameraNumber (0);
		fpsScript.enabled = true;
		Application.LoadLevel (Application.loadedLevel);
		
	}

	public void RestartMission()
	{
		PlayerPrefs.SetInt ("restart", 1);
		PlayerPrefs.Save ();

		MultipleCameraSwitchScript.Instance.TurnOnCameraNumber (0);
		fpsScript.enabled = true;
		Application.LoadLevel (Application.loadedLevel);

	}

	public void ResumeMission()
	{
		currentState = mainstates.GAME;
		hcController.GetComponent<AudioSource> ().Play ();
		GetComponent<AudioSource> ().volume=0.5f;
	}

	public void InfoMenu()
	{
		currentState = mainstates.INFO;
	}

	public void BackToMainMenu()
	{
		
		currentState = mainstates.MAIN;

		
	}

	public void QUIT()
	{
		
		currentState = mainstates.QUIT;
		
		
	}

	
	public void QUITYes()
	{
		
		Application.Quit ();
		
		
	}

	
	public void QUITNo()
	{
		
		currentState = mainstates.MAIN;
		
		
	}


	public void GameOver()
	{
		fpsScript.enabled = false;
		MultipleCameraSwitchScript.Instance.TurnOnMenuCamera();
		MultipleCameraSwitchScript.Instance.menuCamera.transform.parent.GetComponent<RotateAccelerate>().enabled=true;
		currentState = mainstates.GAMEOVER;
	}
}
