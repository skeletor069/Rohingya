using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingArea : Facility {
	private float energyPerHour;
	public override void InitiateData() {
		facilityName = "Sleeping Area 1";
		facilityDescription = "Sleeping is must to revive energy.";
		optionNames[0] = "Sleep 3h";
		optionNames[1] = "Sleep 6h";
		optionNames[2] = "Sleep 8h";
		energyPerHour = 60 * 100f / 480;
		JobActive = false;
	}

	public override void Action1() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, energyPerHour * 3));
		Debug.Log(energyPerHour * 3);
		SlumWorld.GetInstance().ActionPerformed(tokens, 180);
	}

	public override void Action2() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, energyPerHour * 6));
		SlumWorld.GetInstance().ActionPerformed(tokens, 360);
	}

	public override void Action3() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, 100));
		SlumWorld.GetInstance().ActionPerformed(tokens, 480);
	}

	public override void DoJob() {
		
	}
}
