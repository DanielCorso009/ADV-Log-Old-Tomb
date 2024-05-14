using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSeg : MonoBehaviour
{

    public float speed;
    public BossArmBehavior armScript;
    public GameObject previousLink;
    public float z;
    public bool retract = true;
    public bool joined = false;
    // Start is called before the first frame update
    void Start()
    {
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        ArmAtkLoop();
        transform.position = new Vector3(transform.position.x, transform.position.y,z);
    }

    public void ArmAtkLoop(){
        if(retract){
              transform.position = Vector2.MoveTowards(transform.position,armScript.returnPoint,speed*Time.deltaTime);
              if(Vector2.Distance(transform.position,armScript.returnPoint)==0){
                joined = true;
              }
        }else transform.position = Vector2.MoveTowards(transform.position,armScript.playerPoint,speed*Time.deltaTime);
        if(Vector2.Distance(transform.position,armScript.playerPoint) <= 1/16 || Vector2.Distance(transform.position,previousLink.transform.position)>=0.5f){
            retract = true;
        }
    }
    void OnTriggerStay2D(Collider2D col){
        if(col.CompareTag("sword")){
            retract = true;
            armScript.stunMult = 3;
        }
    }
}
