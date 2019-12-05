using UnityEngine;
using System.Collections;

public class MultipleCameraSwitchScript : MonoBehaviour 
{
	private static MultipleCameraSwitchScript instance;
	public static MultipleCameraSwitchScript Instance
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

	public Camera [] gameCameras;
	public Camera menuCamera;
	public int selectedCamera;
	// Use this for initialization
	void Start () 
	{
		selectedCamera = 0;
		TurnOnCameraNumber (selectedCamera);
		TurnOnMenuCamera ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GUIManager.Instance.currentState == GUIManager.mainstates.GAME && Input.GetKeyUp(KeyCode.C))
			TurnOnNextCamera ();
	}

	public void TurnOnCameraNumber(int num)
	{
		selectedCamera = num;
		for(int i=0; i<gameCameras.Length;i++)
		{
			gameCameras[i].enabled=false;
		}
		gameCameras[selectedCamera].enabled=true;

		if (selectedCamera == 2)
			GUIManager.Instance.crossHairs.SetActive (false);
		else
			GUIManager.Instance.crossHairs.SetActive (true);
	}

	public void TurnOnNextCamera()
	{
		gameCameras [selectedCamera].enabled=false;
		if (selectedCamera < gameCameras.Length - 1)
			selectedCamera++;
		else
			selectedCamera = 0;
		gameCameras [selectedCamera].enabled=true;

		if (selectedCamera == 2)
			GUIManager.Instance.crossHairs.SetActive (false);
		else
			GUIManager.Instance.crossHairs.SetActive (true);
	}
	public void TurnOnMenuCamera()
	{
		for(int i=0; i<gameCameras.Length;i++)
		{
			gameCameras[i].enabled=false;
		}
		menuCamera.enabled = true;
	}

	public void TurnOffMenuCamera()
	{
		menuCamera.enabled = false;
	}
}
