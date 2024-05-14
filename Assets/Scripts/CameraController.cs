using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    class Room{
        public int ID;
        public GameObject R;
        public Vector2 coords;

        public Room(float x, float y, GameObject R, int ID){
            coords = new Vector2(x, y);
            this.R = R;
            this.ID = ID;
        }
    }

    private Room[] rooms = new Room[32];
    private float boundaryX = 12;
    private float boundaryYTop =7f;
    private float boundaryYBottom =4;
    //private Camera camera;
    private GameObject player;

    public Transform yourLocation;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        for (int i = 0; i < rooms.Length;i++){
            GameObject j = GameObject.Find("Room"+i);
            if(j != null)rooms[i]=new Room(j.transform.position.x,j.transform.position.y,j,i);
            else rooms[i] = new Room(0,0,null,i);
        }
        //camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i<rooms.Length;i++){
            if(rooms[i].R != null){
                if(Vector2.Distance(transform.position,rooms[i].coords) == 0)
                    rooms[i].R.SetActive(true);
                else rooms[i].R.SetActive(false);
            }
        }
        if(Input.GetKeyUp(KeyCode.Escape)){
            SceneManager.LoadScene("Main Game");
        }
        boundaryX = 12;
        if(player.transform.position.x > transform.position.x + boundaryX){
            transform.position = new Vector3(transform.position.x + 2*boundaryX,transform.position.y,-10);
            yourLocation.position = new Vector3(yourLocation.position.x+0.45f,yourLocation.position.y,0);
        }        
        else if(player.transform.position.x < transform.position.x - boundaryX){
            transform.position = new Vector3(transform.position.x - 2*boundaryX,transform.position.y,-10);
            yourLocation.position = new Vector3(yourLocation.position.x-0.45f,yourLocation.position.y,0);

        }
        if(player.transform.position.y > transform.position.y + boundaryYTop){
            transform.position = new Vector3(transform.position.x,transform.position.y + 11,-10);
            yourLocation.position = new Vector3(yourLocation.position.x,yourLocation.position.y+0.45f,0);
        }        
        else if(player.transform.position.y < transform.position.y - boundaryYBottom){
            transform.position =  new Vector3(transform.position.x,transform.position.y - 11,-10);
            yourLocation.position = new Vector3(yourLocation.position.x,yourLocation.position.y-0.45f,0);
        }
    }
}
