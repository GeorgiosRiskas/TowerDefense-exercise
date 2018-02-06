using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject enemyPrefab;  //   The prefab of the enemy. Assigned on inpector

	public float spawnTimer = 5f;   //  The timer used for spawning
	private float _spawnTimer;      // used for reseting the timer.


	void Start () {
		_spawnTimer = spawnTimer;
	}
	


	void Update () 
	{
        //  The timer counts down
		spawnTimer -= Time.deltaTime;	// shorthand for spawnTimer = spawnTimer - Time.deltaTime;

        //  When it reaches 0
		if (spawnTimer <= 0) 
		{
            // An enemy is spawned 
			SpawnEnemy ();
            // And the timer is reset
			spawnTimer = _spawnTimer;
		}
	}


	void SpawnEnemy()
	{
        //  Instantiate an enemy in the position of this object and with no rotation.
		GameObject go = Instantiate (enemyPrefab, transform.position, Quaternion.identity) as GameObject;

        //  For tiding up the hierarchy all the new enemies are moved on a parent gameobject named Enemies
		Transform enemies = GameObject.Find ("Enemies").transform;
		go.transform.parent = enemies;

        //  If the current created enemy is not already in the list of enemies
		if (!GameMaster.gm.allEnemies.Contains (go)) 
		{
            //  Then add it
			GameMaster.gm.allEnemies.Add (go);
		}

	}

}
