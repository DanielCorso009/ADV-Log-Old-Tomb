using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player ;
    public PlayerController playerScript;
    public GameObject healthBar;
    public Slider HPslider;
    void Start()
    {
        player =GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        HPslider = healthBar.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        HPslider.value = playerScript.health;
        if(playerScript.health >=0){
            SceneManager.LoadScene("StartScreen");
        }
    }
}
