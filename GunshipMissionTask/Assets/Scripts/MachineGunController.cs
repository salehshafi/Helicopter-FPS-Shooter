using UnityEngine;
using System.Collections;

public class MachineGunController : MonoBehaviour 
{
	private static MachineGunController instance;
	public static MachineGunController Instance
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

	public Animator animator;
	public GameObject particleSystem;
	public AudioSource audioSource;
	public GameObject bulletPrefab;


	// Use this for initialization
	void Start () {

		if (animator == null)
			animator = GetComponent<Animator> ();

		if(audioSource==null)
		audioSource = GetComponent<AudioSource> ();

		particleSystem.SetActive (false);
	
	}

	bool shotOne=false;
	// Update is called once per frame
	void Update () 
	{
		if (GUIManager.Instance.currentState == GUIManager.mainstates.GAME && GUIManager.Instance.crossHairs.activeSelf) 
		{
			if (Input.GetMouseButtonDown(0)) 
			{
				animator.SetInteger ("stateChange", 1);
				particleSystem.SetActive (true);
				audioSource.Play();

			}

			if(Input.GetMouseButton(0))
			{
				if(shotOne==false)
				{
					shotOne=true;
					StartCoroutine(ShootBullet());
				}
			}
		}

		if (Input.GetMouseButtonUp(0)) 
		{
			animator.SetInteger ("stateChange", 0);
			particleSystem.SetActive (false);
			audioSource.Stop();
			
		}
	}

	IEnumerator ShootBullet()
	{
		Vector3 pos = MultipleCameraSwitchScript.Instance.gameCameras [0].transform.position;

		GameObject bullet = Instantiate (bulletPrefab, pos, Quaternion.identity) as GameObject ;
		bullet.GetComponent<Rigidbody> ().velocity = MultipleCameraSwitchScript.Instance.gameCameras [MultipleCameraSwitchScript.Instance.selectedCamera].transform.forward * 500;
		yield return new WaitForSeconds (0.1f);
		shotOne = false;
		Destroy (bullet, 3);
	}
}
