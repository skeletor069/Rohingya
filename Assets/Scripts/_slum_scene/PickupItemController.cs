﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickupItemController : MonoBehaviour {
	private float paperProbability = 40f;
	//private float foodProbability = 70f;
	private float canProbability = 80f;

	private List<PickupItem> itemPool;
	private List<PickupItem> activeItems;

	public Sprite bottleSprite, canSprite, paperSprite;
	public PickupEffect pickupEffect;
	public List<Transform> centers;

	void Awake() {
		activeItems = new List<PickupItem>();
		itemPool = new List<PickupItem>();
		for(int i = 0 ; i < transform.childCount; i++)
			itemPool.Add(transform.GetChild(i).GetComponent<PickupItem>());
	}

	void Start() {
		StartCoroutine(GenerationRoutine());
	}

	public Item ProcessPickupGetItem(PickupItem pickItem, Vector3 position) {
		if (activeItems.Contains(pickItem)) {
			activeItems.Remove(pickItem);
			itemPool.Add(pickItem);
			pickItem.gameObject.SetActive(false);
			// particle
		}
		Item item = new Item(ItemType.BOTTLE, 1);
		Sprite sprite = bottleSprite;
		float rand = Random.Range(0, 100f);
		if (rand < paperProbability) {
			item =  new Item(ItemType.PAPER, 1);
			sprite = paperSprite;
		}
		else if (rand < canProbability) {
			item = new Item(ItemType.CANS, 1);
			sprite = canSprite;
		}

		pickupEffect.ShowEffect(position, sprite);
		return item;




	}

	public void GeneratePickupItem(Vector3 position) {
		position = centers[Random.Range(0, centers.Count)].position;
		float maxWalkDistance = 20f;
		Vector3 direction = Random.insideUnitSphere * maxWalkDistance;
		direction += position;
		NavMeshHit hit;
		NavMesh.SamplePosition(direction, out hit, Random.Range(0f, maxWalkDistance), 1);
		Vector3 destination = hit.position;
		PickupItem pickItem = GetFromPool();
		if (pickItem != null && !(float.IsInfinity(destination.x) || float.IsInfinity(destination.y) || float.IsInfinity(destination.z))) {
			pickItem.gameObject.SetActive(true);
			destination.y = .3f;
			pickItem.transform.position = destination;
		}
		
	}

	PickupItem GetFromPool() {
		if (itemPool.Count > 0) {
			PickupItem temp = itemPool[0];
			activeItems.Add(temp);
			itemPool.RemoveAt(0);
			return temp;
		}
		return null;
	}

	IEnumerator GenerationRoutine() {
		WaitForSeconds delay = new WaitForSeconds(5);

		while (true) {
			yield return delay;
			GeneratePickupItem(SlumWorld.GetInstance().player.transform.position);
		}
	}
}
