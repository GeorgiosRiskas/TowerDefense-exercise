/*The functions on this class are called on Button pressed. That is why all the functions are and will be public */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //   Required to use the SceneManager

public class ButtonFunctions : MonoBehaviour {

    //  Loads the level og the index that is passed as a paramater
    //  The build index can be seen on File--> Build Settings.
    //  If the scene is not added on the list it has to be added.
	public void LoadLevel(int buildIndex)
	{
		SceneManager.LoadScene (buildIndex);
	}

    //  The function quits the application 
    //  Can be tested on a build. It won't exit from the editor.
	public void QuitGame()
	{
		Application.Quit ();
		print ("Quit Game");
	}

}
