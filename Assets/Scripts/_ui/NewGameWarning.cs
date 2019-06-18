using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameWarning : MonoBehaviour, ITabMenuListener {

	public MainMenuController mainMenuController;
	public TabbedMenu warningMenu;
	

	void Awake() {
		warningMenu.Initiate(this, 0);
		warningMenu.InteractionActive = true;
	}

	public void ShowNewGameWarning() {
		gameObject.SetActive(true);
		warningMenu.Initiate(this, 0);
		warningMenu.InteractionActive = true;
	}

	private void Start() {
		gameObject.SetActive(false);
	}

	public void ChangedMenu(int index) {
		SoundManager.GetInstance().PlaySound(SoundTypes.BTN_CHOOSE);
	}

	public void SelectedMenu(int index) {
		switch (index) {
			case 0:
				mainMenuController.ShowMainPanel();
				gameObject.SetActive(false);
				break;
			case 1:
				StartCoroutine(mainMenuController.NewGameAfterWarning());
				break;
		}
		SoundManager.GetInstance().PlaySound(SoundTypes.BTN_SELECT);
	}
}
