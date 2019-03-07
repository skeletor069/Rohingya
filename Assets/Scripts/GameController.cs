using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	private static GameController instance;
	private World world;
	private bool worldRunning = false;
	private bool isTutorialRunning = false;
	
	void Awake () {
		instance = this;
	}

	void Start() {
		DontDestroyOnLoad(gameObject);
		//GoToSplashScene();
		GoToMainMenu();
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


	public void NewGame() {
		world = new World();
		worldRunning = false;
		GoToSlumScene();
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
