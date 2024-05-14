using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject how2play;
    private int press = 2;

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            press--;
            how2play.SetActive(!how2play.activeSelf);
        }
        if(press <= 0){
            SceneManager.LoadScene("GAME");
        }
    }
}
