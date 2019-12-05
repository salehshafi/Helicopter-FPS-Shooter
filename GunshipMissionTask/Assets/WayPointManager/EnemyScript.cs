using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public Animator animator;

	public enum EnemyState { Run,RunShoot,StandShoot,Dead };

	public EnemyState currentEnemyState = EnemyState.Run;

	public GameObject bulletPrefab;
	public Transform nozzle;



	// Use this for initialization
	void Start () 
	{

		//animator = GetComponent<Animator> ();
		if(gameObject.tag == "Enemy")
		GetComponentInChildren <ParticleRenderer>().enabled=false;
	}


	public bool shoot =true;
	public float shootWait = 6;
	// Update is called once per frame
	void Update () 
	{
		if (currentEnemyState == EnemyState.StandShoot && shoot) 
		{
			shoot =false;
			Invoke("ReloadWait",Random.Range (shootWait,shootWait+3));
			FireBullet();

		}
	}

	public void ReloadWait()
	{
		shoot = true;
	}

	public void PlayAnimation(int index)
	{
		if (animator == null)
			return;
	
		if (index == 1)
			currentEnemyState = EnemyState.RunShoot;
		else
			if (index == 2) 
		{
		 	 	currentEnemyState = EnemyState.StandShoot;
				GetComponentInChildren <ParticleRenderer>().enabled=true;
		}
		else
			currentEnemyState = EnemyState.Dead;

		animator.StopPlayback ();
		animator.SetInteger ("animState", index);


	}

	public void PlayDeath()
	{
		if (animator == null)
			return;
		int index = 11;
		switch(currentEnemyState)
		{
		case EnemyState.Run:
			index = Random.Range(11,13);
			Invoke("LightsOut",Random.Range(0,0.5f));
			break;
		case EnemyState.RunShoot:
			index = Random.Range(13,15);
			Invoke("LightsOut",Random.Range(0,0.5f));
			break;
		case EnemyState.StandShoot:
			index = 21;
			Invoke("LightsOut",Random.Range(1,1.5f));
			//GetComponentInChildren <ParticleRenderer>().enabled=false;
			break;
		
		}
		currentEnemyState = EnemyState.Dead;
		animator.StopPlayback ();
		animator.SetInteger ("animState", index);
		Destroy (gameObject, 5);
		//EnemyManager.Instance.SpawnNewEnemyAtPath (GetComponent<FollowPath> ().myPath);


	}

	void LightsOut()
	{
		GetComponentInChildren <ParticleRenderer>().enabled=false;
	}

	public float firePower = 100;

	public void FireBullet()
	{

		GameObject bull = Instantiate (bulletPrefab, nozzle.position, transform.rotation) as GameObject;
		bull.GetComponent<Rigidbody> ().AddForce (transform.forward * firePower, ForceMode.Impulse);
		//Debug.Break ();
	}
}
