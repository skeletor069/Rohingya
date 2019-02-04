using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSellShop : Facility {

	public override void InitiateData() {
		facilityName = "Can Dealer";
		facilityDescription = "A guy buying thrown away cans. And he pays well for them.";
		optionNames[0] = "Sell 10 cans ("+Balancer.GetInstance().GetCanPrice(10)+")";
		optionNames[1] = "Sell 20 cans ("+Balancer.GetInstance().GetCanPrice(20)+")";
		optionNames[2] = "Sell 40 cans ("+Balancer.GetInstance().GetCanPrice(40)+")";
		JobActive = true;
		ShowInventory = true;
	}

	public override void Action1() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.CANS, 10))) {
			InteractionDone();
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, Balancer.GetInstance().GetCanPrice(10)));
			SlumWorld.GetInstance().ActionPerformed(temp, 3);
		}
		else {
			// play fail sound
		}
	}

	public override void Action2() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.CANS, 20))) {
			InteractionDone();
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, Balancer.GetInstance().GetCanPrice(20)));
			SlumWorld.GetInstance().ActionPerformed(temp, 5);
		}
		else {
			// play fail sound
		}
	}

	public override void Action3() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.CANS, 40))) {
			InteractionDone();
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, Balancer.GetInstance().GetCanPrice(40)));
			SlumWorld.GetInstance().ActionPerformed(temp, 7);
		}
		else {
			// play fail sound
		}
	}

	public override void DoJob() {
		InteractionDone();
		List<AttributeToken> tokens = new List<AttributeToken>();
//		tokens.Add(new AttributeToken(HeroAttributes.MONEY, 30));
//		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, -30));
		SlumWorld.GetInstance().ActionPerformed(tokens, 60);
	}
}
