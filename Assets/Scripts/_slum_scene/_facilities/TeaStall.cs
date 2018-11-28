using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaStall : Facility {
	
	public override void InitiateData() {
		facilityName = "Tea Stall";
		facilityDescription = "Small shop selling a few snacks. Obviously at a higher price.";
		optionNames[0] = "cup of tea (-৳6)";
		optionNames[1] = "1pc banana (-৳12)";
		optionNames[2] = "1pc biscuit (-৳3)";
		JobActive = true;
		ShowInventory = false;
	}

	public override void Action1() {
		Debug.Log("Action 1");
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.FOOD, Balancer.GetInstance().GetFoodWithMoney(6)));
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, -6));
		SlumWorld.GetInstance().ActionPerformed(tokens, 8);
	}

	public override void Action2() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.FOOD, Balancer.GetInstance().GetFoodWithMoney(12)));
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, -12));
		SlumWorld.GetInstance().ActionPerformed(tokens, 4);
	}

	public override void Action3() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.FOOD, Balancer.GetInstance().GetFoodWithMoney(3)));
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, -3));
		SlumWorld.GetInstance().ActionPerformed(tokens, 4);
	}

	public override void DoJob() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.MONEY, 3));
		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, -30));
		SlumWorld.GetInstance().ActionPerformed(tokens, 60);
	}
}
