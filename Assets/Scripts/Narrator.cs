using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Narrator {
	private GameObject textPanel;
	private TextMeshProUGUI text;
	private Animator panelAnim;
	private int animShow = Animator.StringToHash("show");
	private int animHide = Animator.StringToHash("hide");
	
	public Narrator(GameObject textPanel, TextMeshProUGUI text) {
		this.textPanel = textPanel;
		this.text = text;
		this.panelAnim = textPanel.GetComponent<Animator>();
	}

	public void ShowText(string sentence) {
		text.text = sentence;
		panelAnim.SetTrigger(animShow);
	}

	public void Hide() {
		text.text = "";
		panelAnim.SetTrigger(animHide);
	}
}
