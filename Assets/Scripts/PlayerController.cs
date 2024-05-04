using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    public int health = 5;
    public float useTime = 0f;
    public bool invulnerabilityTime = false;
    public bool frozen = false;
    public bool freeze_inpts = false;
    public bool freeze_atks = false;
    public LayerMask coll;
    public Vector2 move = new Vector2(0,0);
    public Vector2 lastSavedPosition;
    public Animator anim;
    public SpriteRenderer sprite;
    public GameObject sword;
    public GameObject bomb;
    public GameObject explosive;
    public GameObject bow;
    public GameObject arrow;
    public GameObject rocket;
    public GameObject sworderang;
    public GameObject enemy;
    public Transform trans;

    private Rigidbody2D rb;
    public    int U = 0;
    public    int D = 0;
    public    int L = 0;
    public    int R = 0;
    // Start is called before the first frame update
    void Start()
    {
        trans.parent = null;
        anim  = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        print(coll.value);
    }

    // Update is called once per frame
    void Update()
    {
        if(!frozen){
            transform.position = Vector3.MoveTowards(transform.position,trans.position,speed*Time.deltaTime);
            
                if(Vector2.Distance(transform.position,trans.position) <= 0.05f){
                    if(!freeze_inpts)MovementController(); 
                    if(!Physics2D.OverlapCircle(trans.position+ new Vector3(move.x*0.25f,move.y*0.25f,transform.position.z),0.3f,coll))
                        trans.position += new Vector3(move.x*0.25f,move.y*0.25f,transform.position.z);
                }
                if(!freeze_atks)AttackController();
        }

        if(invulnerabilityTime){
            sprite.enabled = !sprite.enabled;
        }

    }
    void MovementController(){
        U = 0;
        D = 0;
        L = 0;
        R = 0;
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
        coll.value = 208;
        Physics2D.IgnoreLayerCollision(3,4,false);

        bool ZX = (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.X))||Input.GetKey(KeyCode.Space);

        bool XC = (Input.GetKeyDown(KeyCode.X) && Input.GetKeyDown(KeyCode.C))||Input.GetKeyDown(KeyCode.V);

        bool ZC = (Input.GetKeyDown(KeyCode.Z) && Input.GetKeyDown(KeyCode.C))||Input.GetKeyDown(KeyCode.B);
        
        if(ZX){//thrust combo
            speed = 10;
            freeze_inpts =true;
            coll.value = 192;
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
                    if(anim.GetBool("Up")){
                        Instantiate(bow,transform.position + new Vector3(0,0.3f,2),Quaternion.Euler(0,0,90));
                        Instantiate(rocket,transform.position + new Vector3(0,0.5f,-2),Quaternion.Euler(0,0,0));}
                    else if(anim.GetBool("Down")){
                        Instantiate(bow,transform.position + new Vector3(0,-0.3f,-2),Quaternion.Euler(0,0,-90));
                        Instantiate(rocket,transform.position + new Vector3(0,-0.5f,-2),Quaternion.Euler(0,0,180));}
                    else if(anim.GetBool("Side")){
                        Instantiate(bow,transform.position + new Vector3(0.4f* (sprite.flipX ? -1:1),0,0),Quaternion.Euler(0,0,sprite.flipX?180:0));
                        Instantiate(rocket,transform.position + new Vector3(0.5f* (sprite.flipX ? -1:1),0,0),Quaternion.Euler(0,0,90*(sprite.flipX ? 1:-1)));}
                anim.SetTrigger("Atk");
                frozen = true;
                speed = 0;
                useTime = 0.5f;
                StartCoroutine("Unfreeze");
        }else if(ZC){
                    if(anim.GetBool("Up")){
                        Instantiate(bow,transform.position + new Vector3(0,0.3f,2),Quaternion.Euler(0,0,90));
                        Instantiate(sworderang,transform.position + new Vector3(0,1f,-2),transform.rotation);}
                    else if(anim.GetBool("Down")){
                        Instantiate(bow,transform.position + new Vector3(0,-0.3f,-2),Quaternion.Euler(0,0,-90));
                        Instantiate(sworderang,transform.position + new Vector3(0,-1f,-2),transform.rotation);}
                    else if(anim.GetBool("Side")){
                        Instantiate(bow,transform.position + new Vector3(0.4f* (sprite.flipX ? -1:1),0,0),Quaternion.Euler(0,0,sprite.flipX?180:0));
                        Instantiate(sworderang,transform.position + new Vector3(1f* (sprite.flipX ? -1:1),0,0),transform.rotation);}
                anim.SetTrigger("Atk");
                frozen = true;
                freeze_atks = true;
                speed = 0;
                useTime = 0.4f;
                StartCoroutine("Unfreeze");

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
                useTime = 0.2f;
                StartCoroutine("Unfreeze");
            }else if(Input.GetKeyDown(KeyCode.X)){
                    if(anim.GetBool("Up")){
                        Instantiate(bomb,transform.position + new Vector3(0,0.5f,-2),Quaternion.Euler(0,0,0));}
                    else if(anim.GetBool("Down")){
                        Instantiate(bomb,transform.position + new Vector3(0,-0.5f,-2),Quaternion.Euler(0,0,0));}
                    else if(anim.GetBool("Side")){
                        Instantiate(bomb,transform.position + new Vector3(0.75f* (sprite.flipX ? -1:1),0,-2),Quaternion.Euler(0,0,0));}
                anim.SetTrigger("Atk");
                frozen = true;
                speed = 0;
                useTime = 0.3f;
                StartCoroutine("Unfreeze");
        //spawn enemy
            }else if(Input.GetKeyDown(KeyCode.C)){
                    if(anim.GetBool("Up")){
                        Instantiate(bow,transform.position + new Vector3(0,0.3f,2),Quaternion.Euler(0,0,90));
                        Instantiate(arrow,transform.position + new Vector3(0,0.5f,-2),Quaternion.Euler(0,0,0));}
                    else if(anim.GetBool("Down")){
                        Instantiate(bow,transform.position + new Vector3(0,-0.3f,-2),Quaternion.Euler(0,0,-90));
                        Instantiate(arrow,transform.position + new Vector3(0,-0.5f,-2),Quaternion.Euler(0,0,180));}
                    else if(anim.GetBool("Side")){
                        Instantiate(bow,transform.position + new Vector3(0.4f* (sprite.flipX ? -1:1),0,0),Quaternion.Euler(0,0,sprite.flipX?180:0));
                        Instantiate(arrow,transform.position + new Vector3(0.5f* (sprite.flipX ? -1:1),0,0),Quaternion.Euler(0,0,90*(sprite.flipX ? 1:-1)));}
                anim.SetTrigger("Atk");
                frozen = true;
                speed = 0;
                useTime = 0.5f;
                StartCoroutine("Unfreeze");
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
        if(freeze_inpts){
           Instantiate(explosive,transform.position,transform.rotation);
           freeze_inpts = false;
           anim.SetBool("Thrust", false);
        }

    }
    void OnCollisionExit2D(){
        
    }
    IEnumerator Invulnerable(){
        yield return new WaitForSeconds(1.5f);
        invulnerabilityTime = false;
        sprite.enabled = true;
    }
    IEnumerator Unfreeze(){
        yield return new WaitForSeconds(useTime);
        frozen = false;
        speed = 5;
    }
}
