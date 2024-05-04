using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed;
    public float damage;
    public Rigidbody2D rb;
    public bool isExplosive;
    public bool isBoomerang;
    public GameObject explosive;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = speed*(transform.eulerAngles.z == 0 ? Vector2.up
        : transform.eulerAngles.z ==90? Vector2.left
        : transform.eulerAngles.z == 180? Vector2.down
        : transform.eulerAngles.z == 270? Vector2.right
        : Vector2.zero);
        print(transform.eulerAngles.z);
    }
    void OnCollisionEnter2D(Collision2D col){
        Destroy(gameObject);
    }
    void OnDestroy(){
        if(isExplosive)
            Instantiate(explosive, transform.position,transform.rotation);
    }
}
