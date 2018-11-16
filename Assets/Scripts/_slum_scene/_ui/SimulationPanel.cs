using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using TMPro;
using UnityEngine;

public class SimulationPanel : MonoBehaviour {
	private TextMeshProUGUI displayText;
	private Animator animator;
	private WaitForSeconds animationDelay;
	private WaitForSeconds dotDelay;
	private int animShow = Animator.StringToHash("show");
	private int animHide = Animator.StringToHash("hide");
	
	void Awake () {
		animator = GetComponent<Animator>();
		displayText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		animationDelay = new WaitForSeconds(.5f);
		dotDelay = new WaitForSeconds(.5f);
	}

	public IEnumerator ShowSimulationDisplay(string text) {
		displayText.text = "";
		animator.SetTrigger(animShow);
		yield return animationDelay;
		displayText.text = text;
		for (int i = 0; i < 3; i++) {
			yield return dotDelay;
			displayText.text += " .";
		}
		animator.SetTrigger(animHide);
		yield return animationDelay;
		// callback
	}


}
