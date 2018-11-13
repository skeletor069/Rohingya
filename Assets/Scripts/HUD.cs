using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct DataHUD
{
	public int health;
	public int energy;
	public int food;
	public int wallet;
	public string heroSkill;
	public string timeText;
	public string daysText;
}

public class HUD : MonoBehaviour {

	public TextMeshProUGUI daysText;
	public TextMeshProUGUI hoursText;
	public TextMeshProUGUI walletText;
	public TextMeshProUGUI skillsText;
	public AttributeUI healthBar;
	public AttributeUI energyBar;
	public AttributeUI foodBar;

	private DataHUD dataHud;
	
	void Update() {
		dataHud = GameController.GetInstance().World.GetDataHud();
		daysText.text = dataHud.daysText;
		hoursText.text = dataHud.timeText;
		healthBar.SetValue(dataHud.health);
		energyBar.SetValue(dataHud.energy);
		foodBar.SetValue(dataHud.food);
		walletText.text = "$"+dataHud.wallet;
	}
}
