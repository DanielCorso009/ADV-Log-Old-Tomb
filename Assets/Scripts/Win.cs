using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.Escape))Application.Quit();
    }
    private void OnTriggerEnter2D(Collider2D col){
        SceneManager.LoadScene("StartScreen");
    }
}
