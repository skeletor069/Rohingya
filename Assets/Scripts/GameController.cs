using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	private static GameController instance;
	private World world;
	private bool worldRunning = false;
	private bool isTutorialRunning = false;
	DataManager dataManager;
	
	void Awake () {
		instance = this;
		dataManager = GetComponent<DataManager>();
	}

	void Start() {
		DontDestroyOnLoad(gameObject);
		//GoToSplashScene();
		//GoToMainMenu();
		SceneManager.LoadScene(Scenes.SOUND_LOAD);
	}

	private void Update() {
		if(worldRunning)
			world.Update(Time.deltaTime);
	}

	public static GameController GetInstance() {
		return instance;
	}

	public World World {
		get { return world; }
		set { world = value; }
	}

	public bool WorldRunning {
		get { return worldRunning; }
		set { worldRunning = value; }
	}

	public bool IsTutorialRunning {
		get { return isTutorialRunning; }
		set { isTutorialRunning = value; }
	}

	public void GoToSplashScene() {
		SceneManager.LoadScene(Scenes.SPLASH);
	}

	public void GoToMainMenu() {
		SceneManager.LoadScene(Scenes.MAIN_MENU);
	}

	public void GoToHome() {
		SceneManager.LoadScene(Scenes.HOME_L1);
	}

	public void GoToCityMap() {
		SceneManager.LoadScene(Scenes.CITY_MAP);
	}

	public void GoToSlumScene() {
		SceneManager.LoadScene(Scenes.SLUM_SCENE);
	}

	public void SaveGame() {
		dataManager.SaveWorld(world);
	}

	public void LoadGame() {
		world = dataManager.LoadWorld();
		isTutorialRunning = false;
		worldRunning = false;
		GoToSlumScene();
	}


	public void NewGame() {
		// if load data found, show prompt
		world = new World();
		worldRunning = false;
		isTutorialRunning = true;
		GoToSlumScene();
	}

	public bool HasSavedData() {
		return dataManager.HasSavedData();
	}

	public void StartSurvival() {
		world.SetMinutesGone(600);
		worldRunning = true;
		HeroConfig heroConfig = new HeroConfig();
		heroConfig.energyPerMinute = Balancer.GetInstance().EnergyPerMinute;
		heroConfig.foodPerMinute = Balancer.GetInstance().FoodPerMinute;
		world.Hero.SetHeroConfig(heroConfig);
		world.Hero.Health = 100;
		HUD.GetInstance().ShowHud();
	}


}
