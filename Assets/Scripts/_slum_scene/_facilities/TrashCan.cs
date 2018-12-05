using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Facility {
	public override void InitiateData() {
		facilityName = "Trash Can";
		facilityDescription = "People throw waste here. However it is a good place to find some bottles, cans or even food.";
		optionNames[0] = "Search for 10 mins";
		optionNames[1] = "Search for 20 mins";
		optionNames[2] = "Search for 30 mins";
		JobActive = false;
		ShowInventory = true;
	}

	public override void Action1() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.BOTTLE, 10))) {
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, 2));
			SlumWorld.GetInstance().ActionPerformed(temp, 3);
		}
		else {
//			play fail sound
		}
	}

	public override void Action2() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.BOTTLE, 20))) {
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, 5));
			SlumWorld.GetInstance().ActionPerformed(temp, 5);
		}
		else {
			// play fail sound
		}
	}

	public override void Action3() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.BOTTLE, 40))) {
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, 12));
			SlumWorld.GetInstance().ActionPerformed(temp, 7);
		}
		else {
			// play fail sound
		}
	}

	public override void DoJob() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, 7));
		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, -30));
		SlumWorld.GetInstance().ActionPerformed(tokens, 60);
	}
}
