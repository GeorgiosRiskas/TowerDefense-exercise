using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	public TowerType towerType;

	public GameObject bulletPrefab; //  prefab of the bullet, set in the inspector.
	public GameObject partToRotate; //  GameObject that contains the Gun part
	public Transform bulletSpawnPoint;  //  the place to spawn the bullet
	public Transform target;    //  The current target for the turret
	public float range; //  the range for detecting enemies
	public float rotationSpeed; //  speed of turret rotation
	public float shootTimer;    //  timer for shooting
	private float _shootTimer;  // used for reseting the timer

	void Start () 
	{
		_shootTimer = shootTimer;


		if (towerType == TowerType.Basic) 
		{
			range = 2.5f;
		} 
		else if (towerType == TowerType.Sniper) 
		{
			range = 10;
		}
	}
	

	void Update () 
	{
        //  Function for finding the closest enemy
		FindEnemy ();

        //  If there is no target, the code should not compile further because it is required
        //  both for rotating the turret and shooting
		if (target == null) 
		{
			return;
		}

        //  Timer used for shooting
		shootTimer -= Time.deltaTime;

        //  When the timer reaches 0
		if(shootTimer <= 0)
		{
            //  The turret shoots 
			ShootBullet ();
            //  and the timer is reset
			shootTimer = _shootTimer;
		}

        //  Very similar to the way the rotation was set on the Enemy script. Check it out for reference.
        //  The biggest difference is that this time there is a part to rotate that should have a correct forward direction.
        //  The blue arrow should be facing towards the direction of the path.
		Vector3 direction = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (direction);
		Vector3 newRotation = Quaternion.Lerp (partToRotate.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
		partToRotate.transform.rotation = Quaternion.Euler(0, newRotation.y, 0);

	}


    //  Custom function for finding the closest enemy
	void FindEnemy()
	{
        //  Running a loop through all the existing enemies
		foreach (GameObject en in GameMaster.gm.allEnemies) 
		{
            //  For each one of them that is withing range,
			if (Vector3.Distance (en.transform.position, this.transform.position) <= range)
			{
                //  Is set as the current target
				target = en.transform;
			}
		}

        //  If the terget is not set, 
		if (target == null) 
		{
            //  The code will not go further
			return;
		}

        //  If the distance from the target is bigger than the range,
		if (Vector3.Distance (target.position, this.transform.position) > range) 
		{
            //  then the turret misses the target
			target = null;			
		}
	}

    //  function for shooting
	void ShootBullet()
	{
        //  Instantiating the bullet prefab on the spawn point that is set on the inspector.
		GameObject spawnedBullet = Instantiate (bulletPrefab, bulletSpawnPoint.position, Quaternion.identity) as GameObject;
        //  Getting a reference of the bullet's script
		Projectile projectileScript = spawnedBullet.GetComponent<Projectile> ();
        //  Calling the function from the bullet's script to set its target as the same that the current turret has.
		projectileScript.SetTarget (target);
	}

}
