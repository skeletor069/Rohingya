using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		List<Item> trashItems = GetTrashItems(10);
		SlumWorld.GetInstance().ItemsFound(trashItems, 10);		
	}

	public override void Action2() {
		List<Item> trashItems = GetTrashItems(20);
		SlumWorld.GetInstance().ItemsFound(trashItems, 20);	
	}

	public override void Action3() {
		List<Item> trashItems = GetTrashItems(30);
		SlumWorld.GetInstance().ItemsFound(trashItems, 30);		
	}

	public override void DoJob() {
		
	}

	List<Item> GetTrashItems(int minutes) {
		List<Item> items = new List<Item>();
		switch (minutes) {
			case 10:
				items.Add(new Item(ItemType.PAPER,Random.Range(2, 5)));
				items.Add(new Item(ItemType.CANS,Random.Range(0, 3)));
				
				break;
			case 20:
				items.Add(new Item(ItemType.PAPER,Random.Range(5, 10)));
				items.Add(new Item(ItemType.CANS,Random.Range(3, 6)));
				items.Add(new Item(ItemType.BOTTLE,Random.Range(0, 3)));
				break;
			case 30:
				items.Add(new Item(ItemType.PAPER,Random.Range(10, 15)));
				items.Add(new Item(ItemType.CANS,Random.Range(6, 10)));
				items.Add(new Item(ItemType.BOTTLE,Random.Range(2, 7)));
				break;
		}

		return items;
	}
}
