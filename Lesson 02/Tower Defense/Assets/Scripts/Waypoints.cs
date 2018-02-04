/* This script is attached on the parent object that contains all the waypoints */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

	public static Transform[] waypoints;    //  All the waypoints that the enemies should follow to navigate through the path.


	void Awake () 
	{
        //  The size of the array is equal to the amount of waypoints gameobjects that are parented.
		waypoints = new Transform[transform.childCount];

        //  Looping through all the elements of the array
		for (int i = 0; i < waypoints.Length; i++) 
		{
            //  And making sure that they are assigned in the same order as they show in the hierarchy.
			waypoints [i] = transform.GetChild (i);
		}
	}
}
