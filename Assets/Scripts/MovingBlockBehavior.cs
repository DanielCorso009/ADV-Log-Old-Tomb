using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingBlockBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private GameObject player;
    private PlayerController playerS;
    public Vector2 look;
    private int z;
    public Transform pushDist;
    public Vector2 move = new Vector2(0,0);
    public LayerMask coll;
    private bool wait = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerS = player.GetComponent<PlayerController>();
        pushDist.parent = null;
        }

    // Update is called once per frame
    void Update()
    {
        z = transform.position.y > player.transform.position.y ? 1: -1;
        transform.position = Vector3.MoveTowards(transform.position,pushDist.position,4*Time.deltaTime);

        if(transform.position == pushDist.position){
            wait = false;
        }
    }
    private void OnCollisionStay2D(Collision2D col){
        if(!wait && col.gameObject == player){
            //playerS.anim.SetBool("Push", true);
            if(playerS.L == 1){
                move.x = -1;
                move.y = 0;
            }else if(playerS.R == 1){
                move.x = 1;
                move.y = 0;
            }else if(playerS.U == 1){
                move.y = 1;
                move.x = 0;
            }else if(playerS.D == 1){
                move.y = -1;
                move.x = 0;
            }
            if(!Physics2D.OverlapCircle(pushDist.position+ new Vector3(move.x*0.5f,move.y*0.5f,0),0.1f,coll)){
                print("Yup");
                pushDist.position += new Vector3(move.x,move.y,0);
                wait = true;
                move= new Vector2(0,0);
            }else playerS.trans.position = player.transform.position-new Vector3(playerS.move.x,playerS.move.y,player.transform.position.z)*0.25f;

        }
        
    }


}
