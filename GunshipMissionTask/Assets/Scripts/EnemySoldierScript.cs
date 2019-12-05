using UnityEngine;
using System.Collections;

public class EnemySoldierScript : MonoBehaviour {


	public Animator animator;
	public GameObject firingParticleSystem;
	public Transform player;
	bool dead = false;
	public int rotSpeed=50;
	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (player != null) 
		{
		
//			Vector3 dir = (player.position - transform.position).normalized;
//			
//			//create the rotation to look at the target
//			Quaternion rotation = Quaternion.LookRotation (dir);
//			//rotate over time
//			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotSpeed / 10);
			
			transform.LookAt (player);
			

		}
	
	}

	public void Die()
	{
		if (!dead) 
		{
			dead = true;
			animator.SetInteger ("animState", 1);
			firingParticleSystem.SetActive (false);
			Destroy (GetComponentInChildren<Canvas> ().gameObject);
			ScoreManager.Instance.score++;
		}

	}
}
