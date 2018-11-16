
using UnityEngine;

public enum ItemType {
	PAPER, BOTTLE, CANS, LEFTOVER
}

public class Item {
	private ItemType typeName;
	private int count;
	static int MAX_COUNT = 100;

	public Item(ItemType typeName, int count) {
		this.typeName = typeName;
		this.count = count;
	}

	public ItemType TypeName {
		get { return typeName; }
		set { typeName = value; }
	}

	public int Count {
		get { return count; }
		set { count = value; }
	}

	public void Add(int amount) {
		count += amount;
		count = Mathf.Min(count, MAX_COUNT);
		Debug.LogError(typeName.ToString() + count);
	}

	public bool Reduce(int amount) {
		if (count - amount >= 0) {
			count -= amount;
			return true;
		}

		return false;
	}
}
