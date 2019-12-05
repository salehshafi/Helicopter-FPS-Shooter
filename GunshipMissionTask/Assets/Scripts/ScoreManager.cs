using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	private static ScoreManager instance;
	public static ScoreManager Instance
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

	public int score=0;
	public int maxScore=0;
	// Use this for initialization
	void Start () {
		score = 0;
		maxScore = GameObject.FindGameObjectsWithTag("targetmarker").Length;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
