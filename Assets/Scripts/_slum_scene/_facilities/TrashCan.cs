using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrashCan : Facility {
	public override void InitiateData() {
		facilityName = "Trash Can";
		facilityDescription = "People throw waste here. However it is a good place to find some bottles, cans or even food.";
//		optionNames[0] = "Search for 10 mins";
//		optionNames[1] = "Search for 20 mins";
//		optionNames[2] = "Search for 30 mins";
		JobActive = false;
		ShowInventory = true;
		
		btnDatas[0] = new FacilityBtnData();
		btnDatas[0].name = "Quick search";
		btnDatas[0].time = 10;
		btnDatas[0].changesText = new string[2];
		btnDatas[0].changesColor = new Color[2];
		btnDatas[0].changesText[0] = "";
		btnDatas[0].changesColor[0] = FacilityDescriptionPanel.moneyColor;
		
		btnDatas[1] = new FacilityBtnData();
		btnDatas[1].name = "Decent search";
		btnDatas[1].time = 20;
		btnDatas[1].changesText = new string[2];
		btnDatas[1].changesColor = new Color[2];
		btnDatas[1].changesText[0] = "";
		btnDatas[1].changesColor[0] = FacilityDescriptionPanel.moneyColor;
		
		btnDatas[2] = new FacilityBtnData();
		btnDatas[2].name = "Deep search";
		btnDatas[2].time = 30;
		btnDatas[2].changesText = new string[2];
		btnDatas[2].changesColor = new Color[2];
		btnDatas[2].changesText[0] = "";
		btnDatas[2].changesColor[0] = FacilityDescriptionPanel.moneyColor;
	}

	public override void Action1() {
		List<Item> trashItems = Balancer.GetInstance().GetTrashItems(10);
		SlumWorld.GetInstance().ItemsFound(trashItems, 10);		
	}

	public override void Action2() {
		List<Item> trashItems = Balancer.GetInstance().GetTrashItems(20);
		SlumWorld.GetInstance().ItemsFound(trashItems, 20);	
	}

	public override void Action3() {
		List<Item> trashItems = Balancer.GetInstance().GetTrashItems(30);
		SlumWorld.GetInstance().ItemsFound(trashItems, 30);		
	}

	public override void DoJob() {
		
	}
}
