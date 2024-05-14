using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool activated;
    public bool permanent;
    public GameObject[] spikes;
    void Start()
    {
        print(transform.childCount);
        spikes = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount;i++)
            spikes[i] = transform.GetChild(i).gameObject;
        //print(spikes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other){
        if(!activated&&!other.CompareTag("arrow")&&!other.CompareTag("bomb")){ActivateAll();}
    }
    private void OnTriggerExit2D(Collider2D other){
        if(!permanent&&!other.CompareTag("arrow")&&!other.CompareTag("bomb"))DeactivateAll();
    }
    void ActivateAll(){
        activated = true;
        for(int i = 0; i < spikes.Length;i++){
            if(spikes[i].GetComponent<SpikeTile>().selfStart)
                spikes[i].GetComponent<SpikeTile>().suspended =!spikes[i].GetComponent<SpikeTile>().suspended ;
            else   
                spikes[i].GetComponent<SpikeTile>().SetActiveOperation(!spikes[i].GetComponent<SpikeTile>().active);
        }
    }
    void DeactivateAll(){
        activated = false;
        for(int i = 0; i < spikes.Length;i++){
            if(spikes[i].GetComponent<SpikeTile>().selfStart){
                spikes[i].GetComponent<SpikeTile>().suspended =!spikes[i].GetComponent<SpikeTile>().suspended ;
            }
            else   
                spikes[i].GetComponent<SpikeTile>().SetActiveOperation(!spikes[i].GetComponent<SpikeTile>().active);
        }
    }
}
