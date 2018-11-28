using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
	private static InventoryUI instance;
	public List<Sprite> itemImages = new List<Sprite>();
	public Transform slotUiHolder;
	private List<SlotUI> slotUis;
	private GameObject panel;

	void Awake() {
		instance = this;
		panel = transform.GetChild(0).gameObject;
		slotUis = new List<SlotUI>();
		for (int i = 0; i < slotUiHolder.childCount; i++) {
			slotUis.Add(slotUiHolder.GetChild(i).GetComponent<SlotUI>());
		}
	}

	void Start() {
		panel.SetActive(false);	
	}

	public static InventoryUI GetInstance() {
		return instance;
	}

	public Sprite GetItemImage(ItemType itemType) {
		switch (itemType) {
			case ItemType.PAPER:
				return itemImages[0];
			case ItemType.BOTTLE:
				return itemImages[1];
			case ItemType.CANS:
				return itemImages[2];
			case ItemType.LEFTOVER:
				return itemImages[3];
		}

		return itemImages[0];
	}

	public void ShowInventoryPanel() {
		panel.SetActive(true);
		ItemSlot[] itemSlots = GameController.GetInstance().World.Inventory.GetItemSlots();
		Debug.Log("count " + itemSlots.Length);
		for (int i = 0; i < itemSlots.Length; i++) {
			if(itemSlots[i].IsEmpty())
				slotUis[i].SetEmpty();
			else
				slotUis[i].SetData(GetItemImage(itemSlots[i].item.TypeName), itemSlots[i].item.Count);
		}
	}

	public bool IsInventoryOpen() {
		return panel.activeInHierarchy;
	}

	public void CloseInventoryPanel() {
		panel.SetActive(false);
	}
}
