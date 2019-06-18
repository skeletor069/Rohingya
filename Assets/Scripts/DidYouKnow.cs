using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DidYouKnow : MonoBehaviour {
	private TextMeshProUGUI infoText;
	private Animator anim;
	private int animShow = Animator.StringToHash("show");
	private int animHide = Animator.StringToHash("hide");
	WaitForSeconds wait2s = new WaitForSeconds(2);
	WaitForSeconds wait5s = new WaitForSeconds(7);
	List<string> information = new List<string>();
	
	void Awake () {
		infoText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		anim = GetComponent<Animator>();
		PopulateInfoList();
		StartCoroutine(InfoShowRoutine());
	}

	void PopulateInfoList() {
		information.Add("There were an estimated 1 million Rohingya living in Myanmar before the 2016–17 crisis");
		information.Add("Rohingya population is denied citizenship under the 1982 Myanmar nationality law");
		information.Add("According to Human Rights Watch, the 1982 laws \"effectively deny to the Rohingya the possibility of acquiring a nationality\"");
		information.Add("The Rohingya have faced military crackdowns in 1978, 1991–1992,[36] 2012, 2015, 2016–2017 and particularly in 2017-2018");
		information.Add("UN officials and HRW have described Myanmar's persecution of the Rohingya as ethnic cleansing");
		information.Add("Since 2015, over 900,000 Rohingya refugees have fled to southeastern Bangladesh alone");
		information.Add("Kutupalong refugee camp in Cox's Bazar, Bangladesh. The camp is one of three, which house up to 300,000 Rohingya people fleeing inter-communal violence in Myanmar.");
		information.Add("Thousands of Rohingyas have also fled to Thailand. There have been charges that Rohingyas were shipped and towed out to open sea from Thailand");
		information.Add("The Rohingya people have been described as \"one of the world's least wanted minorities\" and \"some of the world's most persecuted people\"");
		information.Add("Myanmar's Ministry of Foreign Affairs stated in a press release, \"In actual fact, although there are (135) national races living in Myanmar today, the so-called Rohingya people is not one of them.\"sss");
		information.Add("The Rohingya were deprived of the right to free movement and the right to higher education");
		information.Add("Burma has had different types of citizenship. Citizens possessed red identity cards; Rohingyas were given white cards, essentially labeling them as foreigners in Burma");
		information.Add("They were subjected to routine forced labour. (Typically, a Rohingya man will have to give up one day a week to work on military or government projects, and one night a week for sentry duty.)");
		information.Add("According to Amnesty International, the Rohingya have suffered from human rights violations under the military dictatorship since 1978, and many of them have fled to neighbouring Bangladesh as a result");
	}

	IEnumerator InfoShowRoutine() {
		int index = Random.Range(0, information.Count);
		while (true) {
			
			infoText.text = information[index];
			anim.SetTrigger(animShow);
			yield return wait5s;
			anim.SetTrigger(animHide);
			index++;
			index = index % information.Count;
			yield return wait2s;

		}
	}

}
