using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {


	public float moveSpeed = 5;

	public PathScript myPath;
	public EnemyScript enemy;
	public CharacterController botController;
	public int shootingPoint =1;

	public Transform nextWayPoint;
	public int index = 0;

	public GameObject player;
	// Use this for initialization
	void Start () 
	{
		index =  myPath.ClosestWaypoint (transform);
		nextWayPoint = myPath.wayPointsArray [index];

		enemy = GetComponent<EnemyScript> ();

		player = GameObject.FindWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GUIManager.Instance.currentState == GUIManager.mainstates.GAME) {

			if (nextWayPoint != null) {

//				transform.LookAt (nextWayPoint);
				Vector3 dir = (nextWayPoint.position - transform.position).normalized;

				Quaternion rotation = Quaternion.LookRotation (dir);

				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * moveSpeed / 10);


				botController.Move (transform.forward * moveSpeed * Time.deltaTime);
				//botController.Move(transform.forward);
			} else if (gameObject.tag == "Enemy") {
				if (player != null)
					transform.LookAt (player.transform);
				else
					player = GameObject.FindWithTag ("Player");
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


			if(gameObject.tag == "Enemy")
			WayPointSpecificLogic();

			else if (gameObject.tag == "Tank")
			{
				TankSpecificLogic();
			}
		}


	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (gameObject.tag == "Enemy" && hit.gameObject.tag == "Enemy") 
		{
			index = myPath.wayPointsArray.Length -1;
				nextWayPoint = null;

		}
	}

	void WayPointSpecificLogic()
	{
		if (index == shootingPoint) 
		{
			enemy.PlayAnimation(1);
		}

		if (index == myPath.wayPointsArray.Length-1) 
		{
			enemy.PlayAnimation(2);
			nextWayPoint=null;
		}

	}

	void TankSpecificLogic()
	{
		if (nextWayPoint==null) 
		{
//			GetComponent<TankFire> ().ShootInSecs (1);
			//myPath.nextWayPoint = null;
		}
	}
}
