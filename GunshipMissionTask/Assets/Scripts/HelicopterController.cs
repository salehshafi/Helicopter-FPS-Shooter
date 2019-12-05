using UnityEngine;
using System.Collections;

public class HelicopterController : MonoBehaviour {

	public float moveSpeed = 5;
	
	public PathScript myPath;

	public CharacterController botController;

	public Transform nextWayPoint;
	public int index = 0;

	public bool elevate = false;
	public bool move = false;

	public Transform helicopterTransform;
	// Use this for initialization
	Quaternion initialRotation;

	void Start () 
	{
		index =  0;
		nextWayPoint = myPath.wayPointsArray [0];

		initialRotation = helicopterTransform.rotation;

		//Invoke ("StartElevation", 5);
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i =1; i<myPath.wayPointsArray.Length; i++) 
		{
			Debug.DrawLine(myPath.wayPointsArray[i-1].position,myPath.wayPointsArray[i].position,Color.red);
		}

		if (elevate) 
		{
			if(botController.transform.position.y<myPath.wayPointsArray[0].transform.position.y)

			{
				botController.Move (transform.up * moveSpeed * Time.deltaTime);
			}
			else
			{
				//elevate=false;
				move=true;
				MultipleCameraSwitchScript.Instance.TurnOffMenuCamera();
				MultipleCameraSwitchScript.Instance.TurnOnCameraNumber(2);

			}
		}

		if (move && nextWayPoint != null) {
			if (elevate) {
				if (helicopterTransform.localEulerAngles.x < 285)
					helicopterTransform.Rotate (-Time.deltaTime * 2, 0, 0);//eulerAngles = new Vector3(helicopterTransform.localEulerAngles.x-Time.deltaTime,0,0);
					else {
					elevate = false;
					GUIManager.Instance.currentState = GUIManager.mainstates.GAME;
					MultipleCameraSwitchScript.Instance.TurnOnCameraNumber (0);
				}
			}

			if(nextWayPoint == myPath.wayPointsArray[myPath.wayPointsArray.Length-1])
				if (helicopterTransform.localEulerAngles.x >270 )
					helicopterTransform.Rotate (Time.deltaTime * 2, 0, 0);//eulerAngles = new Vector3(helicopterTransform.localEulerAngles.x-Time.deltaTime,0,0);

			
			//				else
//			{

			//transform.LookAt (nextWayPoint);
//			Vector3 heading = transform.position - nextWayPoint.position;
//			float distnace = heading.magnitude;
//			Vector3 direction = heading/distnace;

			Vector3 dir = (nextWayPoint.position - transform.position).normalized;
			
			//create the rotation to look at the target
			Quaternion rotation = Quaternion.LookRotation (dir);
			//rotate over time
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * moveSpeed / 10);

			//transform.LookAt (nextWayPoint);

			botController.Move (transform.forward * moveSpeed * Time.deltaTime);
				

			//botController.Move(transform.forward);
//			}
		}
		else if (GUIManager.Instance.currentState== GUIManager.mainstates.GAME)
		{
//			if(helicopterTransform.localEulerAngles.x<90)
//				helicopterTransform.Rotate(Time.deltaTime*25,0,0) ;
//			else
			{
				helicopterTransform.rotation = initialRotation ;
				GUIManager.Instance.GameOver();


			}
		}

	}
	
	void OnTriggerEnter(Collider col)
	{
		
		if (col.tag == "wayPoint") 
		{
			//index = myPath.UpdateWaypoint();
			if (index == myPath.wayPointsArray.Length -1) 
			{
				nextWayPoint = null;
				
				//col.transform.position = new Vector3(Random.Range(col.transform.position.x+1,col.transform.position.x-1),col.transform.position.y,Random.Range(col.transform.position.z+1,col.transform.position.z-1));
				
			}
			else
				
			{
				index++;
				nextWayPoint = myPath.wayPointsArray[index];
				
			}
	
		}
		
		
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
//		if (gameObject.tag == "Enemy" && hit.gameObject.tag == "Enemy") 
//		{
//			index = myPath.wayPointsArray.Length -1;
//			nextWayPoint = null;
//			
//		}
	}

	public void StartElevation()
	{
		elevate = true;

	}

	
}
