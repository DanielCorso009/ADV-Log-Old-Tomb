using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public GameObject explosive;
    private SpriteRenderer sprite;
    public int alt = 0;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine("explode");
    }

    // Update is called once per frame
    void Update()
    {
        if(alt%4 == 2)sprite.color = Color.red;else if(alt%4 == 0)sprite.color = Color.white;
        alt++;
    }
    IEnumerator explode(){
        yield return new WaitForSeconds(2);
        Instantiate(explosive,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
