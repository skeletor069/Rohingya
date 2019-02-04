using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	private Animator saveBtn;
	private Animator resumeBtn;
	private Animator soundBtn;
	private Animator controlBtn;
	private Animator quitBtn;

	private int animActive = Animator.StringToHash("active");
	private int animInactive = Animator.StringToHash("inactive");

	private int selectedIndex = 1;
	
	Animator[] optionBtns = new Animator[5];

	private static PauseMenu instance;
	
	void Awake () {
		optionBtns[0] = saveBtn = transform.GetChild(1).GetComponent<Animator>();
		optionBtns[1] = resumeBtn = transform.GetChild(2).GetComponent<Animator>();
		optionBtns[2] = soundBtn = transform.GetChild(3).GetComponent<Animator>();
		optionBtns[3] = controlBtn = transform.GetChild(4).GetComponent<Animator>();
		optionBtns[4] = quitBtn = transform.GetChild(5).GetComponent<Animator>();

		instance = this;
	}

	void Start() {
		gameObject.SetActive(false);
	}

	public static PauseMenu GetInstance() {
		return instance;
	}

	public void ShowPauseMenu() {
		gameObject.SetActive(true);
		optionBtns[selectedIndex].SetTrigger(animInactive);
		selectedIndex = 1;
		optionBtns[selectedIndex].SetTrigger(animActive);
	}

	void ClosePauseMenu() {
		SlumWorld.GetInstance().PauseMenuClosed();
		gameObject.SetActive(false);
	}

	void Update () {
		if (Input.anyKey) {
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				if (selectedIndex != 0) {
					optionBtns[selectedIndex].SetTrigger(animInactive);
					selectedIndex--;
					optionBtns[selectedIndex].SetTrigger(animActive);
				}
			}
		
			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				if (selectedIndex != 4) {
					optionBtns[selectedIndex].SetTrigger(animInactive);
					selectedIndex++;
					optionBtns[selectedIndex].SetTrigger(animActive);
				}
			}

			if (Input.GetKeyDown(KeyCode.Return)) {
				if(selectedIndex == 1)
					ClosePauseMenu();
			}

			if (Input.GetKeyDown(KeyCode.Escape)) {
				ClosePauseMenu();
			}
		}

		
	}
}
