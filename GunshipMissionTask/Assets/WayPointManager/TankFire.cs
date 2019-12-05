using UnityEngine;
using System.Collections;

public class TankFire : MonoBehaviour {

	// Use this for initialization


	bool tankAlive = true;

	public GameObject explosionPrefab;
	public GameObject destructionPrefab;
	public GameObject catchFireObject;
	public GameObject gun;

	public void SelfDestruct()
	{
		if(tankAlive)
		{


			if(destructionPrefab!=null)
				Instantiate (destructionPrefab, gun.transform.position, Quaternion.identity);
			GameObject x = Instantiate (explosionPrefab, gun.transform.position, Quaternion.identity) as GameObject;
			Destroy (x, Random.Range (3, 5));
			catchFireObject.SetActive(true);

			tankAlive = false;

			GetComponent<FollowPath>().enabled= false;
			Destroy(gun.gameObject);
			Destroy (GetComponentInChildren<Canvas> ().gameObject);
			ScoreManager.Instance.score++;
			GetComponent<AudioSource>().Play();
			//Invoke("CheckGameStatus",3);
			Debug.Log ("POOF");

		}

	}

	public PathScript nextPath;

	

}
