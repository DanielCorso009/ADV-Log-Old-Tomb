using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class BossArmBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int linkCount;
    [SerializeField] float phaseTime;
    public int stunMult = 1;
    public GameObject segment;
    public GameObject head;
    public GameObject player;
    public PlayerController playerS;
    private float pixel = 1/16f;
    public GameObject[] segments;
    public int atkState = 1;
    private Vector2 offset;
    public Vector2 returnPoint;
    public Vector2 playerPoint;
    private bool once = true;
    void Start()
    {
        offset = new Vector2(0,pixel*2);
        segments = new GameObject[linkCount];
        segments[0] = segment;
        segments[segments.Length-1] = head;
        head.GetComponent<BossSeg>().armScript = gameObject.GetComponent<BossArmBehavior>();
        if(linkCount > 2){

// used for making an assigning variable segment length
            for (int i = 1; i<segments.Length-1;i++){
                segments[i] = Instantiate(segment,(Vector2)segments[0].transform.position-offset, transform.rotation,gameObject.transform);
                segments[i].name = "seg "+i;
                segments[i].AddComponent<BossSeg>();
                segments[i].GetComponent<BossSeg>().armScript = gameObject.GetComponent<BossArmBehavior>();
                segments[i].GetComponent<BossSeg>().speed = 8f;
                segments[i].GetComponent<BossSeg>().previousLink = segments[i-1];
                segments[i].transform.position = new Vector3(transform.position.x,transform.position.y,-2);
            }

        }
        head.GetComponent<BossSeg>().previousLink = segments[segments.Length-2];
        head.GetComponent<BossSeg>().speed = 8f;
        player = GameObject.Find("Player");
        playerS =  player.GetComponent<PlayerController>();
        returnPoint = (Vector2)segments[0].transform.position-offset;
        
    }
    void Update(){
        if(head.GetComponent<BossSeg>().joined && once){
            StartCoroutine("StateSwitch");
            once = false;
        }
    }
    void BackToTrue(){
        for (int i =1; i<segments.Length;i++){ 
        segments[i].GetComponent<BossSeg>().retract = false; 
        segments[i].GetComponent<BossSeg>().joined = false;
        }
    }
    public IEnumerator StateSwitch(){
        yield return new WaitForSeconds(phaseTime*stunMult);
        atkState = UnityEngine.Random.Range(0,2);
        playerPoint = (Vector2)playerS.trans.position + playerS.move*(playerS.speed*UnityEngine.Random.Range(0.0f,1f));
        BackToTrue();
        once = true;
        stunMult = 1;
    }
    private void OnEnable(){
        StartCoroutine("StateSwitch");
    }
}
