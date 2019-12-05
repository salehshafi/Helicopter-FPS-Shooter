using UnityEngine;
using System.Collections;

public class BarrelScript : MonoBehaviour 
{
	public GameObject explodePrefab;
	public GameObject catchFireObject;
	public float damageRadius =15;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public bool barrelFired = false;

	public void  CatchFire()
	{

		if (!barrelFired) 
		{
			barrelFired = true;
			catchFireObject.SetActive (true);
			Invoke ("Explode", 1);
		}
	}

	public void Explode()
	{

		GameObject [] barrels = GameObject.FindGameObjectsWithTag("barrel");

		//Debug.Log("Total Barrels"+barrels.Length);
		foreach (GameObject barrel in barrels) 
		{
			//Debug.Log("Barrel Located at Distance: "+Vector3.Distance(transform.position,barrel.transform.position));
			if(Vector3.Distance(transform.position,barrel.transform.position)<damageRadius)
			{
				barrel.GetComponent<BarrelScript>().CatchFire();
			}
		}


		GameObject [] tanks = GameObject.FindGameObjectsWithTag("Tank");

		foreach (GameObject tank in tanks) 
		{
			if(Vector3.Distance(transform.position,tank.transform.position)<damageRadius)
			{
				tank.GetComponent<TankFire>().SelfDestruct();

			}
		}

		if (GetComponentInChildren<Canvas> ()!=null) 
		{
			ScoreManager.Instance.score++;
			Destroy (GetComponentInChildren<Canvas> ().gameObject);
		}

		GetComponent<AudioSource> ().Play ();

		Instantiate (explodePrefab, transform.position, transform.rotation);
		catchFireObject.SetActive (false);
		GetComponent<Renderer> ().enabled = false;


		Destroy (transform.root.gameObject,3.55f);

		//SoundManager.Instance.PlayExplosion ();
	}


}
