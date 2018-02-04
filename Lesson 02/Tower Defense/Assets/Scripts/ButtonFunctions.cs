using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {

	public void LoadLevel(int buildIndex)
	{
		SceneManager.LoadScene (buildIndex);
	}

	public void QuitGame()
	{
		Application.Quit ();
		print ("Quit Game");
	}

}
