using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player ;
    public PlayerController playerScript;
    public GameObject healthBar;
    public Slider slider;
    void Start()
    {
        player =GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        slider = healthBar.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerScript.health;
    }
}
