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
    public float move_time = 0f;
    public int health;
    private bool invulnerability = false;
    //private bool frozen = false;
    private Rigidbody2D rb;
    public Vector2 move = new Vector2(0,0);
    private Vector3 look;
    public GameObject player;
    private Animator anim;
    public SpriteRenderer sprite;
    public LayerMask coll;

    public Transform trans;
    
    void Start()
    {
        trans.parent = null;
        player = GameObject.Find("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,trans.position,speed*Time.deltaTime);

        if(health == 0){
            Destroy(gameObject);
            Destroy(trans.gameObject);
        }

        if(invulnerability){
            sprite.enabled = !sprite.enabled;
        }else{
            if(Vector2.Distance(transform.position,trans.position) <= 0.1f){
                if(!Physics2D.OverlapCircle(trans.position+ new Vector3(move.x*0.25f,move.y*0.25f,transform.position.z),0.3f,coll)){
                    if(move_time <=0f){MovementController();}
                    trans.position += new Vector3(move.x*0.25f,move.y*0.25f,transform.position.z);}
                else {move = Pick(move); print(move);}
            }
            move_time--;
        }
        
    }
    void OnCollisionEnter2D(Collision2D col){
        trans.position = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D col){
        if(!invulnerability)
            if(col.gameObject.tag.Equals("sword")||col.gameObject.tag.Equals("Bomb")){
                health--;
                invulnerability = true;
                StartCoroutine("Invulnerable");
            }
    }
    void MovementController(){
         move = Pick(new Vector2(1,1));
        if(move.x ==1 && move.y ==0){
            anim.SetBool("U",false);
            anim.SetBool("D",false);
            anim.SetBool("R",true);
            anim.SetBool("L",false);
        }else if(move.x ==-1 && move.y ==0){
            anim.SetBool("U",false);
            anim.SetBool("D",false);
            anim.SetBool("R",false);
            anim.SetBool("L",true);
        }else if(move.y ==1 && move.x ==0){
            anim.SetBool("R",false);
            anim.SetBool("L",false);
            anim.SetBool("U",true);
            anim.SetBool("D",false);
        }else if(move.y ==-1 && move.x ==0){
            anim.SetBool("R",false);
            anim.SetBool("L",false);
            anim.SetBool("U",false);
            anim.SetBool("D",true);
        }
        move_time = 180;
    }

    Vector2 Pick(Vector2 exclude){
        Vector2 val = new Vector2(0,0);
        val = new Vector2(UnityEngine.Random.Range(-1,2),UnityEngine.Random.Range(-1,2));
        if(((val.x == -1 || val.x == 1)&&(val.y == -1 || val.y == 1))||val == new Vector2(0,0) || val==exclude){
            val = Pick(exclude);
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
