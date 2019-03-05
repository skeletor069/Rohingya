using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobBtn : MonoBehaviour, FacilityDescriptionBtn {
	private Image highlight;
	public Slider relationSlider;
	public TextMeshProUGUI alertText;
	
	void Awake () {
		
		highlight = transform.GetChild(1).GetComponent<Image>();
	}
	
	public void SetBtnName(string btnName) {
		highlight = transform.GetChild(1).GetComponent<Image>();
	}

	public void SetBtnStateAtive(bool active) {
		highlight.enabled = active;
	}

	public void SetBtnData(FacilityBtnData btnData) {
		
	}

	public void SetRelationData(int relation) {
		relationSlider.value = relation;
		if (relation != relationSlider.maxValue)
			alertText.enabled = true;
		else
			alertText.enabled = false;
	}

	public void SetVisible(bool active) {
		gameObject.SetActive(active);
	}
}
