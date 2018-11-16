using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Facility : MonoBehaviour {
	protected string facilityName;
	protected string facilityDescription;
	protected string[] optionNames = new string[3];

	private bool jobActive;
	private bool showInventory;
	// Use this for initialization
	void Awake () {
		InitiateData();
	}

	public bool JobActive {
		get { return jobActive; }
		set { jobActive = value; }
	}

	public bool ShowInventory {
		get { return showInventory; }
		set { showInventory = value; }
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			SlumWorld.GetInstance().ShowInteractionIcon(this);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			SlumWorld.GetInstance().HideInteractionIcon();
		}
	}

	public abstract void InitiateData();
	public abstract void Action1();
	public abstract void Action2();
	public abstract void Action3();
	public abstract void DoJob();

	public string GetFacilityName() {
		return facilityName;
	}

	public string GetFacilityDescription() {
		return facilityDescription;
	}

	public string[] GetOptionNames() {
		return optionNames;
	}

	public void ExecuteAction(int index) {
		switch (index) {
			case 0:
				Action1();
				break;
			case 1:
				Action2();
				break;
			case 2:
				Action3();
				break;
		}
	}
}
