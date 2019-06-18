using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : MonoBehaviour{
	float timeToFinishFood = 360;
	float timeToFinishEnergy = 1080;
	float moneyToRefillFood = 50;
	float bottlePrice = 2f;
	float paperPrice = .2f;
	float canPrice = 1;
	
	float foodReductionPerMinute;
	float energyReductionPerMinute;
	float moneyPerFood;
	float moneyToSurvivePerMinute;
	private float searchIncomePerMinute = 1 / 3f;
	private float jobIncomePerMinute = 1;

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

	public float FoodPerMinute {
		get { return foodReductionPerMinute; }
	}

	public float EnergyPerMinute {
		get { return energyReductionPerMinute; }
	}

	public int GetFoodWithMoney(float money) {
		float foodValue = money / moneyPerFood;
		return Mathf.Min((int)Mathf.Ceil(foodValue), 100);
	}
	
	
	public List<Item> GetTrashItems(float searchTime) {
		List<Item> trashItems = new List<Item>();
		float moneyGiven = searchIncomePerMinute * searchTime;
		int rand = Random.Range(0, 100);
//		if (rand < searchTime) {
//			moneyGiven = moneyGiven * 1.2f;
//			trashItems.Add(new Item(ItemType.LEFTOVER, 2));
//		}
			

		int papersCount = (int)(moneyGiven * .3f / paperPrice);
		int canCount = (int) (moneyGiven * .3f / canPrice);
		int bottlesCount = (int) (moneyGiven * .4f / bottlePrice);
		
		trashItems.Add(new Item(ItemType.PAPER, papersCount));
		trashItems.Add(new Item(ItemType.CANS, canCount));
		trashItems.Add(new Item(ItemType.BOTTLE, bottlesCount));

		return trashItems;
	}

	public float GetBottlePrice(int count) {
		return bottlePrice * count;
	}

	public float GetPaperPrice(int count) {
		return paperPrice * count;
	}

	public float GetCanPrice(int count) {
		return canPrice * count;
	}

	public float GetJobEarning(int minutes) {
		return minutes * jobIncomePerMinute;
	}

}
