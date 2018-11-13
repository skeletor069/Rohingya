using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FacilityDescriptionBtn : MonoBehaviour {
	private Image highlight;
	private TextMeshProUGUI btnText;
	
	// Use this for initialization
	void Awake () {
		btnText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		highlight = transform.GetChild(1).GetComponent<Image>();
	}

	public void SetBtnName(string btnName) {
		btnText.text = btnName;
	}

	public void SetBtnStateAtive(bool active) {
		highlight.enabled = active;
	}
}
