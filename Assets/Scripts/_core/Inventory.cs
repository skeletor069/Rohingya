using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
	private Dictionary<ItemType, Item> inventoryItems;

	public Inventory() {
		inventoryItems = new Dictionary<ItemType, Item>();
		inventoryItems.Add(ItemType.PAPER, new Item(ItemType.PAPER, 0));
		inventoryItems.Add(ItemType.CANS, new Item(ItemType.CANS, 0));
		inventoryItems.Add(ItemType.BOTTLE, new Item(ItemType.BOTTLE, 0));
		inventoryItems.Add(ItemType.LEFTOVER, new Item(ItemType.LEFTOVER, 0));
	}

	public int GetItemCount(ItemType type) {
		return inventoryItems[type].Count;
	}

	public void AddItem(Item item) {
		inventoryItems[item.TypeName].Add(item.Count);
	}

	public bool ConsumeItem(Item item) {
		return inventoryItems[item.TypeName].Reduce(item.Count);
	}

}
