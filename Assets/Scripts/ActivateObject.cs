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
}
