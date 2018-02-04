using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;
	public List <GameObject> allEnemies;


	void Awake () 
	{
		gm = this;
	}
	


	void Update () 
	{
		
	}
}
