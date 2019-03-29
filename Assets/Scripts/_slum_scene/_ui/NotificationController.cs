using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationController : MonoBehaviour {

	public Animator animator;

	private TextMeshProUGUI notificationText;
	private static NotificationController instance;
	private int animShow = Animator.StringToHash("show");
	private int animHide = Animator.StringToHash("hide");
	WaitForSeconds wait1 = new WaitForSeconds(1);
	
	void Awake () {
		instance = this;
		notificationText = animator.gameObject.GetComponent<TextMeshProUGUI>();
	}

	public static NotificationController GetInstance() {
		return instance;
	}

	public void ShowToolTip(string text) {
		StopAllCoroutines();
		notificationText.text = text;
		animator.SetTrigger(animShow);
		StartCoroutine(WaitAndDissolve());
	}

	IEnumerator WaitAndDissolve() {
		yield return wait1;
		animator.SetTrigger(animHide);
	}

	public void ShowText(string text) {
		StopAllCoroutines();
		notificationText.text = text;
		animator.SetTrigger(animShow);
	}

	public void HideText() {
		notificationText.text = "";
		animator.SetTrigger(animHide);
	}



}
