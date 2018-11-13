using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour, ITabMenuListener {

	public TabbedMenu mainMenu; 
	// Use this for initialization
	void Start () {
		mainMenu.Initiate(this, 0);
		mainMenu.InteractionActive = true;
	}

	public void ChangedMenu(int index) {
		
	}

	public void SelectedMenu(int index) {
		switch (index) {
			case 0:
				NewGame();
				break;
			case 1:
				LoadGame();
				break;
			case 2:
				Credits();
				break;
			case 3:
				Settings();
				break;
		}
		mainMenu.InteractionActive = false;
	}

	void NewGame() {
		GameController.GetInstance().NewGame();
	}

	void LoadGame() {
		
	}

	void Credits() {
		
	}

	void Settings() {
		
	}
}
