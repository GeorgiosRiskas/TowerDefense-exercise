using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public Text finalScoreTxt;

	void Start () 
	{
		DisplayFinalScore ();
	}


    //  function that displays the final score on the game over screen
	void DisplayFinalScore()
	{
		finalScoreTxt.text = "Final score: " + Saver.saver.score;
	}
	


}
