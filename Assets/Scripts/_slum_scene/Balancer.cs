using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : MonoBehaviour{
	public float timeToFinishFood = 360;
	public float timeToFinishEnergy = 720;
	public float moneyToRefillFood = 60;
	public float bottlePrice = .02f;
	public float paperPrice = .005f;
	public float canPrice = .03f;
	
	float foodReductionPerMinute;
	float energyReductionPerMinute;
	float moneyPerFood;
	float moneyToSurvivePerMinute;

	private static Balancer instance;

	void Awake() {
		foodReductionPerMinute = 100f / timeToFinishFood;
		energyReductionPerMinute = 100f / timeToFinishEnergy;
		moneyPerFood = moneyToRefillFood / 100f;
		moneyToSurvivePerMinute = foodReductionPerMinute * moneyPerFood;
		instance = this;

	}

	public static Balancer GetInstance() {
		return instance;
	}

	public int GetFoodWithMoney(float money) {
		float foodValue = money / moneyPerFood;
		return (int)Mathf.Ceil(foodValue);
	}
	
	
	public List<Item> GetTrashItems(float searchTime) {
		List<Item> trashItems = new List<Item>();
		float moneyGiven = moneyToSurvivePerMinute * searchTime;
		int rand = Random.Range(0, 100);
		if (rand < searchTime) {
			moneyGiven = moneyGiven * 1.2f;
			trashItems.Add(new Item(ItemType.LEFTOVER, 2));
		}
			

		int papersCount = (int)(moneyGiven * .3f / paperPrice);
		int canCount = (int) (moneyGiven * .3f / canPrice);
		int bottlesCount = (int) (moneyGiven * .4f / bottlePrice);
		
		trashItems.Add(new Item(ItemType.PAPER, papersCount));
		trashItems.Add(new Item(ItemType.CANS, canCount));
		trashItems.Add(new Item(ItemType.BOTTLE, bottlesCount));

		return trashItems;
	}

	public float GetBottlePrice(int count) {
		return bottlePrice * count * (1 + count * .01f);
	}

	public float GetPaperPrice(int count) {
		return paperPrice * count * (1 + count * .01f);
	}

}
