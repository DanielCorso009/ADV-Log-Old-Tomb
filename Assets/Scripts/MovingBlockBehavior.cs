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
        transform.position = Vector3.MoveTowards(transform.position,pushDist.position,4*Time.deltaTime);
        if(transform.position == pushDist.position){
            wait = false;
        }
    }
    public void Push(Vector3 direction){
        if(!Physics2D.OverlapCircle(pushDist.position+ new Vector3(direction.x,direction.y,0),0.4f,coll)&& !wait){
            pushDist.position += direction;
            wait = true;
        }
    }


}
