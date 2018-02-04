using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;         //  speed of enemy movement 
	public float rotationSpeed; //  speed of rotation when turning
	Transform target;           //  where the enemy should go. The target changes depending on the waypoints.

	private int waypointIndex = 0;  //  Keep count of the waypoints index


	void Start () 
	{
        //  On start the waypoint of element 0 is set as target.
		target = Waypoints.waypoints [waypointIndex];
	}
	


	void Update () 
	{
        //  Subtracting the object's position from the target's position returns the vector of the direction,
        //  from the object to the target.
		Vector3 direction = target.position - transform.position;

        //  Translating the enemy to the direction of the target.
        //  The direction is normalized, so that it will always have a magnitude of 1. That means that the movement will not change
        //  if some of the targets are closer of further away than others.
        //  Use of delta time makes the movement framerate independent.
        //  Finally it's multiplied by the speed that is set in the isnpector.
		transform.Translate (direction.normalized * Time.deltaTime * speed, Space.World);


        //  HANDLING ROTATION
        //  Creating a rotation that faces towards the direction of the enemy's movement 
		Quaternion lookRotation = Quaternion.LookRotation (direction);

        //  transform.rotation = lookRotation could be used but the desired rotation to be applied is only on the y axis.
        //  The way to do this is by casting the Quaternion to a Vector3 (by using euler angles)
		Vector3 newRotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;

        //  And then casting it back to a Quaternion, using the function that allows to pass an euler Vector3 values.
        //  In that way, the rotation is applied only on the axis that was wanted.
		transform.rotation = Quaternion.Euler (0, newRotation.y, 0);


        //  To detect when the enemy should go to the next waypoint
        //  The distance between the target and the enemy is compared
		if (Vector3.Distance (target.position, transform.position) < .3f) 
		{
            //  When it returns a small value the function is called.
			SwitchWaypoints ();
		}
	}

    //  function for switching to the next waypoint.
	void SwitchWaypoints()
	{
        //  If the enemy hasn't reached the last waypoint yet,
		if (waypointIndex < Waypoints.waypoints.Length - 1) 
		{
            //  Then the index is incremented
			waypointIndex++;
            //  And the target is set to the element of that index
			target = Waypoints.waypoints [waypointIndex];
		} 
        //  If the last element is reached
		else 
		{
            //  Then destroy the enemy
			Destroy (this.gameObject);
		}
	}

    //  Function that is called automaticaly whenever the current gameObject is destroyed
	void OnDestroy()
	{
        //  Removes the enemy from the list that collects all the living enemies.
		GameMaster.gm.allEnemies.Remove (this.gameObject);
		print (gameObject.name + " was removed from the list");
	}

}
