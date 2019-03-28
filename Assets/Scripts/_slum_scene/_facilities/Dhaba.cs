using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dhaba : Facility {
	public override void InitiateData() {
		facilityName = "Dhaba";
		facilityDescription = "Some guys selling low quality foods for lunch and dinner";
//		optionNames[0] = "small lunch plate (-৳35)";
//		optionNames[1] = "big lunch plate (-৳60)";
//		optionNames[2] = "bread with curry (-৳30)";
		JobActive = true;
		
		btnDatas[0] = new FacilityBtnData();
		btnDatas[0].name = "Bread with curry";
		btnDatas[0].time = 15;
		btnDatas[0].changesText = new string[2];
		btnDatas[0].changesColor = new Color[2];
		btnDatas[0].changesText[0] = "Food +" + Balancer.GetInstance().GetFoodWithMoney(30);
		btnDatas[0].changesColor[0] = FacilityDescriptionPanel.foodColor;
		btnDatas[0].changesText[1] = "Money -30";
		btnDatas[0].changesColor[1] = FacilityDescriptionPanel.moneyColor;
		
		btnDatas[1] = new FacilityBtnData();
		btnDatas[1].name = "Small lunch plate";
		btnDatas[1].time = 15;
		btnDatas[1].changesText = new string[2];
		btnDatas[1].changesColor = new Color[2];
		btnDatas[1].changesText[0] = "Food +" + Balancer.GetInstance().GetFoodWithMoney(35);
		btnDatas[1].changesColor[0] = FacilityDescriptionPanel.foodColor;
		btnDatas[1].changesText[1] = "Money -35";
		btnDatas[1].changesColor[1] = FacilityDescriptionPanel.moneyColor;
		
		btnDatas[2] = new FacilityBtnData();
		btnDatas[2].name = "Big lunch plate";
		btnDatas[2].time = 20;
		btnDatas[2].changesText = new string[2];
		btnDatas[2].changesColor = new Color[2];
		btnDatas[2].changesText[0] = "Food +" + Balancer.GetInstance().GetFoodWithMoney(60);
		btnDatas[2].changesColor[0] = FacilityDescriptionPanel.foodColor;
		btnDatas[2].changesText[1] = "Money -60";
		btnDatas[2].changesColor[1] = FacilityDescriptionPanel.moneyColor;
	}

	public override void Action1() {
		if (GameController.GetInstance().World.Hero.Money >= 30) {
			InteractionDone();
			List<AttributeToken> tokens = new List<AttributeToken>();
			tokens.Add(new AttributeToken(HeroAttributes.FOOD, Balancer.GetInstance().GetFoodWithMoney(30)));
			tokens.Add(new AttributeToken(HeroAttributes.MONEY, -30));
			SlumWorld.GetInstance().ActionPerformed(tokens, 15);
			SoundManager.GetInstance().PlaySound(SoundTypes.EAT_MEAL);
		}
		else {
			// play fail sound
		}

	}

	public override void Action2() {
		if (GameController.GetInstance().World.Hero.Money >= 35) {
			InteractionDone();
			List<AttributeToken> tokens = new List<AttributeToken>();
			tokens.Add(new AttributeToken(HeroAttributes.FOOD, Balancer.GetInstance().GetFoodWithMoney(35)));
			tokens.Add(new AttributeToken(HeroAttributes.MONEY, -35));
			SlumWorld.GetInstance().ActionPerformed(tokens, 15);
			SoundManager.GetInstance().PlaySound(SoundTypes.EAT_MEAL);
		}
		else {
			// play fail sound
		}

	}

	public override void Action3() {
		if (GameController.GetInstance().World.Hero.Money >= 60) {
			InteractionDone();
			List<AttributeToken> tokens = new List<AttributeToken>();
			tokens.Add(new AttributeToken(HeroAttributes.FOOD, 100));
			tokens.Add(new AttributeToken(HeroAttributes.MONEY, -60));
			SlumWorld.GetInstance().ActionPerformed(tokens, 20);
			SoundManager.GetInstance().PlaySound(SoundTypes.EAT_MEAL);
		}
		else {
			// play fail sound
		}
	}

	public override void DoJob() {
		InteractionDone();
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, Balancer.GetInstance().GetJobEarning(60)));
		SlumWorld.GetInstance().JobDone(tokens, 60, SoundTypes.WORK_FOOD);
		SoundManager.GetInstance().PlaySound(SoundTypes.WORK_FOOD);
	}
}
