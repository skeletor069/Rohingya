using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour, ITabMenuListener {
	private Animator saveBtn;
	private Animator resumeBtn;
	private Animator soundBtn;
	private Animator controlBtn;
	private Animator quitBtn;

	private int animActive = Animator.StringToHash("active");
	private int animInactive = Animator.StringToHash("inactive");
	private int animInteract = Animator.StringToHash("interact");

	private int selectedIndex = 1;
	
	Animator[] optionBtns = new Animator[5];

	private static PauseMenu instance;
	private GameObject mainPanel, quitGamePanel, controlPanel;
	private TabbedMenu quitWarningMenu;
	private bool isMainSelection = true;
	private bool isControllerSelection = false;
	
	void Awake () {
		mainPanel = transform.GetChild(1).gameObject;
		quitGamePanel = transform.GetChild(2).gameObject;
		controlPanel = transform.GetChild(3).gameObject;
		quitWarningMenu = transform.GetChild(2).GetChild(1).GetComponent<TabbedMenu>();
		optionBtns[0] = saveBtn = transform.GetChild(1).GetChild(0).GetComponent<Animator>();
		optionBtns[1] = resumeBtn = transform.GetChild(1).GetChild(1).GetComponent<Animator>();
		optionBtns[2] = soundBtn = transform.GetChild(1).GetChild(3).GetComponent<Animator>();
		optionBtns[3] = controlBtn = transform.GetChild(1).GetChild(4).GetComponent<Animator>();
		//optionBtns[4] = quitBtn = transform.GetChild(5).GetComponent<Animator>();

		instance = this;
	}

	void Start() {
		quitWarningMenu.Initiate(this, 0);
		quitWarningMenu.InteractionActive = true;
		quitGamePanel.SetActive(false);
		gameObject.SetActive(false);
	}

	public static PauseMenu GetInstance() {
		return instance;
	}

	public void ShowPauseMenu(int targetIndex=1) {
		gameObject.SetActive(true);
		mainPanel.SetActive(true);
		quitGamePanel.SetActive(false);
		controlPanel.SetActive(false);
		isMainSelection = true;
		if(selectedIndex != targetIndex)
			optionBtns[selectedIndex].SetTrigger(animInactive);
		selectedIndex = targetIndex;
		optionBtns[selectedIndex].SetTrigger(animActive);
	}

	void ClosePauseMenu() {
		SoundManager.GetInstance().PlaySound(SoundTypes.BTN_SELECT);
		SlumWorld.GetInstance().PauseMenuClosed();
		gameObject.SetActive(false);
	}

	void Update () {
		if (true) {
			
//			if (isControllerSelection && Input.anyKeyDown) {
//				isControllerSelection = false;
//				isMainSelection = true;
//				mainPanel.SetActive(true);
//				controlPanel.SetActive(false);
//				optionBtns[selectedIndex].SetTrigger(animActive);
//			}

			if (isControllerSelection && Input.GetKeyUp(KeyCode.Return)) {
				mainPanel.SetActive(false);
				controlPanel.SetActive(true);
				isControllerSelection = false;
			}

			if (isMainSelection) {
				if (Input.GetKeyDown(KeyCode.UpArrow)) {
					if (selectedIndex != 0) {
						SoundManager.GetInstance().PlaySound(SoundTypes.BTN_CHOOSE);
						optionBtns[selectedIndex].SetTrigger(animInactive);
						selectedIndex--;
						optionBtns[selectedIndex].SetTrigger(animActive);
					}
				}
		
				if (Input.GetKeyDown(KeyCode.DownArrow)) {
					if (selectedIndex != 3) {
						SoundManager.GetInstance().PlaySound(SoundTypes.BTN_CHOOSE);
						optionBtns[selectedIndex].SetTrigger(animInactive);
						selectedIndex++;
						optionBtns[selectedIndex].SetTrigger(animActive);
					}
				}

				if (Input.GetKeyDown(KeyCode.Return)) {
					optionBtns[selectedIndex].SetTrigger(animInteract);
					if (selectedIndex == 0) {
						SoundManager.GetInstance().PlaySound(SoundTypes.BTN_SELECT);
						Vector3 heroPosition = SlumWorld.GetInstance().player.transform.position;
						GameController.GetInstance().World.SetHeroPositionAtSave(heroPosition.x, heroPosition.y, heroPosition.z);
						GameController.GetInstance().SaveGame();
					}
					
					else if(selectedIndex == 1)
						ClosePauseMenu();
					else if (selectedIndex == 2) {
						SoundManager.GetInstance().PlaySound(SoundTypes.BTN_SELECT);
						isMainSelection = false;
						isControllerSelection = true;
//						StartCoroutine(SetControlSelection());
						Debug.Log("show Controller");
						
					}else if (selectedIndex == 3) {
						SoundManager.GetInstance().PlaySound(SoundTypes.BTN_SELECT);
						mainPanel.SetActive(false);
						quitGamePanel.SetActive(true);
						isMainSelection = false;
						quitWarningMenu.Initiate(this,0);
					}
				}
				
				

				if (Input.GetKeyDown(KeyCode.Escape)) {
					ClosePauseMenu();
				}
			}
//			else if(isControllerSelection) {
//				Debug.Log("any key pressed");
//				isControllerSelection = false;
//				isMainSelection = true;
//				mainPanel.SetActive(true);
//				controlPanel.SetActive(false);
//				optionBtns[selectedIndex].SetTrigger(animActive);
//			}

			

		}

		
	}
//
//	IEnumerator SetControlSelection() {
//		Debug.Log("controller selection");
//		yield return new WaitForSeconds(1.5f);
//		isControllerSelection = true;
//		WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
//
//		while (true) {
//			if(Input.anyKey)
//				break;
//			yield return endOfFrame;
//		}
//
//		isControllerSelection = false;
//		isMainSelection = true;
//		mainPanel.SetActive(true);
//		controlPanel.SetActive(false);
//	}

	public void ChangedMenu(int index) {
		SoundManager.GetInstance().PlaySound(SoundTypes.BTN_CHOOSE);
	}

	public void SelectedMenu(int index) {
		if (index == 0) {
			// go back
			quitGamePanel.SetActive(false);
			mainPanel.SetActive(true);
			isMainSelection = true;
			optionBtns[selectedIndex].SetTrigger(animActive);
		}
		else {
			Time.timeScale = 1;
			GameController.GetInstance().GoToMainMenu();
		}
		SoundManager.GetInstance().PlaySound(SoundTypes.BTN_SELECT);
	}
}
