using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    public int health = 5;
    public bool invulnerabilityTime = false;
    public bool frozen = false;
    public bool freeze_inpts = false;
    public Vector2 move = new Vector2(0,0);
    public Vector2 lastSavedPosition;
    private Animator anim;
    private SpriteRenderer sprite;
    public GameObject sword;
    public GameObject bomb;
    public GameObject explosive;
    public GameObject enemy;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim  = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!frozen){
            if(!freeze_inpts){MovementController(); }
            AttackController();
        //transform.Translate(move * speed*Time.deltaTime);
        //rb.AddForce(move*speed);
        //rb.MovePosition((Vector2)transform.position +(move*speed*Time.deltaTime));
            rb.velocity = move*speed;
        }
        if(invulnerabilityTime){
            sprite.enabled = !sprite.enabled;
        }

    }
    void MovementController(){
        int U = 0;
        int D = 0;
        int L = 0;
        int R = 0;
        speed = 5;
        if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)){
            U = 1;
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
            anim.SetBool("Side", false);

            anim.SetBool("Walk", true);
            sprite.flipX = false;
            lastSavedPosition = transform.position;
        }
        else if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){
            L = 1;
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Side", true);
            
            anim.SetBool("Walk", true);

            sprite.flipX = true;
            lastSavedPosition = transform.position;
        }
        else if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)){
            D = 1;
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
            anim.SetBool("Side", false);
            
            anim.SetBool("Walk", true);
            sprite.flipX = false;
            lastSavedPosition = transform.position;
        }
        else if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){
            R = 1;
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Side", true);
            
            anim.SetBool("Walk", true);
            sprite.flipX = false;
            lastSavedPosition = transform.position;

        }else{
            anim.SetBool("Walk", false);
        }

        move = new Vector2(R-L,U-D);
    }
    void AttackController(){
        freeze_inpts = false;
        anim.SetBool("Thrust", false);
        Physics2D.IgnoreLayerCollision(3,4,false);
        bool ZX = Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.X);

        bool XC = Input.GetKeyDown(KeyCode.X) && Input.GetKeyDown(KeyCode.C);

        bool ZC = Input.GetKeyDown(KeyCode.Z) && Input.GetKeyDown(KeyCode.C);
        if(ZX){//thrust combo
            speed = 8;
            freeze_inpts =true;
            Physics2D.IgnoreLayerCollision(3,4,true);
            anim.SetBool("Thrust",true);
            if(anim.GetBool("Up")){
                anim.SetBool("Down", false);
                anim.SetBool("Side", false);
                move = new Vector2(0,1);
            }else if(anim.GetBool("Down")){
                anim.SetBool("Up", false);
                anim.SetBool("Side", false);
                move = new Vector2(0,-1);
            }else if(anim.GetBool("Side")){
                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
                move = new Vector2(sprite.flipX ? -1:1,0);
            }
        }else if(XC){
            print("hello");
        }else if(ZC){
            print("Kachow");
        }else{
            if(Input.GetKeyDown(KeyCode.Z)){
                if(anim.GetBool("Up")){
                    Instantiate(sword,transform.position + new Vector3(0,0.5f,1),Quaternion.Euler(0,0,0));}
                else if(anim.GetBool("Down")){
                    Instantiate(sword,transform.position + new Vector3(0,-0.5f,-2),Quaternion.Euler(0,0,180));}
                else if(anim.GetBool("Side")){
                    Instantiate(sword,transform.position + new Vector3(0.75f* (sprite.flipX ? -1:1),0,0),Quaternion.Euler(0,0,90*(sprite.flipX ? 1:-1)));}
                anim.SetTrigger("Atk");
                frozen = true;
                speed = 0;
                StartCoroutine("Unfreeze");
            }else if(Input.GetKeyDown(KeyCode.X)){
                    if(anim.GetBool("Up")){
                        Instantiate(bomb,transform.position + new Vector3(0,0.5f,1),Quaternion.Euler(0,0,0));}
                    else if(anim.GetBool("Down")){
                        Instantiate(bomb,transform.position + new Vector3(0,-0.5f,-2),Quaternion.Euler(0,0,0));}
                    else if(anim.GetBool("Side")){
                        Instantiate(bomb,transform.position + new Vector3(0.75f* (sprite.flipX ? -1:1),0,0),Quaternion.Euler(0,0,0));}
                anim.SetTrigger("Atk");
                frozen = true;
                speed = 0;
                StartCoroutine("Unfreeze");
        //spawn enemy
            }else if(Input.GetKeyDown(KeyCode.G)){
                Instantiate(enemy,new Vector3(UnityEngine.Random.Range(transform.position.x-14,transform.position.x+14), UnityEngine.Random.Range(transform.position.y-5,transform.position.y+5),0),transform.rotation);
        }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {

        if(!invulnerabilityTime){
            print(col.gameObject.tag);
            switch(col.gameObject.tag){
                case "Bomb":
                    health-=2;
                    invulnerabilityTime = true;
                    StartCoroutine("Invulnerable");
                break;
                case "Enemy":
                    health--;
                    invulnerabilityTime = true;
                    StartCoroutine("Invulnerable");
                break;
            }

        }
    }
    void OnCollisionEnter2D(Collision2D col){
        anim.SetBool("Push", true);
        if(freeze_inpts){
           Instantiate(explosive,transform.position,transform.rotation);
           freeze_inpts = false;
           anim.SetBool("Thrust", false);
        }

    }
    void OnCollisionExit2D(){
        anim.SetBool("Push", false);
    }
    IEnumerator Invulnerable(){
        yield return new WaitForSeconds(1.5f);
        invulnerabilityTime = false;
        sprite.enabled = true;
    }
    IEnumerator Unfreeze(){
        yield return new WaitForSeconds(0.2f);
        frozen = false;
        speed = 5;
    }
}
