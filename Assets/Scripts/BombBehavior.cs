using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public GameObject explosive;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine("explode");
    }

    // Update is called once per frame

    IEnumerator explode(){
        yield return new WaitForSeconds(2);
        Instantiate(explosive,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
