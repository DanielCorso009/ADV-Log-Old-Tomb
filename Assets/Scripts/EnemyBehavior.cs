using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed;
    public float move_time;
    public int health;
    private bool invulnerability = false;
    private Rigidbody2D rb;
    public Vector2 move = new Vector2(0,0);
    private Vector3 look;
    public GameObject player;
    private Animator anim;
    public SpriteRenderer sprite;
    
    void Start()
    {
        player = GameObject.Find("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        look = player.transform.position - transform.position;
        if(health == 0){
            Destroy(gameObject);
        }else if(invulnerability){
            sprite.enabled = !sprite.enabled;
            speed =0;
        }
            MovementController();
            rb.velocity = move*speed;
            //transform.Translate(move*speed*Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Main Camera")){

        }
        if(!invulnerability)
            if(col.gameObject.tag.Equals("sword")||col.gameObject.tag.Equals("Bomb")){
                health--;
                invulnerability = true;
                StartCoroutine("Invulnerable");
            }
    }
    void MovementController(){
        move = new Vector2(0,0);
        if(Math.Abs(look.x)>Math.Abs(look.y))
            {anim.SetBool("U",false);
            anim.SetBool("D",false);
            if(look.x<0)
            {anim.SetBool("L",true);
            anim.SetBool("R",false);
            move.x =-1;}
            else 
            if(look.x>0)
            {anim.SetBool("L",false);
            anim.SetBool("R",true);
            move.x = 1;}
            else move.x = 0;}
        else 
        if(Math.Abs(look.x)<Math.Abs(look.y))
            {anim.SetBool("R",false);
            anim.SetBool("L",false);
            if(look.y>0)
            {anim.SetBool("U",true);
            anim.SetBool("D",false);
            move.y =1;}
            else 
            if(look.y<0)
            {anim.SetBool("U",false);
            anim.SetBool("D",true);
            move.y = -1;}
            else move.y = 0;}
        else move = Pick();

    }
    Vector2 Pick(){
        Vector2 val = new Vector2(0,0);
        val = new Vector2(UnityEngine.Random.Range(-1,2),UnityEngine.Random.Range(-1,2));
        if((val.x == -1 || val.x == 1)&&(val.y == -1 || val.y == 1)){
            val = Pick();
        }
        return val;
    }
    IEnumerator Invulnerable(){
        yield return new WaitForSeconds(0.2f);
        invulnerability = false;
        sprite.enabled = true;
        speed = 2;

    }
}
