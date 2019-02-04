using System.Collections.Generic;
using UnityEngine;

public class World
{
	private int daysGone = 0;
	private float minutesGone = 0;

	private float accum = 0;

	private Hero hero;
	private Inventory inventory;
	List<ITickerSubscriber> sceneTickerSubscribers;
	private DataHUD dataHud;
	
	
	public World () {
		hero = new Hero();
		inventory = new Inventory();
		sceneTickerSubscribers = new List<ITickerSubscriber>();
		dataHud = new DataHUD();
		minutesGone = 0;
	}

	public Inventory Inventory {
		get { return inventory; }
		set { inventory = value; }
	}

	public Hero Hero {
		get { return hero; }
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

	public float GetMinutesGone() {
		return minutesGone;
	}

	public float GetHour() {
		return minutesGone / 60;
	}

	public int GetDaysGone() {
		return daysGone;
	}

	public void SetMinutesGone(float minutesGone) {
		this.minutesGone = minutesGone;
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
		//Update(minutes);
		hero.UpdateAttributes(tokens);
	}
}
