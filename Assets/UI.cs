using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

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

    void Restart() {
        Serializer.Save<Loadable>("gamedata", new Loadable());
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
