using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mastering : MonoBehaviour {
	private void OnGUI() {
		if(GUI.Button(new Rect(0f,0f, 50,20), "day"))
		{
			Action(SoundTypes.DAY_BG);
				
		}
		if(GUI.Button(new Rect(50f,0f, 50,20), "morning"))
		{
			Action(SoundTypes.MORNING_BG);
		}
		if(GUI.Button(new Rect(100f,0f, 50,20), "night"))
		{
			Action(SoundTypes.NIGHT_BG);
		}
		
		if(GUI.Button(new Rect(0f,30f, 50,20), "left foot"))
		{
			Action(SoundTypes.FOOT_LEFT);	
		}
		if(GUI.Button(new Rect(50f,30f, 50,20), "right foot"))
		{
			Action(SoundTypes.FOOT_RIGHT);
		}
		
		if(GUI.Button(new Rect(0f,60f, 50,20), "choose"))
		{
			Action(SoundTypes.BTN_CHOOSE);	
		}
		if(GUI.Button(new Rect(50f,60f, 50,20), "select"))
		{
			Action(SoundTypes.BTN_SELECT);
		}
		
		if(GUI.Button(new Rect(0f,90f, 50,20), "heart"))
		{
			Action(SoundTypes.HEART_BEAT);
				
		}
		if(GUI.Button(new Rect(50f,90f, 50,20), "breath"))
		{
			Action(SoundTypes.BREATH);
		}
		if(GUI.Button(new Rect(100f,90f, 50,20), "hungry"))
		{
			Action(SoundTypes.HUNGRY);
		}
	}

	void Action(SoundTypes soundType) {
		if(!SoundManager.GetInstance().IsPlaying(soundType))
			SoundManager.GetInstance().PlaySound(soundType);
		else 
			SoundManager.GetInstance().StopSound(soundType);
	}
}
