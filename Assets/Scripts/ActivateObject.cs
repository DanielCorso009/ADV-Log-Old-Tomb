using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    private  EnemyBehavior enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyBehavior>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Main Camera")){
            gameObject.SetActive(false);
        }
    }
}
