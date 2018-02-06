/* The purpose of this script is to be a component of every tile / node in the game scene
For prototyping purposes, the tiles should change to a different color when hovering over them.
They should also change to a different color when clicked.
When one tile is already clicked and is clicked again, a turret is built
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

	public GameObject turretPrefab; //  Reference to the turret prefab. Assigned in the inpector.
	public Color hoverColor, selectedColor; //  The colors are set in the inpector
	internal Color initialColor;    //  The color is internal so that it can be accessed from othe classes, but it's not visible in the inpector
    MeshRenderer mRend; //  The MeshRenderer component.

	public bool tileIsClicked;

	bool tileHasTurret;

	void Start()
	{
        //  Finding the MeshRenderer
		mRend = GetComponent<MeshRenderer> ();
        //  Saving the initial color
		initialColor = mRend.material.color;
	}

    //  Function that checks if the mouse is hovering over the gameObject
    //  Works with GUI elements and Colliders -- > in this case a box collider is used in each tile
	void OnMouseOver()
	{
        //  Everything inside this function happens only when when the mouse pointer is over a tile.

        //  If the tile is not clicked
		if (!tileIsClicked) 
		{
            //  Change the color to the hover color
			ColorChanger (hoverColor);
		}

        //  If the Left mouse button is clicked,
		if (Input.GetMouseButtonDown (0)) 
		{
            //  And this tile is the already selected one and this tile has no turret built yet
			if (GameMaster.gm.selectedTile == this.gameObject && !tileHasTurret) 
			{
                //  Then Instantiate a turret at the assigned position
				var yOffset = .6f;
				Vector3 spawnPos = new Vector3 (transform.position.x, transform.position.y + yOffset, transform.position.z);
				Instantiate (turretPrefab, spawnPos, Quaternion.identity);
                //  And confirm that this tile now has a turret
				tileHasTurret = true;
			}

            //  After that clean the selection, so no tile is clicked
			GameMaster.gm.CleanSelection ();
            //  And set this one as the selected tile
			GameMaster.gm.selectedTile = this.gameObject;
            //  Change the color to the selected color
			ColorChanger (selectedColor);
            //  And confirm that the tile is clicked/selected
			tileIsClicked = true;
		}
	}

    //  Function that checks if the mouse is not hovering over the gameObject anymore
    //  Works with GUI elements and Colliders -- > in this case a box collider is used in each tile
	void OnMouseExit()
	{
        //  If the tile is not clicked
		if (!tileIsClicked) 
		{
            //  change back to the original color
			ColorChanger (initialColor);
		}
	}


    //  Custom function for changing the color of the tile
    //  Takes the desired new color as a parameter --> Look when it's called above
	public void ColorChanger(Color newColor)
	{
        //  The color of the material used is assigned to whatever color we passed every time the function was called.
		mRend.material.color = newColor;
	}

}
