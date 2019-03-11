using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaStall : Facility {
	
	public override void InitiateData() {
		facilityName = "Tea Stall";
		facilityDescription = "Small shop selling a few snacks. Obviously at a higher price.";
//		optionNames[0] = "cup of tea (-৳6)";
//		optionNames[1] = "1pc banana (-৳12)";
//		optionNames[2] = "1pc biscuit (-৳3)";
		JobActive = true;
		ShowInventory = false;
		
		btnDatas[0] = new FacilityBtnData();
		btnDatas[0].name = "1pc biscuit";
		btnDatas[0].time = 4;
		btnDatas[0].changesText = new string[2];
		btnDatas[0].changesColor = new Color[2];
		btnDatas[0].changesText[0] = "Food +" + Balancer.GetInstance().GetFoodWithMoney(3);
		btnDatas[0].changesColor[0] = FacilityDescriptionPanel.foodColor;
		btnDatas[0].changesText[1] = "Money -3";
		btnDatas[0].changesColor[1] = FacilityDescriptionPanel.moneyColor;
		btnDatas[0].actionSound = SoundTypes.EAT_CRUNCHY;
		
		btnDatas[1] = new FacilityBtnData();
		btnDatas[1].name = "Cup of tea";
		btnDatas[1].time = 8;
		btnDatas[1].changesText = new string[2];
		btnDatas[1].changesColor = new Color[2];
		btnDatas[1].changesText[0] = "Food +" + Balancer.GetInstance().GetFoodWithMoney(6);
		btnDatas[1].changesColor[0] = FacilityDescriptionPanel.foodColor;
		btnDatas[1].changesText[1] = "Money -6";
		btnDatas[1].changesColor[1] = FacilityDescriptionPanel.moneyColor;
		btnDatas[1].actionSound = SoundTypes.DRINK;
		
		btnDatas[2] = new FacilityBtnData();
		btnDatas[2].name = "1pc banana";
		btnDatas[2].time = 4;
		btnDatas[2].changesText = new string[2];
		btnDatas[2].changesColor = new Color[2];
		btnDatas[2].changesText[0] = "Food +" + Balancer.GetInstance().GetFoodWithMoney(12);
		btnDatas[2].changesColor[0] = FacilityDescriptionPanel.foodColor;
		btnDatas[2].changesText[1] = "Money -12";
		btnDatas[2].changesColor[1] = FacilityDescriptionPanel.moneyColor;
		btnDatas[2].actionSound = SoundTypes.EAT_JUICY;
	}

	public override void Action1() {
		InteractionDone();
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.FOOD, Balancer.GetInstance().GetFoodWithMoney(3)));
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, -3));
		SlumWorld.GetInstance().ActionPerformed(tokens, 8);
		SoundManager.GetInstance().PlaySound(SoundTypes.EAT_CRUNCHY);
	}

	public override void Action2() {
		InteractionDone();
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.FOOD, Balancer.GetInstance().GetFoodWithMoney(6)));
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, -6));
		SlumWorld.GetInstance().ActionPerformed(tokens, 4);
		SoundManager.GetInstance().PlaySound(SoundTypes.DRINK);
	}

	public override void Action3() {
		InteractionDone();
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.FOOD, Balancer.GetInstance().GetFoodWithMoney(12)));
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, -12));
		SlumWorld.GetInstance().ActionPerformed(tokens, 4);
		SoundManager.GetInstance().PlaySound(SoundTypes.EAT_JUICY);
	}

	public override void DoJob() {
		InteractionDone();
		List<AttributeToken> tokens = new List<AttributeToken>();
//		tokens.Add(new AttributeToken(HeroAttributes.MONEY, 3));
//		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, -30));
		SlumWorld.GetInstance().ActionPerformed(tokens, 60);
	}
}
