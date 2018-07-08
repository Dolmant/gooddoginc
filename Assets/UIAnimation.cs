using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIAnimation : MonoBehaviour {
    public Slider BaseMeter;
    public Text textFab;
    private Text textFabAnimate;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (textFabAnimate != null) {
            if (textFabAnimate.transform.position.y - BaseMeter.transform.position.y < 125) {
                Debug.Log(textFabAnimate.transform.position.y - BaseMeter.transform.position.y);
                textFabAnimate.transform.position = textFabAnimate.transform.position + new Vector3(0, (float)1);
            }
            if (textFabAnimate.transform.position.y - BaseMeter.transform.position.y >= 125) {
                Destroy(textFabAnimate.gameObject);
            }
        }
	}

    public void SpawnText(string text, Color color) {
        if (textFabAnimate == null) {
            var newText = Instantiate(textFab, BaseMeter.transform.position + new Vector3(0, (float)25), BaseMeter.transform.rotation, BaseMeter.transform);
            newText.text = text;
            newText.color = color;
            textFabAnimate = newText;
            newText.canvasRenderer.SetAlpha(1f);
            newText.CrossFadeAlpha(0f, 1f, false); //second param is the time
        }
    }
}
