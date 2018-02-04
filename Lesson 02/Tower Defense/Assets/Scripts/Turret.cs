using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	public GameObject bulletPrefab;
	public GameObject partToRotate;
	public Transform bulletSpawnPoint;
	public Transform target;
	public float range;
	public float rotationSpeed;
	public float shootTimer;
	private float _shootTimer;

	void Start () 
	{
		_shootTimer = shootTimer;
	}
	

	void Update () 
	{
		FindEnemy ();

		if (target == null) 
		{
			return;
		}

		shootTimer -= Time.deltaTime;

		if(shootTimer <= 0)
		{
			ShootBullet ();
			shootTimer = _shootTimer;
		}

		Vector3 direction = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (direction);
		Vector3 newRotation = Quaternion.Lerp (partToRotate.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
		partToRotate.transform.rotation = Quaternion.Euler(0, newRotation.y, 0);

	}


	void FindEnemy()
	{
		foreach (GameObject en in GameMaster.gm.allEnemies) 
		{
			if (Vector3.Distance (en.transform.position, this.transform.position) <= range)
			{
				target = en.transform;
			}
		}

		if (target == null) 
		{
			return;
		}

		if (Vector3.Distance (target.position, this.transform.position) > range) 
		{
			target = null;			
		}
	}


	void ShootBullet()
	{
		Instantiate (bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
	}

}
