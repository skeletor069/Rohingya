﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Facility : MonoBehaviour {
	protected string facilityName;
	protected string facilityDescription;
//	protected string[] optionNames = new string[3];
	public int openningMinute = 0;
	public int closingMinute = 0;

	private bool facilityActive;
	private bool jobActive;
	private bool showInventory;

	private BoxCollider collider;

	private int interactionCount = 0;
	private int lastInteractionDay = 0;

	//private GameObject facilityIcon;
	protected FacilityBtnData[] btnDatas = new FacilityBtnData[3];
	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider>();
		//facilityIcon = transform.GetChild(0).gameObject;
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
	
	public bool FacilityActive {
		get { return facilityActive; }
		set {
			facilityActive = value;
			//facilityIcon.SetActive(value);
		}
	}

	public void CheckFacilityActive(int minutesGone) {
		facilityActive = (minutesGone >= openningMinute && minutesGone < closingMinute);
	}

	private void OnTriggerEnter(Collider other) {
		//Debug.Log(facilityActive);
		if (other.tag == "Player") {
//			Debug.Log("enter");
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

//	public string[] GetOptionNames() {
//		return optionNames;
//	}

	public FacilityBtnData[] GetActionBtnsData() {
		return btnDatas;
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

	protected void InteractionDone() {
		interactionCount++;
		interactionCount = Mathf.Min(interactionCount, 15);
		lastInteractionDay = GameController.GetInstance().World.GetDaysGone();
	}

	protected void JobDone() {
		interactionCount++;
		lastInteractionDay = GameController.GetInstance().World.GetDaysGone();
	}

	public int GetRelationStatus() {
		int diff = GameController.GetInstance().World.GetDaysGone() - lastInteractionDay;
		if (diff > 1)
			return Mathf.Min(Mathf.Max(0, interactionCount - diff + 1), 15);
		else
			return interactionCount;
	}

	public bool IsRelationMax() {
		return interactionCount == 15;
	}
}
