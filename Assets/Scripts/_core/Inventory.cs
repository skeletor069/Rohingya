using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory {

	

	private Dictionary<ItemType, Item> inventoryItems;
	private ItemSlot[] itemSlots;

	public Inventory() {
		inventoryItems = new Dictionary<ItemType, Item>();
		inventoryItems.Add(ItemType.PAPER, new Item(ItemType.PAPER, 0));
		inventoryItems.Add(ItemType.CANS, new Item(ItemType.CANS, 0));
		inventoryItems.Add(ItemType.BOTTLE, new Item(ItemType.BOTTLE, 0));
		inventoryItems.Add(ItemType.LEFTOVER, new Item(ItemType.LEFTOVER, 0));
		
		itemSlots = new ItemSlot[9];
		for(int i = 0 ; i < 9; i++)
			itemSlots[i] = new ItemSlot();
		
		//PopulateSlots();
	}

	public int GetItemCount(ItemType type) {
		return inventoryItems[type].Count;
	}

	public void AddItem(Item item) {
		AddMuchAsPossible(item);
		PopulateDictionary();
	}

	public bool ConsumeItem(Item item) {
		Debug.LogError("item count " + item.Count);
		Debug.LogError("Previous " + inventoryItems[item.TypeName].Count);
		if (inventoryItems[item.TypeName].Reduce(item.Count)) {
			Debug.Log("expected reduce " + inventoryItems[item.TypeName].Count);
			PopulateSlots();
			return true;
		}

		return false;
	}

	public ItemSlot[] GetItemSlots() {
		return itemSlots;
	}

	void AddMuchAsPossible(Item item) {
		Item temp = new Item(item.TypeName, item.Count);
		for (int i = 0; i < itemSlots.Length; i++) {
			temp = itemSlots[i].AddItem(temp);
			if(temp.Count == 0)
				break;
		}
	}

	void PopulateDictionary() {
		inventoryItems[ItemType.PAPER].Count = 0;
		inventoryItems[ItemType.CANS].Count = 0;
		inventoryItems[ItemType.BOTTLE].Count = 0;
		inventoryItems[ItemType.LEFTOVER].Count = 0;

		for (int i = 0; i < itemSlots.Length; i++) {
			if (!itemSlots[i].IsEmpty()) {
				inventoryItems[itemSlots[i].item.TypeName].Add(itemSlots[i].item.Count);
			}
		}

	}

	void PopulateSlots() {
		for(int i = 0 ; i < 9; i++)
			itemSlots[i].SetEmpty();
		AddMuchAsPossible(inventoryItems[ItemType.PAPER]);
		AddMuchAsPossible(inventoryItems[ItemType.BOTTLE]);
		AddMuchAsPossible(inventoryItems[ItemType.CANS]);
		AddMuchAsPossible(inventoryItems[ItemType.LEFTOVER]);
	}

}
