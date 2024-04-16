using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpiritBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed;
    public float move_time;
    public int health;
    private bool invulnerability = false;
    public Vector2 move = new Vector2(0,0);
    private Vector3 look;
    public GameObject player;
    public SpriteRenderer sprite;

    void Start()
    {
        player = GameObject.Find("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        look = (player.transform.position - transform.position).normalized;
        if(health == 0){
            Destroy(gameObject);
        }else if(invulnerability){
            sprite.enabled = !sprite.enabled;
        }
        transform.Translate(look*speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D col){
        if(!invulnerability)
            if(col.gameObject.tag.Equals("sword")||col.gameObject.tag.Equals("Bomb")){
                health--;
                invulnerability = true;
            }
        StartCoroutine("Invulnerable");
    }
    IEnumerator Invulnerable(){
        yield return new WaitForSeconds(0.2f);
        invulnerability = false;
        sprite.enabled = true;
    }
}
