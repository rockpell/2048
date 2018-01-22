﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void restartGame() {
        Application.LoadLevel(0);
    }

    public void loadMainMenu() {
        SceneManager.LoadScene("menu");
    }

    public void loadGame() {
        SceneManager.LoadScene("main");
    }

    public void exitGame() {
        Application.Quit();
    }
}
