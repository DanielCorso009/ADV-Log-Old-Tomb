using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float boundaryX = 14;
    private float boundaryY =5f;
    
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player.transform.position.x > transform.position.x + boundaryX){
            transform.position = new Vector3(transform.position.x + 2*boundaryX,transform.position.y,-10);
        }        
        else if(player.transform.position.x < transform.position.x - boundaryX){
            transform.position = new Vector3(transform.position.x - 2*boundaryX,transform.position.y,-10);
        }
        if(player.transform.position.y > transform.position.y + boundaryY+2.5){
            transform.position = new Vector3(transform.position.x,transform.position.y + 2*boundaryY+2.5f,-10);
        }        
        else if(player.transform.position.y < transform.position.y - boundaryY){
            transform.position =  new Vector3(transform.position.x,transform.position.y - 2*boundaryY-2.5f,-10);
        }
    }
}
