using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{

    private float boundaryX = 12;
    private float boundaryYTop =7f;
    private float boundaryYBottom =4;
    //private Camera camera;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetKeyUp(KeyCode.Escape)){
            SceneManager.LoadScene("Main Game");
        }
        boundaryX = 12;
        if(player.transform.position.x > transform.position.x + boundaryX){
            transform.position = new Vector3(transform.position.x + 2*boundaryX,transform.position.y,-10);
        }        
        else if(player.transform.position.x < transform.position.x - boundaryX){
            transform.position = new Vector3(transform.position.x - 2*boundaryX,transform.position.y,-10);
        }
        if(player.transform.position.y > transform.position.y + boundaryYTop){
            transform.position = new Vector3(transform.position.x,transform.position.y + 11,-10);
        }        
        else if(player.transform.position.y < transform.position.y - boundaryYBottom){
            transform.position =  new Vector3(transform.position.x,transform.position.y - 11,-10);
        }
    }
}
