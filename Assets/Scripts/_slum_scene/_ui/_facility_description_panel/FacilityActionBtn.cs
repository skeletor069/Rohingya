using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FacilityActionBtn : MonoBehaviour,FacilityDescriptionBtn {
	private Image highlight;
	private TextMeshProUGUI btnText;
	private TextMeshProUGUI timeText;
	private TextMeshProUGUI[] changeTexts = new TextMeshProUGUI[2];
	private TextMeshProUGUI notificationText;
	private Image progressImage;
	
	void Awake () {
		btnText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		highlight = transform.GetChild(1).GetComponent<Image>();
		timeText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
		changeTexts[0] = transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
		changeTexts[1] = transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
		notificationText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
		progressImage = transform.GetChild(5).GetComponent<Image>();
	}
	
	public void SetBtnName(string btnName) {
		btnText.text = btnName;
	}

	public void SetBtnStateAtive(bool active) {
		highlight.enabled = active;
	}

	public void SetVisible(bool active) {
		gameObject.SetActive(active);
	}

	public void SetBtnData(FacilityBtnData btnData) {
		btnText.text = btnData.name;
		
		if(btnData.time > 60)
			timeText.text = "Time " + (btnData.time/60) + "h";
		else 
			timeText.text = "Time " + btnData.time + "m";
		changeTexts[0].text = btnData.changesText[0];
		changeTexts[0].color = btnData.changesColor[0];

		if (btnData.changesText[1] != null) {
			changeTexts[1].gameObject.SetActive(true);
			changeTexts[1].text = btnData.changesText[1];
			changeTexts[1].color = btnData.changesColor[1];
		}
		else {
			changeTexts[1].gameObject.SetActive(false);
		}

		notificationText.text = btnData.notificationText;
	}

	public void SetProgress(float fillAmount) {
		progressImage.fillAmount = fillAmount;
	}


}
