/* Class used for storing information that many scripts would need access to */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TowerType {Basic, Sniper};   //  Creating an enum that will be used for the different tower types.

public class GameMaster : MonoBehaviour {

	public GameObject [] turretPrefabs; //  Reference to the turret prefab. Assigned in the inpector.

	public static GameMaster gm;    //  Creating a static reference of GameMaster so it can be called from other scripts faster.

	public int resources = 200;     //  The resources

	public int [] towerPrices;    //  The basic tower price

	public Text resourcesTxt;   //  Text reference for the resources
	public Text timerTxt;   //  Text reference for the wave timer

	[HideInInspector]
	public List <GameObject> allEnemies;    //  List to contain all the enemies that are alive.

	[HideInInspector] 
	public GameObject selectedTile;     //  Gameobject for saving the selected tile.
        
    ClickableTile [] allTiles;      // An array of ClickableTile(tiles) scripts


	void Awake ()
	{
        //  Since gm is a reference it has to be assigned to a value.
		gm = this;

        //  Finding all the tiles' scripts in the scene.
        // One cannot be sure of the order that they will be assigned in the array, but in that case that is indifferent.
		allTiles = FindObjectsOfType<ClickableTile> ();

		UpdateResources ();


		towerPrices = new int[]{ 50, 100 };
	}


    // Custom function that cleans the 	selected tile selection 
    //  and sets all the clicked tiles to not clicked.
    //  It is meant to be called by the tile script and this is why it is public
    //  Called when selecting a new tile --> Check on the ClickableTile script.
	public void CleanSelection()
	{
		selectedTile = null;

		//	For each one of the tiles
		foreach (var t in allTiles) 
		{
			//	if one is clicked
			if (t.tileIsClicked) 
			{
				//	set it to not clicked
				t.tileIsClicked = false;
				//	And set it back to its default color
				t.ColorChanger(t.initialColor);
			}
		}
	}


    //  Function for changing the resources both for adding and subtracting
    //  In case of subtracting the changeAmount has to be passed as a negative number --> look at Clickable Tile, line 56
	public void ModifyResources(int changedAmount)
	{
		resources += changedAmount;
		UpdateResources ();
	}

    //  Function for updating the resources in the UI --> needs to be renamed
	public void UpdateResources()
	{
        //  updating the ui text with the current resources amount.
		resourcesTxt.text = "Resources: " + resources.ToString();
	}


    //  Function responsible for spawning towers
	public void SpawnTower(int towerTypeIndex)
	{
		ClickableTile selectedTileScript = selectedTile.GetComponent<ClickableTile> ();

        //  If the selected tile doesn't have a turret and the resources are enough
		if (!selectedTileScript.tileHasTurret && resources >= towerPrices[towerTypeIndex]) 
		{
            //  Setting up the position for the tower to be instantiated
			var yOffset = .6f;
			Vector3 spawnPos = new Vector3 (selectedTile.transform.position.x, selectedTile.transform.position.y + yOffset, selectedTile.transform.position.z);

			//  Instantiate the tower prefab and save the gameObject reference
			GameObject spawnedTower = Instantiate (turretPrefabs[towerTypeIndex], spawnPos, Quaternion.identity) as GameObject;

            // Reduce the corresponding resources accordingly
			ModifyResources (-towerPrices[towerTypeIndex]);

			//  Getting the script of the spawned tower
			Turret spawnedTowerScript = spawnedTower.GetComponent<Turret> ();
			//  And setting its type to the same type that was passed as a parameter when the function was called.
			spawnedTowerScript.towerType = (TowerType)towerTypeIndex;

            //  Confirming that the selected tile now has a tower built
			selectedTileScript.tileHasTurret = true;
		}


      
	}
}
