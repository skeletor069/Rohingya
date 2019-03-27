using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPanel : MonoBehaviour {

	public PauseMenu pauseMenu;
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			pauseMenu.ShowPauseMenu(2);
		}
		
		
	}
}
