using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;
	public float rotationSpeed;
	public Transform target;

	private int waypointIndex = 0;

	void Start () 
	{
		target = Waypoints.waypoints [waypointIndex];
	}
	


	void Update () 
	{
		Vector3 direction = target.position - transform.position;

		transform.Translate (direction.normalized * Time.deltaTime * speed, Space.World);

		Quaternion lookRotation = Quaternion.LookRotation (direction);

		Vector3 newRotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;

		transform.rotation = Quaternion.Euler (0, newRotation.y, 0);



		if (Vector3.Distance (target.position, transform.position) < .3f) 
		{
			SwitchWaypoints ();
		}
	}

	void SwitchWaypoints()
	{
		if (waypointIndex < Waypoints.waypoints.Length - 1) 
		{
			waypointIndex++;
			target = Waypoints.waypoints [waypointIndex];
		} 
		else 
		{
			Destroy (this.gameObject);
		}


	}
}
