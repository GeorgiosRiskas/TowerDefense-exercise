using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject enemyPrefab;

	public float spawnTimer = 5f;
	private float _spawnTimer;


	void Start () {
		_spawnTimer = spawnTimer;
	}
	


	void Update () 
	{
		spawnTimer -= Time.deltaTime;	// shorthand for spawnTimer = spawnTimer - Time.deltaTime;

		if (spawnTimer <= 0) 
		{
			SpawnEnemy ();
			spawnTimer = _spawnTimer;
		}
	}


	void SpawnEnemy()
	{
		GameObject go = Instantiate (enemyPrefab, transform.position, Quaternion.identity) as GameObject;

		Transform enemies = GameObject.Find ("Enemies").transform;

		go.transform.parent = enemies;

		if (!GameMaster.gm.allEnemies.Contains (go)) 
		{
			GameMaster.gm.allEnemies.Add (go);
		}

	}

}
