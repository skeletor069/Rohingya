using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUI : MonoBehaviour {

	public Image fillBar;

	public void SetValue(int attributeValue) {
		fillBar.fillAmount = attributeValue * .01f;
	}
}
