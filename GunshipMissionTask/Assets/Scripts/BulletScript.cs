using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
	public GameObject  [] hitPrefabs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "target") 
		{
			Destroy( Instantiate(hitPrefabs[0],col.contacts[0].point,Quaternion.identity),3);
			Destroy(col.gameObject);

		}

		else if (col.gameObject.tag == "terrain") 
		{
			Destroy( Instantiate(hitPrefabs[1],col.contacts[0].point,Quaternion.identity),3);
			//Destroy(col.gameObject);
			
		}
		else if (col.gameObject.tag == "fueltanker") 
		{
			Debug.Log("FUEL TANKER HIT!");
			col.transform.root.gameObject.GetComponent<TankFire>().SelfDestruct();
			
		}
		else if (col.gameObject.tag == "enemysoldier") 
		{
			
			col.transform.root.gameObject.GetComponent<EnemySoldierScript>().Die();
			
		}
		else if (col.gameObject.tag == "barrel") 
		{
			col.gameObject.GetComponent<BarrelScript>().CatchFire();
			//Destroy( Instantiate(hitPrefabs[1],col.contacts[0].point,Quaternion.identity),3);
			//Destroy(col.gameObject);
			
		}
	}
}
