using UnityEngine;
using UnityEngine.UI;

public class CancelBtn : MonoBehaviour, FacilityDescriptionBtn {
	private Image highlight;
	
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
	
	public void SetVisible(bool active) {
		gameObject.SetActive(active);
	}
}
