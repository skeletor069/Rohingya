using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour {
	private string FILE_PATH = "/save.dat";

	public void SaveWorld(World world) {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + FILE_PATH, FileMode.OpenOrCreate);
		bf.Serialize(file, world);
		file.Close();
	}

	public bool HasSavedData() {
		return File.Exists(Application.persistentDataPath + FILE_PATH);
	}

	public World LoadWorld() {
		if (File.Exists (Application.persistentDataPath + FILE_PATH)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + FILE_PATH, FileMode.Open);
			World world = (World)bf.Deserialize(file);
			file.Close();
			return world;
		} else 
			return new World();
	}
	
	
}
