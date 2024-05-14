using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer spr;
    public GameObject bolt;
    public GameObject father;
    public int health = 30;
    public bool Invulnerable = false;
    public bool flag1 = false;
    public bool flag2 = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        father = transform.parent.gameObject;
    }
    void Update()
    {
        if(Invulnerable){
            spr.enabled = !spr.enabled;
        }
        if(health ==22){
            flag1 = true;
            anim.SetTrigger("right");
        }else if(health ==12){
            flag2 = true;
            anim.SetTrigger("left");
        }else if(health <= 0){
            Destroy(father.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col){
        if(!Invulnerable){ 
            switch(col.tag)
                {
                case "sword":health--;Invulnerable = true;StartCoroutine("Invincible");break;
                case "arrow":health-=2;Invulnerable = true;StartCoroutine("Invincible");break;
                case "Bomb":health-=3;Invulnerable = true;StartCoroutine("Invincible");break;
                default:               break; 
                }        
        }

    }
    IEnumerator Invincible(){
        yield return new WaitForSeconds(0.25f);
        Invulnerable = false;
        if(flag1){
            Instantiate(bolt,transform.position + new Vector3(1/16,9/32,-2),Quaternion.Euler(0,0,-90));
            flag1 = false;
            anim.SetTrigger("mid");
        }
        if(flag2){
            Instantiate(bolt,transform.position + new Vector3(-1/16,9/32,-2),Quaternion.Euler(0,0,90));
            anim.SetTrigger("mid");
            flag2 = false;
        }
    }
    
}
