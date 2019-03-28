using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabbedMenu : MonoBehaviour {
	
	private List<Animator> itemAnims;
	private int selectedIndex;
	public int ITEM_COUNT = 4;
	private ITabMenuListener listener;
	private bool interactionActive = false;

	private readonly int animSelect = Animator.StringToHash("select");
	private readonly int animDeselect = Animator.StringToHash("deselect");
	
	void Awake () {
		itemAnims = new List<Animator>();
		ITEM_COUNT = transform.childCount;
		for(int i = 0 ; i < ITEM_COUNT; i++)
			itemAnims.Add(transform.GetChild(i).GetComponent<Animator>());
	}

	public void Initiate(ITabMenuListener listener, int initialIndex = 0) {
		this.listener = listener;
		itemAnims[selectedIndex].SetTrigger(animDeselect);
		selectedIndex = initialIndex;
		itemAnims[selectedIndex].SetTrigger(animSelect);
	}

	public void SetSelectedItemIndex(int index) {
		itemAnims[selectedIndex].SetTrigger(animDeselect);
		selectedIndex = index;
		itemAnims[selectedIndex].SetTrigger(animSelect);
		listener.ChangedMenu(selectedIndex);
	}

	public bool InteractionActive {
		get { return interactionActive; }
		set { interactionActive = value; }
	}

	void Update () {
		if (Input.anyKey && interactionActive) {
			if (selectedIndex > 0 && Input.GetKeyDown(KeyCode.UpArrow)) {
				SetSelectedItemIndex(selectedIndex-1);
			}
			else if (selectedIndex < ITEM_COUNT - 1 && Input.GetKeyDown(KeyCode.DownArrow)) {
				SetSelectedItemIndex(selectedIndex+1);
			}

			if (Input.GetKeyDown(KeyCode.Return)) {
				listener.SelectedMenu(selectedIndex);
			}
		}
		
	}
}
