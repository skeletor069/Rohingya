using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	private static GameController instance;
	private World world;
	private bool worldRunning = false;
	
	void Awake () {
		instance = this;
	}

	void Start() {
		DontDestroyOnLoad(gameObject);
		GoToSplashScene();
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
		worldRunning = true;
		GoToSlumScene();
	}


}
