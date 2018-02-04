/* Class used for storing information that many scripts would need access to */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;    //  Creating a static reference of GameMaster so it can be called from other scripts faster.
	public List <GameObject> allEnemies;    //  List to contain all the enemies that are alive.


	void Awake () 
	{
        //  Since gm is a reference it has to be assigned to a value.
		gm = this;
	}	


}
