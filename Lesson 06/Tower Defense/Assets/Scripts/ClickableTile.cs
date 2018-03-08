/* The purpose of this script is to be a component of every tile / node in the game scene
For prototyping purposes, the tiles should change to a different color when hovering over them.
They should also change to a different color when clicked.
When one tile is already clicked and is clicked again, a turret is built
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableTile : MonoBehaviour {

//	public GameObject turretPrefab; //  Reference to the turret prefab. Assigned in the inpector.
	public Color hoverColor, selectedColor; //  The colors are set in the inpector
	internal Color initialColor;    //  The color is internal so that it can be accessed from othe classes, but it's not visible in the inpector
    MeshRenderer mRend; //  The MeshRenderer component.

	[HideInInspector]public bool tileIsClicked;

	[HideInInspector]public bool tileHasTurret;


	public GameObject tileUiCanvas;
	public Button basicTurretBt;
	public Button sniperTurretBt;

	bool pointerIsOverUi;

	void Start()
	{
        //  Adding a function on the onClick of the button.
		basicTurretBt.onClick.AddListener (delegate {GameMaster.gm.SpawnTower(0);});

        //  Adding a function on the onClick of the button.
		sniperTurretBt.onClick.AddListener (delegate {GameMaster.gm.SpawnTower(1);});

        //  Finding the MeshRenderer
		mRend = GetComponent<MeshRenderer> ();
        //  Saving the initial color
		initialColor = mRend.material.color;
	}
     


	void Update()
	{
        //  checks if the mouse pointer is over a ui element
		pointerIsOverUi = EventSystem.current.IsPointerOverGameObject ();
	}

    //  Function that checks if the mouse is hovering over the gameObject
    //  Works with GUI elements and Colliders -- > in this case a box collider is used in each tile
	//  Everything inside this function happens only when when the mouse pointer is over a tile.
	void OnMouseOver()
	{
        //  If the tile is not clicked
		if (!tileIsClicked) 
		{
            //  if the mouse is not hovering over a ui element, 
            if (!pointerIsOverUi)
            {
                //  Then clean the selection
                GameMaster.gm.CleanSelection();
            }
            //  Change the color to the hover color
			ColorChanger (hoverColor);
		}

        //  If the Left mouse button is clicked,
		if (Input.GetMouseButtonDown (0) && !pointerIsOverUi)
		{
            //  After that clean the selection, so no tile is clicked
			GameMaster.gm.CleanSelection ();
            //  And set this one as the selected tile
			GameMaster.gm.selectedTile = this.gameObject;
            //  Change the color to the selected color
			ColorChanger (selectedColor);
            //  And confirm that the tile is clicked/selected
			tileIsClicked = true;
            //  Enables the UI button.
			ToggleUiButton (true);
		}
			
	}

    //  Function that checks if the mouse is not hovering over the gameObject anymore
    //  Works with GUI elements and Colliders -- > in this case a box collider is used in each tile
	void OnMouseExit()
	{
        //  if the mouse is not hovering over a ui element,
		if (!tileUiCanvas.activeSelf) 
		{
            //  change back to the original color
			ColorChanger (initialColor);
		}
	}

    //  function for enabling and disabling the ui canvas
    public void ToggleUiButton(bool value){
        tileUiCanvas.SetActive (value);
    }


    //  Custom function for changing the color of the tile
    //  Takes the desired new color as a parameter --> Look when it's called above
	public void ColorChanger(Color newColor)
	{
        //  The color of the material used is assigned to whatever color we passed every time the function was called.
		mRend.material.color = newColor;
	}

}
