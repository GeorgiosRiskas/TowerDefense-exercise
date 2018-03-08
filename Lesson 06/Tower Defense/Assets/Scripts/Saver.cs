/*The saver is responsible for storing data that need to be transfered from scene to scene */
using UnityEngine;

public class Saver : MonoBehaviour {
	public static Saver saver;

	public int score;

	void Awake () 
	{
        //  SINGLETON pattern

        //  if there is a saver on the scene, 
		if (saver) 
		{
            //  Then destroy this one immediately --> because there is already one
			DestroyImmediate (this.gameObject);	
		}
        //  if there is not one,
		else 
		{
            //  Then don't destroy this one
			DontDestroyOnLoad (this.gameObject);
            //  And set it as the current saver.
			saver = this;
		}
	}
	

}
