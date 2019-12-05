using UnityEngine;
using System.Collections;

public class PathScript : MonoBehaviour 

{



	public Transform [] wayPointsArray;
	Transform nextWayPoint;
	int index = 0;

	public bool hidePathMesh = true;
	// Use this for initialization
	void Awake () 
	{

		foreach (Transform wP in wayPointsArray)
			wP.gameObject.GetComponent<Renderer> ().enabled = false;
		nextWayPoint = wayPointsArray [index];


	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public int UpdateWaypoint( )
	{
		if (index == wayPointsArray.Length -1) 
		{
			nextWayPoint = null;

		}

		index++;

		//nextWayPoint = wayPointsArray [index];

		return index;

	}

	public int ClosestWaypoint(Transform aiBot)
	{
		float minDist = Vector3.Distance( aiBot.position,wayPointsArray [0].position);

		for (int i=1;i<wayPointsArray.Length;i++)
		{
			if(Vector3.Distance(aiBot.position,wayPointsArray[i].position)<minDist)
			{
				minDist = Vector3.Distance(aiBot.position,wayPointsArray[i].position);
				index = i;
				nextWayPoint = wayPointsArray [index];
			}

		}

		return index;
	}
}
