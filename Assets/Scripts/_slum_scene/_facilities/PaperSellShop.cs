﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSellShop : Facility {

	public override void InitiateData() {
		facilityName = "Scrap Paper Shop";
		facilityDescription = "A guy buying scrap papers. He makes packets out of it and sells later.";
		optionNames[0] = "Sell 10 papers";
		optionNames[0] = "Sell 20 papers";
		optionNames[0] = "Sell 40 papers";
		JobActive = true;
	}

	public override void Action1() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.PAPER, 10))) {
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, 1));
			SlumWorld.GetInstance().ActionPerformed(temp, 3);
		}
		else {
			// play fail sound
		}
	}

	public override void Action2() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.PAPER, 20))) {
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, 2));
			SlumWorld.GetInstance().ActionPerformed(temp, 5);
		}
		else {
			// play fail sound
		}
	}

	public override void Action3() {
		if (GameController.GetInstance().World.Inventory.ConsumeItem(new Item(ItemType.PAPER, 40))) {
			List<AttributeToken> temp = new List<AttributeToken>();
			temp.Add(new AttributeToken(HeroAttributes.MONEY, 5));
			SlumWorld.GetInstance().ActionPerformed(temp, 7);
		}
		else {
			// play fail sound
		}
	}

	public override void DoJob() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, 5));
		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, -30));
		SlumWorld.GetInstance().ActionPerformed(tokens, 60);
	}
}