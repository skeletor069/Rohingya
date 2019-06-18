using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct FacilityBtnData {
	public string name;
	public int time;
	public string[] changesText;
	public Color[] changesColor;
	public string notificationText;
	public SoundTypes actionSound;
}

public interface FacilityDescriptionBtn {


	void SetBtnName(string btnName);
	void SetBtnStateAtive(bool active);
	void SetVisible(bool active);
	void SetBtnData(FacilityBtnData btnData);
	void SetProgress(float fillAmount);
}
