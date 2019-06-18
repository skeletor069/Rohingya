using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroSkills
{
	ENTRY_LEVEL, EXPERIENCED, PROFESSIONAL
}

public enum HeroAttributes {
	HEALTH, ENERGY, FOOD, MONEY
}

[System.Serializable]
public class AttributeToken {
	public HeroAttributes attribute;
	public float amount;

	public AttributeToken(HeroAttributes attribute, float amount) {
		this.attribute = attribute;
		this.amount = amount;
	}
}

[System.Serializable]
public struct HeroConfig {
	public float foodPerMinute;
	public float energyPerMinute;
}

[System.Serializable]
public class Hero {
	private HeroConfig heroConfig;
	private HeroSkills heroSkill = HeroSkills.ENTRY_LEVEL;
	private float hungerPerTick = 100f / 360;
	private float healthReducePerTick = 100f / 3000;
	private Dictionary<HeroSkills, string> heroSkillsName;
	Dictionary<HeroAttributes, float> heroAttributes;
	

	public Hero()
	{
		heroSkillsName = new Dictionary<HeroSkills, string>();
		heroSkillsName.Add(HeroSkills.ENTRY_LEVEL, "Entry Level");
		heroSkillsName.Add(HeroSkills.EXPERIENCED, "Experienced");
		heroSkillsName.Add(HeroSkills.PROFESSIONAL, "Professional");
		
		heroAttributes = new Dictionary<HeroAttributes, float>();
		heroAttributes.Add(HeroAttributes.HEALTH, 10);
		heroAttributes.Add(HeroAttributes.ENERGY, 0);
		heroAttributes.Add(HeroAttributes.FOOD, 0);
		heroAttributes.Add(HeroAttributes.MONEY, 0);
	}

	public int Health
	{
		get { return (int)heroAttributes[HeroAttributes.HEALTH]; }
		set { heroAttributes[HeroAttributes.HEALTH] = value; }
	}

	public int Energy
	{
		get { return (int)heroAttributes[HeroAttributes.ENERGY]; }
		set { heroAttributes[HeroAttributes.ENERGY] = value; }
	}

	public int Food
	{
		get { return (int)heroAttributes[HeroAttributes.FOOD]; }
		set { heroAttributes[HeroAttributes.FOOD] = value; }
	}
	
	public float Money
	{
		get { return heroAttributes[HeroAttributes.MONEY]; }
		set { heroAttributes[HeroAttributes.MONEY] = value; }
	}

	public void SetHeroConfig(HeroConfig heroConfig) {
		this.heroConfig = heroConfig;
	}

	public HeroConfig GetHeroConfig() {
		return heroConfig;
	}

	public HeroSkills HeroSkill
	{
		get { return heroSkill; }
		set { heroSkill = value; }
	}

	public string GetHeroSkillName()
	{
		return heroSkillsName[heroSkill];
	}

	public bool IsDead() {
		return heroAttributes[HeroAttributes.HEALTH] <= 0;
	}

	public void Update(float deltaTime, bool reduceHealth = true)
	{
		heroAttributes[HeroAttributes.FOOD] -= heroConfig.foodPerMinute * deltaTime;
		heroAttributes[HeroAttributes.ENERGY] -= heroConfig.energyPerMinute * deltaTime;

		heroAttributes[HeroAttributes.FOOD] = Mathf.Min(heroAttributes[HeroAttributes.FOOD], 100);
		heroAttributes[HeroAttributes.ENERGY] = Mathf.Min(heroAttributes[HeroAttributes.ENERGY], 100);
		
		if (heroAttributes[HeroAttributes.FOOD] < 0)
		{
			if(reduceHealth)
				ReduceHealth(-heroAttributes[HeroAttributes.FOOD]/(heroConfig.foodPerMinute * 2));
			heroAttributes[HeroAttributes.FOOD] = 0;
		}

		if (heroAttributes[HeroAttributes.ENERGY] <= 0) {
			if(reduceHealth)
				ReduceHealth(-heroAttributes[HeroAttributes.ENERGY]/(heroConfig.energyPerMinute * 2));
			heroAttributes[HeroAttributes.ENERGY] = 0;
		}
	}

	void ReduceHealth(float deltaTime)
	{
		heroAttributes[HeroAttributes.HEALTH] -= healthReducePerTick * deltaTime;
	}

	public void UpdateAttributes(List<AttributeToken> tokens) {
		for (int i = 0; i < tokens.Count; i++) {
			heroAttributes[tokens[i].attribute] += tokens[i].amount;

			if (tokens[i].attribute != HeroAttributes.MONEY && heroAttributes[tokens[i].attribute] > 100) {
				heroAttributes[tokens[i].attribute] = 100;
			}
		}
	}

}
