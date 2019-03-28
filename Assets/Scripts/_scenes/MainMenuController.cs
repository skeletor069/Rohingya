using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuController : MonoBehaviour, ITabMenuListener {

	public TabbedMenu mainMenu;
	public NewGameWarning newGameWarning;
	public GameObject mainPanel;
	public SimulationPanel overlayPanel;
	public AudioMixerSnapshot sceneEndSnapshot;
	
	IEnumerator Start () {
		mainMenu.Initiate(this, 0);
		mainMenu.InteractionActive = true;
		yield return new WaitForSeconds(1);
		if(!SoundManager.GetInstance().IsPlaying(SoundTypes.MENU_BG))
			SoundManager.GetInstance().PlaySound(SoundTypes.MENU_BG);
		overlayPanel.DissolveOverlay();
	}

	public void ChangedMenu(int index) {
		SoundManager.GetInstance().PlaySound(SoundTypes.BTN_CHOOSE);
	}

	public void SelectedMenu(int index) {
		switch (index) {
			case 0:
				StartCoroutine(NewGame());
				break;
			case 1:
				StartCoroutine(LoadGame());
				break;
			case 2:
				Settings();
				break;
			case 3:
				Settings();
				break;
		}
		SoundManager.GetInstance().PlaySound(SoundTypes.BTN_SELECT);
		//mainMenu.InteractionActive = false;
	}
	

	IEnumerator NewGame() {
		if (GameController.GetInstance().HasSavedData()) {
			mainPanel.SetActive(false);
			newGameWarning.ShowNewGameWarning();
		}
		else {
			overlayPanel.StartOverlay();
			sceneEndSnapshot.TransitionTo(4);
			yield return new WaitForSeconds(2);
			GameController.GetInstance().NewGame();
		}
			
	}

	public IEnumerator NewGameAfterWarning() {
		overlayPanel.StartOverlay();
		sceneEndSnapshot.TransitionTo(4);
		yield return new WaitForSeconds(2);
		GameController.GetInstance().NewGame();
	}

	IEnumerator LoadGame() {
		if (GameController.GetInstance().HasSavedData()) {
			overlayPanel.StartOverlay();
			sceneEndSnapshot.TransitionTo(4);
			yield return new WaitForSeconds(2);
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
