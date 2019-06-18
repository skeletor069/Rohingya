using UnityEngine;
[System.Serializable]
public class ItemSlot {
	public Item item;
	private int MAX_COUNT = 30;

	public ItemSlot() {
		item = null;
	}

	public bool IsEmpty() {
		return item == null;
	}

	public void SetEmpty() {
		item = null;
	}

	public Item AddItem(Item newItem) {
		if (item == null) {
			Debug.Log("Slot free");
			if (newItem.Count <= MAX_COUNT) {
				item = newItem;
				return new Item(newItem.TypeName, 0);
			}
			else {
				int remaining = newItem.Count - MAX_COUNT;
				newItem.Count = MAX_COUNT;
				item = newItem;
				return new Item(newItem.TypeName, remaining);
			}
		}
		else{
			Debug.Log("Slot has item");
			if (item.TypeName == newItem.TypeName) {
				if (item.Count + newItem.Count <= MAX_COUNT) {
					item.Add(newItem.Count);
					return new Item(newItem.TypeName, 0);
				}
				else {
					int taken = MAX_COUNT - item.Count;
					item.Add(taken);
					return new Item(newItem.TypeName, newItem.Count - taken);
				}
			}

			return newItem;
		}
	}
}