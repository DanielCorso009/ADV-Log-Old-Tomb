using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    private Vector2 move = new Vector2(0,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementController();
    }
    void MovementController(){
        int U = 0;
        int D = 0;
        int L = 0;
        int R = 0;

        if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)){
            U = 1;
        }
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){
            L = 1;
        }
        if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)){
            D = 1;
        }
        if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){
            R = 1;
        }

        move = new Vector2(R-L,U-D);
        transform.Translate(move * speed*Time.deltaTime);
    }
}
