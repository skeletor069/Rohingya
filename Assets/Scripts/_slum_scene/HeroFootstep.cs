using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFootstep : MonoBehaviour {

	public void FootStepL() {
		SoundManager.GetInstance().PlayFootStep(true);
	}

	public void FootStepR() {
		SoundManager.GetInstance().PlayFootStep(false);
	}
}
