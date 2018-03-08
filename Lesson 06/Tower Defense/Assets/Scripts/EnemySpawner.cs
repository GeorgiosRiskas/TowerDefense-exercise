using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public float waveTimer = 20;
	private float _waveTimer;

	public GameObject enemyPrefab;  //   The prefab of the enemy. Assigned on inpector

	public float spawnTimer = 5f;   //  The timer used for spawning
	private float _spawnTimer;      // used for reseting the timer.


	public int enemyCountPerWave = 0;   // The amount of enemies of the current wave.
	public int enemyWaveSize = 10;  //  the desired initial wave size


	void Start () 
	{
		_spawnTimer = spawnTimer;
		_waveTimer = waveTimer;
	}
	


	void Update () 
	{
        //  The wave timer counts down
		waveTimer -= Time.deltaTime;

        //  Making sure that the timer will not go below 0
		waveTimer = Mathf.Clamp (waveTimer, 0, _waveTimer);

        //  changing the UI text of the wave timer
        //  The waveTimer is converted into int so that the numbers that show in the UI are only integers.
		GameMaster.gm.timerTxt.text = "" + (int)waveTimer;

        //  If the wave timer is still counting
		if (waveTimer > 0)
		{
            //  don't go below that point --> because this is where the spawning of the enemies happens.
			return;
		}

        //  The timer counts down
		spawnTimer -= Time.deltaTime;	// shorthand for spawnTimer = spawnTimer - Time.deltaTime;

        //  When it reaches 0
		if (spawnTimer <= 0) 
		{
            // An enemy is spawned 
			SpawnEnemy ();

            //  After an enemy is spawned, one more is added to the current enemies per wave count
			enemyCountPerWave++;

            //  If the desired amount of enemies has spawned
			if (enemyCountPerWave == enemyWaveSize) 
			{
                //  The wave timer starts counting again
				waveTimer = _waveTimer;
                //  the count is reset
				enemyCountPerWave = 0;
			}

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
