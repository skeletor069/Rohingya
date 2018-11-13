﻿using System.Collections.Generic;
using UnityEngine;

public class World
{
	private int daysGone = 0;
	private float minutesGone = 0;

	private float accum = 0;

	private Hero hero;
	List<ITickerSubscriber> sceneTickerSubscribers;
	private DataHUD dataHud;
	
	
	public World () {
		hero = new Hero();
		sceneTickerSubscribers = new List<ITickerSubscriber>();
		dataHud = new DataHUD();
		minutesGone = 600;
	}
	
	public void Update (float deltaTime)
	{
		UpdateClock(deltaTime);
		hero.Update(deltaTime);
	}

	public string GetDaysText()
	{
		int tempDaysGone = daysGone;
		int year = tempDaysGone / 365;
		tempDaysGone = tempDaysGone % 365;
		int month = tempDaysGone / 12;
		int day = tempDaysGone % 12;
		return "year " + year + ", month " + month + ", day " + day;

	}

	public string GetTimeText()
	{
		int tempMinutesGone = (int)minutesGone;
		int hour = tempMinutesGone / 60;
		int minute = tempMinutesGone % 60;
		return ((hour%12 < 10)?"0":"")+(hour%12) + ":" +((minute < 10)?"0":"")+ minute + " " + ((hour < 12) ? "am" : "pm");
	}

	void UpdateClock(float deltaTime)
	{
		minutesGone += deltaTime;
		if (minutesGone >= 1440)
		{
			minutesGone = minutesGone - 1440;
			daysGone++;
		}
		
	}

	public void RegisterSceneTickerSubscribers()
	{
		
	}

	public DataHUD GetDataHud()
	{
		dataHud.health = hero.Health;
		dataHud.energy = hero.Energy;
		dataHud.food = hero.Food;
		dataHud.wallet = hero.Money;
		dataHud.heroSkill = hero.GetHeroSkillName();
		dataHud.daysText = GetDaysText();
		dataHud.timeText = GetTimeText();
		return dataHud;
	}

	public void ActionPerformed(List<AttributeToken> tokens, float minutes) {
		Update(minutes);
		hero.UpdateAttributes(tokens);
	}
}