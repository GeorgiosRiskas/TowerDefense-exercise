/* Class used for storing information that many scripts would need access to */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;    //  Creating a static reference of GameMaster so it can be called from other scripts faster.

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

	}


    // Custom function that cleans the selected tile selection 
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


}
