using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingArea : Facility {
	private float energyPerHour;
	public override void InitiateData() {
		facilityName = "Sleeping Area 1";
		facilityDescription = "Sleeping is must to revive energy.";
//		optionNames[0] = "Sleep 3h";
//		optionNames[1] = "Sleep 6h";
//		optionNames[2] = "Sleep 8h";
		energyPerHour = 60 * 100f / 480;
		JobActive = false;
		
		btnDatas[0] = new FacilityBtnData();
		btnDatas[0].name = "Take a nap";
		btnDatas[0].time = 180;
		btnDatas[0].changesText = new string[2];
		btnDatas[0].changesColor = new Color[2];
		btnDatas[0].changesText[0] = "Energy +30";
		btnDatas[0].changesColor[0] = FacilityDescriptionPanel.energyColor;
		
		btnDatas[1] = new FacilityBtnData();
		btnDatas[1].name = "Decent sleep";
		btnDatas[1].time = 360;
		btnDatas[1].changesText = new string[2];
		btnDatas[1].changesColor = new Color[2];
		btnDatas[1].changesText[0] = "Energy +70";
		btnDatas[1].changesColor[0] = FacilityDescriptionPanel.energyColor;
		
		btnDatas[2] = new FacilityBtnData();
		btnDatas[2].name = "Deep sleep";
		btnDatas[2].time = 480;
		btnDatas[2].changesText = new string[2];
		btnDatas[2].changesColor = new Color[2];
		btnDatas[2].changesText[0] = "Energy +100";
		btnDatas[2].changesColor[0] = FacilityDescriptionPanel.energyColor;
	}

	public override void Action1() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, 30));
		//Debug.Log(energyPerHour * 3);
		SlumWorld.GetInstance().SleepActionPerformed(tokens, 180);
	}

	public override void Action2() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, 70));
		SlumWorld.GetInstance().SleepActionPerformed(tokens, 360);
	}

	public override void Action3() {
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.ENERGY, 100));
		SlumWorld.GetInstance().SleepActionPerformed(tokens, 480);
	}

	public override void DoJob() {
		
	}
}
