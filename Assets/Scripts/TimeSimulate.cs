using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSimulate : MonoBehaviour {

	public Animator dayNightAnim;
	private int animTime = Animator.StringToHash("time");
	public int hour;
	
	// Update is called once per frame
	void Update () {
		hour = Mathf.Min(23, hour);
		hour = Mathf.Max(0, hour);
		dayNightAnim.SetFloat(animTime, hour);
	}
}
