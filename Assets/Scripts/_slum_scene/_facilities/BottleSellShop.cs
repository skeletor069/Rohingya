using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSellShop : Facility {

	public override void InitiateData() {
		facilityName = "Bottle Shop";
		facilityDescription = "A guy buying plastic bottles. He sells them to different shops selling liquid items..";
		optionNames[0] = "Sell 10 bottles ("+Balancer.GetInstance().GetBottlePrice(10)+")";
		optionNames[1] = "Sell 20 bottles ("+Balancer.GetInstance().GetBottlePrice(20)+")";
		optionNames[2] = "Sell 40 bottles ("+Balancer.GetInstance().GetBottlePrice(40)+")";
		JobActive = true;
		ShowInventory = true;
	}

	public override void Action1() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.BOTTLE, 10))) {
			InteractionDone();
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, Balancer.GetInstance().GetBottlePrice(10)));
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
			SlumWorld.GetInstance().ActionPerformed(temp, 7);
		}
		else {
			// play fail sound
		}
	}

	public override void DoJob() {
		InteractionDone();
		List<AttributeToken> tokens = new List<AttributeToken>();
//		tokens.Add(new AttributeToken(HeroAttributes.MONEY, 7));
//		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, -30));
		SlumWorld.GetInstance().ActionPerformed(tokens, 60);
	}
}
