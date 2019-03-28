using System.Collections.Generic;
using UnityEditor.Audio;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundTypes {
	EAT_JUICY, EAT_CRUNCHY, DRINK, SLEEP, EAT_MEAL, SEARCH, SELL, WORK_FOOD, WORK_WORKSHOP, DOOR_OPEN, DOOR_CLOSE, BUTTON_SELECT,
	MENU_BTN_SELECT, MENU_BG, DAY_BG, NIGHT_BG, MORNING_BG, WORK_BOTTLE, WORK_CAN, WORK_PAPER, BREATH, HUNGRY, HEART_BEAT, PICK_UP
}

public class SoundManager : MonoBehaviour {
	
	public AudioSource dayChannel;
	public AudioSource nightChannel;
	public AudioSource morningChannel;
	public AudioSource eatMeal;
	public AudioSource eatBanana;
	public AudioSource eatBiscuit;
	public AudioSource drinkCoffee;
	public AudioSource sleeping;
	public AudioSource stepL;
	public AudioSource stepR;
	public AudioSource doorOpen;
	public AudioSource doorClose;
	public AudioSource buttonSelect;
	public AudioSource menuButtonSelect;
	public AudioSource menuBG;
	public AudioSource dayBG;
	public AudioSource nightBG;
	public AudioSource morningBG;
	public AudioSource workBottle;
	public AudioSource workCan;
	public AudioSource workPaper;
	public AudioSource workFood;
	public AudioSource breath;
	public AudioSource hungry;
	public AudioSource heartBeat;
	public AudioSource pickUp;
	public AudioSource search;
	public AudioSource sell;
	
	
	private AudioMixer mixer;
	public AudioMixerSnapshot backgroundActiveSnapshot;
	public AudioMixerSnapshot performActionSnapshot;
	public AudioMixerSnapshot sleepSnapshot;
	private Dictionary<SoundTypes, AudioSource> audioSources;
	public AudioMixerGroup ambientGroup;

	private static SoundManager instance;

	void Awake() {
		instance = this;
		audioSources = new Dictionary<SoundTypes, AudioSource>();
		audioSources.Add(SoundTypes.EAT_JUICY, eatBanana);
		audioSources.Add(SoundTypes.EAT_CRUNCHY, eatBiscuit);
		audioSources.Add(SoundTypes.DRINK, drinkCoffee);
		audioSources.Add(SoundTypes.SLEEP, sleeping);
		audioSources.Add(SoundTypes.EAT_MEAL, eatMeal);
		audioSources.Add(SoundTypes.SEARCH, search);
		audioSources.Add(SoundTypes.SELL, sell);
		
		audioSources.Add(SoundTypes.DOOR_OPEN, doorOpen);
		audioSources.Add(SoundTypes.DOOR_CLOSE, doorClose);
		audioSources.Add(SoundTypes.BUTTON_SELECT, buttonSelect);
		audioSources.Add(SoundTypes.MENU_BTN_SELECT, menuButtonSelect);
		audioSources.Add(SoundTypes.MENU_BG, menuBG);
		audioSources.Add(SoundTypes.DAY_BG, dayBG);
		audioSources.Add(SoundTypes.NIGHT_BG, nightBG);
		audioSources.Add(SoundTypes.MORNING_BG, morningBG);
		audioSources.Add(SoundTypes.WORK_BOTTLE, workBottle);
		audioSources.Add(SoundTypes.WORK_CAN, workCan);
		audioSources.Add(SoundTypes.WORK_PAPER, workPaper);
		audioSources.Add(SoundTypes.WORK_FOOD, workFood);
		audioSources.Add(SoundTypes.BREATH, breath);
		audioSources.Add(SoundTypes.HUNGRY, hungry);
		audioSources.Add(SoundTypes.HEART_BEAT, heartBeat);
		audioSources.Add(SoundTypes.PICK_UP, pickUp);
		
	}

	private void Start() {
		//backgroundActiveSnapshot.TransitionTo(2);
		DontDestroyOnLoad(gameObject);
	}

	public static SoundManager GetInstance() {
		return instance;
	}

	public void PlayEatBiscuitSound() {
		eatBiscuit.Play();
	}

	public void PlayEatBananaSound() {
		eatBanana.Play();
	}
	
	public void PlayDrinkCoffeeSound() {
		drinkCoffee.Play();
	}

	public void PlaySound(SoundTypes soundType) {
		if(audioSources.ContainsKey(soundType))
			audioSources[soundType].Play();
	}

	public void StopSound(SoundTypes soundType) {
		if(audioSources.ContainsKey(soundType))
			audioSources[soundType].Stop();
	}

	public bool IsPlaying(SoundTypes soundType) {
		if(audioSources.ContainsKey(soundType))
			return audioSources[soundType].isPlaying;
		return false;
	}

	public void PlayFootStep(bool leftFoot) {
		if(leftFoot)
			stepL.Play();
		else 
			stepR.Play();
	}

	public void SwitchToActionMode() {
		performActionSnapshot.TransitionTo(.15f);
	}

	public void SwitchToNormalMode() {
		backgroundActiveSnapshot.TransitionTo(2);
	}

	public void SwitchToSleepMode() {
		sleepSnapshot.TransitionTo(2);
	}

	public void WarningMode() {
		mixer.SetFloat("ambience_cutoff", 200);
	}

	public void NormalMode() {
		mixer.SetFloat("ambience_cutoff", 8000);
	}

	public void UpdateAmbientSound(int minutesGone) {
		// night-morning transition 4.30 - 5.30
		// morning-day transition 7.00 - 8.00
		// day sound - 0% at 7.00, 100% at 11.00 - 15.00, 0% at 20.00 
		// night sound - 0% at 18.00, 100% at 20.00, 100% at 4.30, 0% at 5.30
		// morning sound - 0% at 4.30, 100% at 5.30 - 6.30, 0% at 8.00,

		if (minutesGone > 1200 || minutesGone < 420) { // from 20:00 to 07:00
			dayChannel.volume = 0;
		}else if (minutesGone > 660 && minutesGone < 900) { // from 11:00 to 15:00
			dayChannel.volume = 1;
		}
		else {
			if(minutesGone >= 900) // 15:00 to 20:00
				dayChannel.volume = (1200 - minutesGone) / 300f;
			else { // 7:00 to 11:00
				dayChannel.volume = (minutesGone - 420) / 240f;
			}
		}
		
		if (minutesGone > 1200 || minutesGone < 270) { // from 20:00 to 04:30
			nightChannel.volume = 1;
		}else if (minutesGone > 330 && minutesGone < 960) { // from 05:30 to 16:00
			nightChannel.volume = 0;
		}
		else {
			if(minutesGone >= 960) // 16:00 to 20:00
				nightChannel.volume = (minutesGone - 960) / 240f;
			else { // 4:30 to 5.30
				nightChannel.volume = (330 - minutesGone) / 60f;
			}
		}
		
		if (minutesGone > 480 || minutesGone < 270) { // from 8:00 to 04:30
			morningChannel.volume = 0;
		}else if (minutesGone > 330 && minutesGone < 390) { // from 05:30 to 6:30
			morningChannel.volume = 1;
		}
		else {
			if(minutesGone >= 390) // 6:30 to 8:00
				morningChannel.volume = (480 - minutesGone) / 90;
			else { // 4:30 to 5.30
				morningChannel.volume = (minutesGone - 270) / 60;
			}
		}
	}
}
