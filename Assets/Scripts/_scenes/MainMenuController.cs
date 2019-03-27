using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour, ITabMenuListener {

	public TabbedMenu mainMenu;
	public NewGameWarning newGameWarning;
	public GameObject mainPanel;
	
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
				Settings();
				break;
			case 3:
				Settings();
				break;
		}
		//mainMenu.InteractionActive = false;
	}
	

	void NewGame() {
		if (GameController.GetInstance().HasSavedData()) {
			// show ui
			mainPanel.SetActive(false);
			newGameWarning.ShowNewGameWarning();
		}else
			GameController.GetInstance().NewGame();
	}

	void LoadGame() {
		if (GameController.GetInstance().HasSavedData()) {
			GameController.GetInstance().LoadGame();
		}
		else {
			// show ui
		}

	}

	void Credits() {
		
	}

	void Settings() {
		
	}

	public void ShowMainPanel() {
		mainPanel.SetActive(true);
		mainMenu.Initiate(this, 0);
		mainMenu.InteractionActive = true;
	}

}
