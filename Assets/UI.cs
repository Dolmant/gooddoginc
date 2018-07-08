using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    public Doggo doggo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Continue() {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    void DrinkyBoi() {
        doggo.loadable.Drink += 5;
        Serializer.Save<Loadable>("gamedata", doggo.loadable);
        Continue();
    }

    void FasterBoi() {
        doggo.loadable.Speed += 1;
        Serializer.Save<Loadable>("gamedata", doggo.loadable);
        Continue();
    }

    void GooderBoi() {
        doggo.loadable.MaxGoodBoy += 100;
        Serializer.Save<Loadable>("gamedata", doggo.loadable);
        Continue();
    }

    void Restart() {
        Serializer.Save<Loadable>("gamedata", new Loadable());
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
