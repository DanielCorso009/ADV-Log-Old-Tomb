using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private GameObject player;
    private PlayerController playerS;
    public Vector2 look;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerS = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionStay2D(Collision2D col){
        if(col.gameObject.layer ==3){
                    look = (transform.position-player.transform.position).normalized;
                    int z = transform.position.y > player.transform.position.y ? 1: -1;
                   transform.position = new Vector3(transform.position.x,transform.position.y,z);
            rb.AddForce(look,ForceMode2D.Force);
        }
    }
}
