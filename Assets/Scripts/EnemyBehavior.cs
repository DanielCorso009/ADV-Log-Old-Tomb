using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed;
    private int usedSpeed;
    public float move_time = 3;
    public int health;
    private bool invulnerability = false;
    //private bool frozen = false;
    public Vector2 move = new Vector2(0,0);
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
        usedSpeed = speed;
        StartCoroutine("UpdatePath");
        MovementController();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position,trans.position,usedSpeed*Time.deltaTime);
        if(health == 0){
            Destroy(gameObject);
            Destroy(trans.gameObject);
        }

        if(invulnerability){
            sprite.enabled = !sprite.enabled;
        }else{
            if(Vector2.Distance(transform.position,trans.position) <= 0.1f){
                if(!Physics2D.OverlapCircle(trans.position+ new Vector3(move.x*0.25f,move.y*0.25f,transform.position.z),0.3f,coll)){
                    trans.position += new Vector3(move.x*0.25f,move.y*0.25f,transform.position.z);}
            }
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D col){
        trans.position = transform.position;
    }
    private void OnTriggerStay2D(Collider2D col){
        if(!invulnerability){
            switch(col.tag)
                {
                case "sword":health--;invulnerability = true;StartCoroutine("Invulnerable");break;
                case "arrow":health-=2;invulnerability = true;StartCoroutine("Invulnerable");break;
                case "Bomb":health-=3;invulnerability = true;StartCoroutine("Invulnerable");break;
                default:               break; 
                }        
            }
    }
    void MovementController(){
        move = Pick();
        if(move.x ==1){
            anim.SetTrigger("R");
        }else if(move.x ==-1){
            anim.SetTrigger("L");
        }else if(move.y ==1){
            anim.SetTrigger("U");
        }else if(move.y ==-1){
            anim.SetTrigger("D");
        }

    }

    Vector2 Pick(){
        Vector2 val;
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
        usedSpeed = speed;

    }
    IEnumerator UpdatePath(){
        yield return new WaitForSeconds(move_time);
        MovementController();
        StartCoroutine("UpdatePath");
    }
    private void OnEnable()
    {        
        StartCoroutine("UpdatePath");
    }
}
