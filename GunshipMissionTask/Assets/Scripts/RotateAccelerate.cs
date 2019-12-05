using UnityEngine;
using System.Collections;

public class RotateAccelerate : MonoBehaviour {

	public float xRot =0;
	public float yRot =0;
	public float zRot =0;

	public float currentSpeed =0;
	public float endSpeed =50;

	public float increment =0.1f;

	// Use this for initialization
	void Start () 
	{
	
	}

	// Update is called once per frame
	void Update () 
	{
		if (currentSpeed < endSpeed)
			currentSpeed += increment;
		transform.Rotate (xRot * currentSpeed, yRot * currentSpeed, zRot * currentSpeed);
	}
}
