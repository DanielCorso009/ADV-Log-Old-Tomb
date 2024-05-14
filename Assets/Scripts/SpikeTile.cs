using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : MonoBehaviour
{
    public bool active;
    public bool selfStart;
    public bool suspended;
    public float switchTime;
    public Sprite on;
    public Sprite off;
    private SpriteRenderer sprite;
    private BoxCollider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = active? on:off;
        hitbox = GetComponent<BoxCollider2D>();
        if(selfStart){StartCoroutine("TimedActivity");}
        hitbox.enabled = active;
        gameObject.tag = active? "spike" : "Untagged";
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetActiveOperation(bool set){
        active = set;
        sprite.sprite = active? on:off;
        gameObject.tag = active? "spike" : "Untagged";
        hitbox.enabled = active;
    }
    public IEnumerator TimedActivity(){
        yield return new WaitForSeconds(switchTime);
        if(!suspended){
            SetActiveOperation(!active);
        }
        StartCoroutine("TimedActivity");

    }

    void OnEnable(){
        if(selfStart){StartCoroutine("TimedActivity");}
    }
}
