using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour {
	private Image itemImage;
	private TextMeshProUGUI itemCountText;
	
	void Awake () {
		itemImage = transform.GetChild(0).GetComponent<Image>();
		itemCountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
	}

	public void SetData(Sprite sprite, int count) {
		itemImage.enabled = true;
		itemCountText.enabled = true;
		itemImage.sprite = sprite;
		itemCountText.text = count.ToString();
	}

	public void SetEmpty() {
		itemImage.enabled = false;
		itemCountText.enabled = false;
	}
}
