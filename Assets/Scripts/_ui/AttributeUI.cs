using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUI : MonoBehaviour {

	public Image fillBar;
	public Image fill;
	public Image icon;
	public TextMeshProUGUI title;

	

	public void SetValue(int attributeValue) {
		fill.fillAmount = attributeValue * .01f;
	}

	public void Hide() {
		//icon.enabled = false;
		fill.enabled = false;
		fillBar.enabled = false;
		title.enabled = false;
	}

	public void Show() {
		//icon.enabled = true;
		fill.enabled = true;
		fillBar.enabled = true;
		title.enabled = true;
	}
}
