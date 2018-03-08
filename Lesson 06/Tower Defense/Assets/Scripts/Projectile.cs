using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float bulletDamage = 1;
	public float speed;
	public float rotationSpeed;
	public Transform target;

	Vector3 direction;

	void Start () 
	{
        //  On start, destroy the gameobject 3 seconds after its creation in the scene
        //  In case it is not destroyed by the collision or for any othe reason
		Destroy (gameObject, 3f);

	}


	void Update () 
	{
        //  Setting the rotation and the bullet's movement in an identical way as in the enemy's movement --> check the Enemy script
		transform.Translate (direction.normalized * Time.deltaTime * speed, Space.World);
	}


    //  Custom function for setting the target of the bullet
    //  Called from the Turret script, upon the bullet's instantiation
	public void SetTarget(Transform turretTarget)
	{
        //  The target is whatever transform was passed as a parameter when the function was called
		target = turretTarget;
		direction = target.position - transform.position;
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
