using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheatCodeController : MonoBehaviour {
	private int activateBtnCount = 0;
	private bool cheatCodeActive = false;
	private bool inputFieldActive = false;
	public TMP_InputField cheatInput;
	
	// Use this for initialization
	void Start () {
		cheatInput.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.C)) {
			activateBtnCount++;
			if (activateBtnCount == 5) {
				activateBtnCount = 0;
				cheatCodeActive = !cheatCodeActive;
				if (!cheatCodeActive) {
					inputFieldActive = false;
					cheatInput.gameObject.SetActive(false);
				}
			}
		}

		if (cheatCodeActive && Input.GetKeyUp(KeyCode.LeftShift)) {
			// toggle ui
			inputFieldActive = !inputFieldActive;
			cheatInput.gameObject.SetActive(inputFieldActive);
			
		}

		if (cheatCodeActive && inputFieldActive && Input.GetKeyUp(KeyCode.Return)) {
			ProcessCheatCode(cheatInput.text);
			inputFieldActive = false;
			cheatInput.gameObject.SetActive(false);
		}
	}

	void ProcessCheatCode(string cheatCode) {
		string[] parts = cheatCode.Split(' ');

		if (parts.Length == 2 && parts[0].Equals("health")) {
			try {
				int value = int.Parse(parts[1]);
				if (value >= 0 && value <= 100) {
					GameController.GetInstance().World.Hero.Health = value;
				}
			}
			catch (Exception e) {
				Console.WriteLine(e);
				throw;
			}
		}
		
		if (parts.Length == 2 && parts[0].Equals("energy")) {
			try {
				int value = int.Parse(parts[1]);
				if (value >= 0 && value <= 100) {
					GameController.GetInstance().World.Hero.Energy = value;
				}
			}
			catch (Exception e) {
				Console.WriteLine(e);
				throw;
			}
		}
		
		if (parts.Length == 2 && parts[0].Equals("food")) {
			try {
				int value = int.Parse(parts[1]);
				if (value >= 0 && value <= 100) {
					GameController.GetInstance().World.Hero.Food = value;
				}
			}
			catch (Exception e) {
				Console.WriteLine(e);
				throw;
			}
		}
		
		if (parts.Length == 2 && parts[0].Equals("money")) {
			try {
				int value = int.Parse(parts[1]);
				if (value >= 0) {
					GameController.GetInstance().World.Hero.Money = value;
				}
			}
			catch (Exception e) {
				Console.WriteLine(e);
				throw;
			}
		}
		
		if (parts.Length == 3 && parts[0].Equals("time")) {
			try {
				int hour = int.Parse(parts[1]);
				int minute = int.Parse(parts[2]);
				int minutesGone = hour * 60 + minute;
				GameController.GetInstance().World.SetMinutesGone(minutesGone);
			}
			catch (Exception e) {
				Console.WriteLine(e);
				throw;
			}
		}
		
		
	}
}
