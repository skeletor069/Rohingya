using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacilityType
{
	HOME, FOOD, JOBS, MEDICAL, EDUCATION
}

public class CityMapController : MonoBehaviour, ITabMenuListener {
	
	public TabbedMenu destinationMenu; 
	public List<Transform> foodBuildings = new List<Transform>();
	public List<Transform> jobBuildings = new List<Transform>();
	public List<Transform> hospitalBuildings = new List<Transform>();
	
	List<Transform> markers = new List<Transform>();

	public Transform markersHolder;
	
	// Use this for initialization
	void Start () {
		destinationMenu.Initiate(this, 0);
		destinationMenu.InteractionActive = true;
		ShowDestinationsPanel();
	}

	void ShowDestinationsPanel()
	{
		
	}

	public void InitialDestinationSelected(FacilityType selectedDestination)
	{
		
	}

	public void ShowDestinationOptions(FacilityType selectedDestination)
	{
		
	}

	void GoHome()
	{
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void ChangedMenu(int index) {
		switch (index) {
			case 0:
				
				break;
			case 1:
				
				break;
			case 2:
				
				break;
			case 3:
				
				break;
		}	
	}

	public void SelectedMenu(int index) {
		switch (index) {
			case 0:
				GameController.GetInstance().GoToHome();
				break;
			case 1:
				
				break;
			case 2:
				
				break;
			case 3:
				
				break;
		}
		destinationMenu.InteractionActive = false;
	}
}
