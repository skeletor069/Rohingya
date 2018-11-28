using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dhaba : Facility {
	public override void InitiateData() {
		facilityName = "Dhaba";
		facilityDescription = "Some guys selling low quality foods for lunch and dinner";
		optionNames[0] = "small lunch plate (-৳35)";
		optionNames[1] = "big lunch plate (-৳60)";
		optionNames[2] = "bread with curry (-৳30)";
		JobActive = true;
	}

	public override void Action1() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.FOOD, Balancer.GetInstance().GetFoodWithMoney(35)));
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, -35));
		SlumWorld.GetInstance().ActionPerformed(tokens, 15);
	}

	public override void Action2() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.FOOD, 100));
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, -60));
		SlumWorld.GetInstance().ActionPerformed(tokens, 20);
	}

	public override void Action3() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.FOOD, Balancer.GetInstance().GetFoodWithMoney(30)));
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, -30));
		SlumWorld.GetInstance().ActionPerformed(tokens, 15);
	}

	public override void DoJob() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, 5));
		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, -35));
		SlumWorld.GetInstance().ActionPerformed(tokens, 60);
	}
}
