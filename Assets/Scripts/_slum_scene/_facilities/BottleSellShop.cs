using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSellShop : Facility {

	public override void InitiateData() {
		facilityName = "Bottle Dealer";
		facilityDescription = "A guy buying plastic bottles. He sells them to different shops selling liquid items..";
//		optionNames[0] = "Sell 10 bottles ("+Balancer.GetInstance().GetBottlePrice(10)+")";
//		optionNames[1] = "Sell 20 bottles ("+Balancer.GetInstance().GetBottlePrice(20)+")";
//		optionNames[2] = "Sell 40 bottles ("+Balancer.GetInstance().GetBottlePrice(40)+")";
		JobActive = true;
		ShowInventory = true;
		
		btnDatas[0] = new FacilityBtnData();
		btnDatas[0].name = "Sell 10 bottles";
		btnDatas[0].time = 3;
		btnDatas[0].changesText = new string[2];
		btnDatas[0].changesColor = new Color[2];
		btnDatas[0].changesText[0] = "Money +" + Balancer.GetInstance().GetBottlePrice(10);
		btnDatas[0].changesColor[0] = FacilityDescriptionPanel.moneyColor;
		
		btnDatas[1] = new FacilityBtnData();
		btnDatas[1].name = "Sell 20 bottles";
		btnDatas[1].time = 5;
		btnDatas[1].changesText = new string[2];
		btnDatas[1].changesColor = new Color[2];
		btnDatas[1].changesText[0] = "Money +" + Balancer.GetInstance().GetBottlePrice(20);
		btnDatas[1].changesColor[0] = FacilityDescriptionPanel.moneyColor;
		
		btnDatas[2] = new FacilityBtnData();
		btnDatas[2].name = "Sell 40 bottles";
		btnDatas[2].time = 7;
		btnDatas[2].changesText = new string[2];
		btnDatas[2].changesColor = new Color[2];
		btnDatas[2].changesText[0] = "Money +" + Balancer.GetInstance().GetBottlePrice(40);
		btnDatas[2].changesColor[0] = FacilityDescriptionPanel.moneyColor;
		
	}

	public override void Action1() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.BOTTLE, 10))) {
			InteractionDone();
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, Balancer.GetInstance().GetBottlePrice(10)));
			SoundManager.GetInstance().PlaySound(SoundTypes.SELL);
			SlumWorld.GetInstance().ActionPerformed(temp, 3);
		}
		else {
			// play fail sound
		}
	}

	public override void Action2() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.BOTTLE, 20))) {
			InteractionDone();
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, Balancer.GetInstance().GetBottlePrice(20)));
			SoundManager.GetInstance().PlaySound(SoundTypes.SELL);
			SlumWorld.GetInstance().ActionPerformed(temp, 5);
		}
		else {
			// play fail sound
		}
	}

	public override void Action3() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.BOTTLE, 40))) {
			InteractionDone();
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, Balancer.GetInstance().GetBottlePrice(40)));
			SoundManager.GetInstance().PlaySound(SoundTypes.SELL);
			SlumWorld.GetInstance().ActionPerformed(temp, 7);
		}
		else {
			// play fail sound
		}
	}

	public override void DoJob() {
		InteractionDone();
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, Balancer.GetInstance().GetJobEarning(60)));
		SlumWorld.GetInstance().JobDone(tokens, 60, SoundTypes.WORK_BOTTLE);
		SoundManager.GetInstance().PlaySound(SoundTypes.WORK_BOTTLE);
	}
}
