using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 distance;

    public int speed;
    public int damage;
    public GameObject player;
    public PlayerController playerS;
    public Transform trans;
    public bool toPlayer = false;
    public LayerMask coll;
    void Start()
    {
        trans.parent = null;
        player = GameObject.Find("Player");
        playerS = player.GetComponent<PlayerController>();

        distance = 4*(playerS.anim.GetBool("Up") ? Vector2.up
        : playerS.anim.GetBool("Side")&&playerS.sprite.flipX ? Vector2.left
        : playerS.anim.GetBool("Down") ? Vector2.down
        : playerS.anim.GetBool("Side")&&!playerS.sprite.flipX ? Vector2.right
        : Vector2.zero);
        trans.position = (Vector2)transform.position+distance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward*15);
        transform.position =Vector3.MoveTowards(transform.position,trans.position,speed*Time.deltaTime);
        if((Vector2.Distance(transform.position,trans.position) == 0 || Physics2D.OverlapCircle((Vector2)transform.position,0.3f,coll))&&toPlayer==false){
            toPlayer = true;
        }else if(toPlayer){
            trans.position = player.transform.position;
            speed = playerS.speed+2;
        }
        if(Vector2.Distance(transform.position,player.transform.position) <= 0.25f)
        { Destroy(gameObject); Destroy(trans.gameObject); playerS.freeze_atks =false;}
    }
}
