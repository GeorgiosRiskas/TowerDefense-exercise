using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float bulletDamage = 1;
	public float speed;
	public float rotationSpeed;
	public Transform target;

	void Start () 
	{
        //  On start, destroy the gameobject 3 seconds after its creation in the scene
        //  In case it is not destroyed by the collision or for any othe reason
		Destroy (gameObject, 3f);
	}


	void Update () 
	{
        //  If there is no target set
		if (target == null) 
		{
            //  Go no further in the code execution
			return;
		}

        //  Setting the rotation and the bullet's movement in an identical way as in the enemy's movement --> check the Enemy script
		Vector3 direction = target.position - transform.position;
		transform.Translate (direction.normalized * Time.deltaTime * speed, Space.World);
		Quaternion lookRotation = Quaternion.LookRotation (direction);
		Vector3 newRotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
		transform.rotation = Quaternion.Euler (0, newRotation.y, 0);
	}


    //  Custom function for setting the target of the bullet
    //  Called from the Turret script, upon the bullet's instantiation
	public void SetTarget(Transform turretTarget)
	{
        //  The target is whatever transform was passed as a parameter when the function was called
		target = turretTarget;
	}


    //  Checks if a collision happened between two objects that use colliders (and rigidbodies at least for one of them)
	void OnCollisionEnter(Collision col)
	{
        //  If the projectile collides with a gameobject that has a component of the type Enemy aka an enemy
		if (col.gameObject.GetComponent<Enemy> ()) 
		{
            //  Then call the DamageEnemy function at the gameobject aka the enemy
			col.gameObject.GetComponent<Enemy>().DamageEnemy(bulletDamage);
            //  And destroy the projectile itself
			Destroy (this.gameObject);
		}
	}
}
